using RestSharp;
using System.Threading.Tasks;


namespace Newb_Launcher
{
    public static class Request
    {
        // Request Account API.
        public static async Task<string> accountsAPIRequest(string path, RestSharp.Method method, object body)
        {
            RestClient client = new RestClient("https://www.nexon.com/");
            RestRequest request = new RestRequest(path, method);

            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) NexonLauncher/1.0.1 Chrome/58.0.3029.110 Electron/1.7.13 Safari/537.36";

            request.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            request.AddHeader("Origin", "https://www.nexon.com");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-Language", "en-US");
            request.AddJsonBody(body);

            var result = await client.ExecuteTaskAsync(request);

            return result.Content;
        }


        // Legacy Login Request.
        public static async Task<string> performLegacyLogin(string username, string password)
        {
            return await accountsAPIRequest("/account/login/legacy", Method.POST, new { id = username, password = password });
        }

        // Migration Request.
        public static async Task<string> performMigration(string username, string password, string email)
        {
            return await accountsAPIRequest("/account/migrate", Method.POST, new { id = username, password = password, new_id = email });
        }

        // Login Request.
        public static async Task<string> performLogin(string username, string password)
        {
            return await accountsAPIRequest("/account-webapi/login/launcher", Method.POST, new { id = username, password = Misc.SHA512(password), auto_login = false, client_id = "7853644408", scope = "us.launcher.all", device_id = Misc.SHA256(username + password) });
        }

        // Trusted Device Error Request.
        public static async Task<string> trusted_devices(string username, string password, string code)
        {
            return await accountsAPIRequest("/account-webapi/trusted_devices", Method.PUT, new { email = username, remember_me = true, device_id = Misc.SHA256(username + password), verification_code = code });
        }


        // Nexon API Request.
        public static async Task<string> nexonAPIRequest(string path, RestSharp.Method method, string token)
        {
            var client = new RestClient("https://api.nexon.io/");
            var request = new RestRequest(path, method);

            client.UserAgent = "NexonLauncher.nxl-18.12.02-407-773e6dd";

            request.AddHeader("Authorization", "bearer " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token)));
            request.AddParameter("nxtk", token, ParameterType.Cookie);
            request.AddParameter("domain", ".nexon.net", ParameterType.Cookie);
            request.AddParameter("path", "/", ParameterType.Cookie);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "");

            var result = await client.ExecuteTaskAsync(request);

            return result.Content;
        }

        // Request Passport.
        public static async Task<string> requestPassport(string token)
        {
            return await nexonAPIRequest("/users/me/passport", Method.GET, token);
        }

        public static async Task<string> passportRefresh(string authserv, string passport)
        {
            var client = new RestClient("http://" + authserv + ".nexon.net/");
            var request = new RestRequest("/ajax/default.aspx?_vb=UpdateSession", Method.GET);

            client.UserAgent = "Python-urllib/2.7";

            request.AddParameter("NPPv2", passport, ParameterType.Cookie);

            var result = await client.ExecuteTaskAsync(request);

            return result.Content;
        }
    }
}
