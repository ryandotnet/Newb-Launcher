using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Newb_Launcher
{
    public static class Misc
    {
        public static string maplestoryPath = "MapleStory.exe";
        public static string maplestoryDir = "";

        // Check the Registry for the MapleStory install location.
        private static List<Tuple<string, string>> registryPaths = new List<Tuple<string, string>>() {
            new Tuple<string,string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Wizet\\MapleStory", "ExecPath"),
            new Tuple<string,string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MapleStory","InstallLocation"),
            new Tuple<string,string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wizet\\MapleStory", "ExecPath"),
            new Tuple<string,string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MapleStory","InstallLocation")
        };

        
        // Launch MapleStory.
        public static async Task<bool> launchMaple(string username, string password, bool usenexonlaunch)
        {
            string token;
            int uid;
            string passport;

            try // Try to Log In to the Nexon Servers.
            {
                token = "";
                uid = -1;

                string response = await getAuthToken(username, password);

                var jsonparse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

                if (jsonparse.ContainsKey("access_token"))
                {
                    token = jsonparse["access_token"];
                    uid = (int)jsonparse["user_no"];
                }

                if (uid == -1 || token == "")
                {
                    return false;
                }
            }

            catch (Exception) // Not used kappa
            {
                MessageBox.Show("An Unknown Error has occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            try // Attempt to retrieve the Passport.
            {
                passport = await getLoginString(token);

                if (passport == null || passport == "")
                {
                    MessageBox.Show("An Error Occured when trying to retrieve the Passport!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }

            catch (Exception) // Not used kappa
            {
                MessageBox.Show("An Error Occured when trying retrieve the Passport!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            try // Attempt to Launch MapleStory
            {
                Process p = new Process();

                p.StartInfo.FileName = maplestoryPath;
                p.StartInfo.WorkingDirectory = maplestoryDir;

                if (usenexonlaunch)
                {
                    p.StartInfo.Arguments = "-nxl " + passport;
                }

                else
                {
                    p.StartInfo.Arguments = "WebStart " + passport;
                }

                p.Start();
            }

            catch (Exception) // Not used Again.. Kappa (^:
            {
                MessageBox.Show("Unable to start MapleStory! Please make sure this program is in your MapleStory folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }


        // SHA512 Text Conversion
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);

            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128); // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 

                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString().ToLower();
            }
        }

        // SHA256 Text Conversion
        public static string SHA256(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);

            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);          
                var hashedInputStringBuilder = new System.Text.StringBuilder(64); // StringBuilder Capacity is 64, because 256 bits / 8 bits in byte * 2 symbols for byte 

                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString().ToLower();
            }
        }


        // Convert Old Username Based Accounts to Email.
        public static async Task<bool> oldAccountConversion(string username, string password)
        {
            long user_no;

            string login_id;
            string email;
            string legacylogin_response = await Request.performLegacyLogin(username, password);

            var jsonparselegacy = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(legacylogin_response);

            if (jsonparselegacy.ContainsKey("is_verified") && jsonparselegacy["is_verified"])
            {
                user_no = jsonparselegacy["user_no"];
                login_id = jsonparselegacy["login_id"]; // Not Used TBH.
                email = jsonparselegacy["email"];

                string migration_response = await Request.performMigration(username, password, email);

                var jsonmigration = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(migration_response);

                if (jsonmigration.ContainsKey("user_no") && user_no == jsonmigration["user_no"])
                {
                    return true;
                }
            }

            return false;
        }


        // Trusted Device Verification (Hate this shit).
        public static async Task<bool> trustedDeviceVerification(string username, string password)
        {
            string code = "";

            code = Interaction.InputBox("Enter the Code sent to: (" + username + ").", "TRUSTED_DEVICE_ERROR", "");

            if (code != "")
            //if (inputBox.InputBox("TRUSTED_DEVICE_ERROR", "Please enter the code sent to your email (" + username + ") below.", ref code) == DialogResult.OK)
            {
                string response = await Request.trusted_devices(username, password, code);

                if (response.Length == 0)
                {
                    return true;
                }

                else
                {
                    MessageBox.Show("Incorrect Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }


        // Get Authentication Token.
        public static async Task<string> getAuthToken(string username, string password)
        {
            string response = await Request.performLogin(username, password);

            var jsonparse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

            if (jsonparse.ContainsKey("code"))
            {
                string errorcode = jsonparse["code"];
                string errormessage = jsonparse["message"];

                if (errorcode == "NOT_EXIST_USER" && await oldAccountConversion(username, password)) //This means the user doesn't exist or needs to change to email. 
                {
                    return await getAuthToken(username, password); // Attempt to Login Again.
                }

                else if (errorcode == "TRUST_DEVICE_REQUIRED" && await Misc.trustedDeviceVerification(username, password))
                {
                    return await getAuthToken(username, password);
                }

                MessageBox.Show(errorcode + ": " + errormessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return response;
        }

        // Get the Login String.
        public static async Task<string> getLoginString(string token)
        {
            string s = await Request.requestPassport(token);

            var jsonparse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(s);

            if (jsonparse.ContainsKey("passport"))
            {
                return jsonparse["passport"];
            }

            return null;
        }

        // Find MapleStory.exe Path from Registry Key.
        private static bool registryExists(string keyName, string keyValue, out string msDir, out string msPath)
        {
            msDir = "";
            msPath = "";

            string regmaplepath = (string)Registry.GetValue(keyName, keyValue, "");

            if (regmaplepath != "")
            {
                string exepath = regmaplepath + "\\MapleStory.exe";

                if (File.Exists(exepath))
                {
                    msDir = regmaplepath;
                    msPath = exepath;

                    return true;
                }
            }

            return false;
        }

        // Checks if MapleStory actually exists in said path.
        public static bool mapleExists()
        {
            if (File.Exists(maplestoryPath)) // Check current working directory.
            {
                return true;
            }

            else // Check Registry Keys.
            {
                string msDir;
                string msPath;

                foreach (Tuple<string, string> paths in registryPaths)
                {
                    if (registryExists(paths.Item1, paths.Item2, out msDir, out msPath))
                    {
                        maplestoryDir = msDir;
                        maplestoryPath = msPath;

                        return true;
                    }
                }

            }
            
            MessageBox.Show("Please put this launcher in the MapleStory folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Game not found.. Place in MapleStory folder hehe.

            return false;
        }
    }
}
