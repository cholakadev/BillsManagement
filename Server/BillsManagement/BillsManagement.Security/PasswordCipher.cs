using BillsManagement.Utility;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BillsManagement.Security
{
    public static class PasswordCipher
    {
        public static string Encode(EncryptCriteria criteria)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(criteria.Secret);
            sb.Append(criteria.Password);
            sb.Append(criteria.Email);

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(sb.ToString())));
        }

        private static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(GlobalConstants.strPermutation,
            new byte[] { GlobalConstants.bytePermutation1,
                         GlobalConstants.bytePermutation2,
                         GlobalConstants.bytePermutation3,
                         GlobalConstants.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        private static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(GlobalConstants.strPermutation,
            new byte[] { GlobalConstants.bytePermutation1,
                         GlobalConstants.bytePermutation2,
                         GlobalConstants.bytePermutation3,
                         GlobalConstants.bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
