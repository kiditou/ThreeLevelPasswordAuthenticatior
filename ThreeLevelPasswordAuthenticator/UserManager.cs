using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace kdt.ThreeLevelPasswordAuthenticator.UserManagement
{
    internal class UserManager
    {
        public static User ReadFromFile(string filePath)
        {
            string[] fileContents = null;
            if (File.Exists(filePath))
            {
                fileContents = File.ReadAllLines(filePath);
            }

            string[] currentLine = null;
            string optionTitle = null;
            string optionContent = null;
            StringSplitOptions splitOption = StringSplitOptions.RemoveEmptyEntries;
            string[] lineSeparator = new string[] { " = " };

            User user = new User();

            if (fileContents != null)
            {
                for (int l = 0; l < fileContents.Length; l++)
                {
                    currentLine = fileContents[l].Split(lineSeparator, splitOption);

                    optionTitle = currentLine[0];
                    optionContent = currentLine[1];

                    if (currentLine.Length != 0)
                    {
                        switch (optionTitle)
                        {
                            case "Name":
                                if (optionContent.Length != 0)
                                {
                                    user.Name = optionContent;
                                }
                                else
                                {
                                    user.Name = null;
                                }
                                break;

                            case "Passphrase":
                                if (optionContent.Length != 0)
                                {
                                    user.Passphrase = optionContent;
                                }
                                else
                                {
                                    user.Passphrase = null;
                                }
                                break;

                            case "Passpword":
                                if (optionContent.Length != 0)
                                {
                                    user.Password = optionContent;
                                }
                                else
                                {
                                    user.Password = null;
                                }
                                break;

                            case "Username":
                                if (optionContent.Length != 0)
                                {
                                    user.Username = optionContent;
                                }
                                else
                                {
                                    user.Username = null;
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                user.Name = null;
                user.Passphrase = null;
                user.Password = null;
                user.Username = null;
            }

            return new User();
        }

        public static void WriteToFile(User user)
        {
            string dateNow = DateTime.Now.ToShortDateString();
            string timeNow = DateTime.Now.ToShortTimeString();

            string[] fileContents = new string[]
            {
                string.Format("# User configuration for {0}.",user.Username),
                string.Format("# Last updated on {0}, {0}", dateNow, timeNow),
                "",
                string.Format("Name = {0}", user.Name),
                string.Format("Passphrase = {0}", user.Passphrase),
                string.Format("Password = {0}", user.Password),
                string.Format("Username = {0}", user.Username)
            };

            string file = string.Format(@"Users\{0}.usr", user.Username);

            File.Create(file);
            File.WriteAllLines(file, fileContents);
        }

        // TODO: Add AppendToFile() method.
    }
}