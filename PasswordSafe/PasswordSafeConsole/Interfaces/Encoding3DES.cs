using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordSafeConsole.Interfaces
{
    class Encoding3DES : IEncoding
    {
        public string Decryption(byte[] key, byte[] crypted)
        {
            var TripleDES = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var decryptor = TripleDES.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(crypted, 0, crypted.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public byte[] Encryption(byte[] key, string plain)
        {
            var TripleDES = new TripleDESCryptoServiceProvider
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var plainText = Encoding.UTF8.GetBytes(plain);
            var encryptor = TripleDES.CreateEncryptor();
            return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
        }
    }
}
