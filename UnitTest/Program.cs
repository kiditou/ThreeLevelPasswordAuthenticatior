using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kdt.ThreeLevelPasswordAuthenticator.Security;

namespace UnitTest
{
    internal class Program
    {
        private static void Main(string[] args)
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