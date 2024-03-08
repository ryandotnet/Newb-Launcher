using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Newb_Launcher
{
    public partial class MainForm : Form
    {
        Account account; // Make Reference

        static string saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Newb\\Launcher";
        string savePath = saveDirectory + "\\Newb Launcher.bin";

        public MainForm()
        {
            InitializeComponent();

            Directory.CreateDirectory(saveDirectory);

            loadAccounts();

            lbAccounts.DataSource = account.Usernames;
        }


        // Load Accounts from File.
        public void loadAccounts()
        {
            if (File.Exists(savePath))
            {
                account = Serialize.loadAccount(savePath);
            }

            else
            {
                account = new Account();
            }
        }

        // Save Accounts to File.
        public void saveAccounts()
        {
            Serialize.saveAccount(account, savePath);
        }


        // Gets Selected Item.
        private Tuple<string, string> getSelectedItem()
        {
            if (lbAccounts.SelectedItem != null)
            {
                Tuple<string, string> selected = account.getAccount((string)lbAccounts.SelectedItem);
                if (selected != null)
                {
                    return selected;
                }
            }
            return null;
        }


        // Update the Text Boxes.
        private void updateUserPass()
        {
            Tuple<string, string> selected = getSelectedItem();

            if (selected != null)
            {
                tbUsername.Text = selected.Item1;
                tbPassword.Text = selected.Item2;
            }
        }


        // Add Account Function.
        private void addAccount()
        {
            if (validUsername(tbUsername.Text))
            {
                lStatus.Text = tbUsername.Text + " has been Saved!";

                account.addAccount(tbUsername.Text.Trim(), tbPassword.Text);

                clearAll();

                if (account.Accounts.Count > 0)
                {
                    lbAccounts.TopIndex = 0;
                }
            }
        }

        // Remove Account Function.
        private void removeAccount()
        {
            Tuple<string, string> selected = getSelectedItem();
            if (selected != null)
            {
                //spawn dialog to confirm
                DialogResult userResponse = MessageBox.Show("Are you sure you want to remove " + selected.Item1 + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (userResponse == DialogResult.Yes)
                {
                    lStatus.Text = selected.Item1 + " has been Removed!";
                    account.removeAccount(selected.Item1);
                    clearAll();
                }
            }

        }


        // Clear all Text Boxes.
        private void clearAll()
        {
            lbAccounts.ClearSelected();
            tbUsername.Text = "";
            tbPassword.Text = "";
        }


        // Checks if the Email Address is VALID.
        // Thanks to http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        private bool validEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Checks if the Username (or email in this case) is valid.
        private bool validUsername(string Username)
        {
            if (!validEmail(Username.Trim()))
            {
                MessageBox.Show("Invalid E-Mail Address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        // Checks if the Password is valid.
        private bool validPassword(string Password)
        {
            if (Password.Length < 6)
            {
                MessageBox.Show("Invalid Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }


        // Start Maple Function!!! Pretty much just does some last second checks before calling launchMaple(). Also updates label (:
        private async Task<bool> startMaple()
        {
            bStartMaple.Enabled = false;
            tbUsername.Enabled = false;
            tbPassword.Enabled = false;

            lStatus.Text = "Logging In..";

            if (!validUsername(tbUsername.Text) || !validPassword(tbPassword.Text)) // Small Validation/Checks.
            {
                bStartMaple.Enabled = true;
                tbUsername.Enabled = true;
                tbPassword.Enabled = true;

                lStatus.Text = "Ready!";

                return false;
            }

            string Username = tbUsername.Text.Trim();
            string Password = tbPassword.Text;

            if (await Misc.launchMaple(Username, Password, cbNXLauncher.Checked)) // Launch Maple.
            {
                lStatus.Text = "Launched!";

                if (account.containsUsername(Username))
                {
                    account.moveToTop(Username);
                    lbAccounts.SetSelected(0, true);
                }
            }

            else
            {
                lStatus.Text = "Failed to Launch MapleStory!"; // Uh Oh.
            }

            tbUsername.Enabled = true;
            tbPassword.Enabled = true;
            bStartMaple.Enabled = true;

            return true;
        }


        // Delete Key -> Remove Account.
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                removeAccount();
            }
        }

        // Enter Key -> Start Maple. E A S E O F U S E 
        private async void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await startMaple();
            }
        }

        // Unhides the Password Text.
        private void cbPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = !cbPassword.Checked;
        }

        // Start MapleStory button. Thread, Task or purely Async..
        private async void bStartMaple_Click(object sender, EventArgs e)
        {
            await startMaple();
        }

        // Add Account Button.
        private void bAddAccount_Click(object sender, EventArgs e)
        {
            addAccount();
        }

        // Remove Account Button.
        private void bRemoveUser_Click(object sender, EventArgs e)
        {
            removeAccount();
        }

        // Gets Selected Index (Account/User).
        private void lbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateUserPass();
        }

        // Makes sure you save those accounts fam.
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveAccounts();
        }
    }
}
