using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newb_Launcher
{
    [Serializable]

    public class Account
    {
        public List<Tuple<string, string>> Accounts;
        public BindingList<string> Usernames;

        public Account()
        {
            Accounts = new List<Tuple<string, string>>();
            Usernames = new BindingList<string>();
        }


        // Get Account Function.
        public Tuple<string, string> getAccount(string Username)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Username.ToLower().Equals(Accounts.ElementAt(i).Item1.ToLower()))
                {
                    return Accounts.ElementAt(i);
                }
            }

            return null;
        }

        // Add Account to List Function.
        public void addAccount(string Username, string Password)
        {
            if (containsUsername(Username))
            {
                removeAccount(Username);
            }

            Accounts.Insert(0, new Tuple<string, string>(Username, Password));
            Usernames.Insert(0, Username);

        }

        // Remote Account from List Function.
        public void removeAccount(string Username)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Username.ToLower().Equals(Accounts.ElementAt(i).Item1.ToLower()))
                {
                    Accounts.RemoveAt(i);
                    Usernames.RemoveAt(i);

                    break;
                }
            }
        }


        // Checks for username's already saved in the Account List.
        public bool containsUsername(string Username)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Username.ToLower().Equals(Accounts.ElementAt(i).Item1.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }


        // Moves new Accounts to the Top.
        public void moveToTop(string Username)
        {
            if (containsUsername(Username))
            {
                Tuple<string, string> Account = getAccount(Username);

                removeAccount(Username);

                Accounts.Insert(0, Account);
                Usernames.Insert(0, Account.Item1);
            }
        }
    }
}
