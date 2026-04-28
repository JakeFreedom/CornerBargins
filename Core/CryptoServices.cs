using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Core
{
    public class CryptoServices
    {

        public static string EncryptString(string stringToEncrypt, byte[] key, byte[] iv) {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(stringToEncrypt);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string DeCryptString()
        {
            return string.Empty;
        }

        public static (byte[] key, byte[] iv) GenerateKeyAndIv()
        {
            using (Aes aes = Aes.Create())
            {
                return (aes.Key, aes.IV);
            }
        }

        public static string HashString(string stringToHash)
        {
            string hash = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(stringToHash)));
            System.Diagnostics.Debug.WriteLine(hash);
            return hash;
        }

    }
}
