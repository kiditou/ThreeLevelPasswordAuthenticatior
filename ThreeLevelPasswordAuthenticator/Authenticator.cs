using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using kdt.ThreeLevelPasswordAuthenticator.UserManagement;

namespace kdt.ThreeLevelPasswordAuthenticator.Authentication
{
    public static class Authenticator
    {
        public static ValidationToken AuthenticatePassword(User user)
        {
            ValidationToken vt = new ValidationToken();
            vt.ValidationType = ValidationType.Password;

            string userFilePath = string.Format("Users\\{0}.usr", user.Username);

            User registeredUser = UserManager.ReadFromFile(userFilePath);

            int result = string.Compare(registeredUser.Password, user.Password);

            if (result < 0 || result > 0)
            {
                vt.Result = Token.NotMatched;
            }
            else if (result == 0)
            {
                vt.Result = Token.Matched;
            }
            return vt;
        }

        public static ValidationToken AuthenticatePassphrase(User user)
        {
            ValidationToken vt = new ValidationToken();
            vt.ValidationType = ValidationType.Password;

            string userFilePath = string.Format("Users\\{0}.usr", user.Username);

            User registeredUser = UserManager.ReadFromFile(userFilePath);

            int result = string.Compare(registeredUser.Passphrase, user.Passphrase);

            if (result < 0 || result > 0)
            {
                vt.Result = Token.NotMatched;
            }
            else if (result == 0)
            {
                vt.Result = Token.Matched;
            }
            return vt;
        }

        public static ValidationToken UserExistence(User user)
        {
            ValidationToken vt = new ValidationToken();
            vt.ValidationType = ValidationType.Existence;

            string userFilePath = string.Format("Users\\{0}.usr", user.Username);

            if (File.Exists(userFilePath))
            {
                vt.Result = Token.Existing;
            }
            else
            {
                vt.Result = Token.NonExisting;
            }

            return vt;
        }

        public static ValidationToken Captcha(string input)
        {
            ValidationToken vt = new ValidationToken();
            vt.ValidationType = ValidationType.Captcha;

            if (input == "0q54ax") // HACK: Create a randomized set of captcha images and affix random values.
            {
                vt.Result = Token.Matched;
            }
            else
            {
                vt.Result = Token.NotMatched;
            }

            return vt;
        }
    }
}