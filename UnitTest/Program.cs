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
            string clearString = "The quick brown fox jumps over the lazy dog.";
            string encryptedString = Cipher.EncryptString(clearString, "AyyLmaoXD");

            Console.WriteLine("Clear Text:\t{0}", clearString);
            Console.WriteLine("Encrypted Text:\t{0}", encryptedString);
        }
    }
}