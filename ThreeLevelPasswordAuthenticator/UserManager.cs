using System;

using System.IO;

namespace kdt.ThreeLevelPasswordAuthenticator.UserManagement
{
    public class UserManager
    {
        public static User ReadFromFile(string filePath)
        {
            string[] fileContents = null;

            if (File.Exists(filePath))
            {
                fileContents = File.ReadAllLines(filePath);
            }

            User user = new User();

            string[] currentLine;
            string[] lineSeparator = new string[] { " = " };
            StringSplitOptions splitOption = StringSplitOptions.RemoveEmptyEntries;

            string currentTitle = null;
            string currentContent = null;

            if (fileContents != null)
            {
                for (int l = 0; l < fileContents.Length; l++)
                {
                    currentLine = fileContents[l].Split(lineSeparator, splitOption);

                    if (currentLine.Length != 0)
                    {
                        currentTitle = currentLine[0];
                        currentContent = currentLine[1];

                        switch (currentTitle)
                        {
                            case "Name":
                                user.Name = currentContent;
                                break;

                            case "Passphrase":
                                user.Passphrase = currentContent;
                                break;

                            case "Passpword":
                                user.Password = currentContent;
                                break;

                            case "Username":
                                user.Username = currentContent;
                                break;
                        }
                    }
                }
            }

            return user;
        }

        public static void WriteToFile(User user)
        {
            string filePath = string.Format(@"Users\{0}.usr", user.Username);

            WriteToFile(filePath, user);
        }

        public static void WriteToFile(string filePath, User user)
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

            File.WriteAllLines(filePath, fileContents);
        }

        // TODO: Add AppendToFile() method.
    }
}