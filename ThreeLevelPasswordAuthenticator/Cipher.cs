using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace kdt.ThreeLevelPasswordAuthenticator.Security
{
    public class Cipher // TODO: Document the class and its methods.
    {
        /// <summary>
        /// Encrypts an array of data using a Key and IV.
        /// </summary>
        /// <param name="clearData">Data to be encrypted.</param>
        /// <param name="key">Key used for the symmetric algorithm.</param>
        /// <param name="IV">Initialization vector for the symmetric algorithm.</param>
        /// <returns>Returns an array of encrypted bytes.</returns>
        public static byte[] Encrypt(byte[] clearData, byte[] key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();

            return ms.ToArray();
        }

        public static byte[] Encrypt(byte[] clearData, string password)
        {
            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static string EncryptString(string clearString, string password)
        {
            byte[] clearData = Encoding.Unicode.GetBytes(clearString);

            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            byte[] encryptedData = Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);
        }

        public static void EncryptFile(string fileIn, string fileOut, string password)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);

            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            CryptoStream cs = new CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write);

            int bufferLength = 4096;
            byte[] buffer = new byte[bufferLength];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLength);

                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            fsIn.Close();
            cs.Close();
        }

        /// <summary>
        /// Encrypts an array of data using a Key and IV.
        /// </summary>
        /// <param name="encryptedData">Data to be encrypted.</param>
        /// <param name="key">Key used for the symmetric algorithm.</param>
        /// <param name="IV">Initialization vector for the symmetric algorithm.</param>
        /// <returns>Returns an array of encrypted bytes.</returns>
        public static byte[] Decrypt(byte[] encryptedData, byte[] key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(encryptedData, 0, encryptedData.Length);
            cs.Close();

            return ms.ToArray();
        }

        public static byte[] Decrypt(byte[] encryptedData, string password)
        {
            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            return Decrypt(encryptedData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static string DecryptString(string encryptedString, string password)
        {
            byte[] encryptedData = Convert.FromBase64String(encryptedString);

            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            byte[] clearData = Decrypt(encryptedData, pdb.GetBytes(32), pdb.GetBytes(16));

            return Encoding.Unicode.GetString(clearData);
        }

        public static void DecryptFile(string fileIn, string fileOut, string password)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);

            byte[] rgbSalt = Encoding.Unicode.GetBytes("The quick brown fox jumps over the lazy dog.");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, rgbSalt);

            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            CryptoStream cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write);

            int bufferLength = 4096;
            byte[] buffer = new byte[bufferLength];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLength);

                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            fsIn.Close();
            cs.Close();
        }
    }
}