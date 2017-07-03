using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;

namespace ThreeLevelPasswordAuthenticator
{
    public class Cipher
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
    }
}