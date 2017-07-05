using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using kdt.ThreeLevelPasswordAuthenticator.UserManagement;
using kdt.ThreeLevelPasswordAuthenticator.Security;
using kdt.ThreeLevelPasswordAuthenticator.Authentication;

namespace UnitTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ValditationTest();
        }

        private static void ValditationTest()
        {
            User kiditou = new User("Ron Michael", "kiditou", "endGemePlease", "I4m,y0urF4th3r");
            ValidationToken vt = Authenticator.AuthenticatePassword(kiditou);
            Console.WriteLine(kiditou.Password);
            Console.WriteLine();
            Console.WriteLine("Validation Type: {0}", vt.ValidationType);
            Console.WriteLine("Validation Result: {0}", vt.Result.ToString());
        }

        private static void UserManagerTest()
        {
            User kiditou = new User("Ron Michael", "kiditou", "endGemePlease", "I4m,y0urF4th3r");
            User user = UserManager.ReadFromFile(@"Users\kiditou.usr");
            Console.WriteLine(user.Name);
        }

        private static void EncryptionTestingForFiles()
        {
            string clearFile = @"G:\TestDirectory\kdt.txt";
            string encryptedFile = @"G:\TestDirectory\kdt.txt.cry";

            Cipher.EncryptFile(clearFile, encryptedFile, "kiditouGami161_");
            Cipher.DecryptFile(encryptedFile, @"G:\TestDirectory\decrypted.txt", "kiditouGami161_");
        }

        private static void EncryptDecryptStrings()
        {
            string clearString = "The quick brown fox jumps over the lazy dog.";
            string encryptedString = Cipher.EncryptString(clearString, clearString);

            Console.WriteLine("Clear Text:\t{0}", clearString);
            Console.WriteLine();
            Console.WriteLine("Encrypted Text:\t{0}", encryptedString);
            Console.WriteLine();

            string decryptedString = Cipher.DecryptString(encryptedString, clearString);

            Console.WriteLine("Decrypted Text:\t{0}", decryptedString);
        }
    }
}