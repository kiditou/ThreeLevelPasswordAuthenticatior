using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kdt.ThreeLevelPasswordAuthenticator.UserManagement
{
    public class User
    {
        public string Name { get; set; }
        public string Passphrase { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        // TODO: Add more user details.

        public User()
        {
        }

        public User(string name, string username, string passphrase, string password)
        {
            this.Name = name;
            this.Username = username;
            this.Passphrase = passphrase;
            this.Password = password;
        }
    }
}