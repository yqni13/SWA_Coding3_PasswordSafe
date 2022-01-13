using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordSafeConsole.Interfaces
    
{
    class EncodingAES : IEncoding
    {

        public string Decryption(byte[] key, byte[] crypted)
        {
            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(key, key);
            {
                var decryptedBytes = decryptor.TransformFinalBlock(crypted, 0, crypted.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }


        public byte[] Encryption(byte[] key, string plain)
        {
            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(key, key);
            {
                var plainText = Encoding.UTF8.GetBytes(plain);
                return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
            }           
        }
    }
}
