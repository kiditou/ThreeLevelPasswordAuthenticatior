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
        public static User ReadFile(string filePath)
        {
            string[] fileContents = null;
            if (File.Exists(filePath))
            {
                fileContents = File.ReadAllLines(filePath);
            }

            string[] currentLine = null;
            string optionTitle;
            string optionContent;
            if (fileContents != null)
            {
                for (int l = 0; l < fileContents.Length; l++)
                {
                    currentLine = fileContents[l].Split(new string[] { " = " },
                                                        StringSplitOptions.RemoveEmptyEntries);

                    optionTitle = currentLine[0];
                    optionContent = currentLine[1];

                    if (currentLine.Length != 0)
                    {
                        switch (optionTitle)
                        {
                            case "Name":
                                if (optionContent.Length != 0)
                                {
                                }
                                break;
                        }
                    }
                }
            }
            return new User();
        }
    }
}