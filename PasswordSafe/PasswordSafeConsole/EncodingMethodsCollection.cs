using System.Security.Cryptography;
using System.Text;

namespace PasswordSafeConsole
{
    internal class EncodingMethodsCollection
    {

        public byte[] EncryptionAES(byte[] key, string plain)
        {
            using (var aes = Aes.Create())                              
            using (var encryptor = aes.CreateEncryptor(key, key))       
            {
                var plainText = Encoding.UTF8.GetBytes(plain);
                return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
            }
        }

        public string DecryptionAES(byte[] key, byte[] crypted)
        {
            using (var aes = Aes.Create())
            using (var decryptor = aes.CreateDecryptor(key, key))
            {
                var decryptedBytes = decryptor.TransformFinalBlock(crypted, 0, crypted.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        public byte[] Encryption3DES(byte[] key, string plain)
        {
            var TripleDES = new TripleDESCryptoServiceProvider();
            TripleDES.Key = key;
            TripleDES.Mode = CipherMode.ECB;
            TripleDES.Padding = PaddingMode.PKCS7;
            var plainText = Encoding.UTF8.GetBytes(plain);
            var encryptor = TripleDES.CreateEncryptor();
            return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
        }

        public string Decryption3DES(byte[] key, byte[] crypted)
        {
            var TripleDES = new TripleDESCryptoServiceProvider();
            TripleDES.Key = key;
            TripleDES.Mode = CipherMode.ECB;
            TripleDES.Padding = PaddingMode.PKCS7;
            var decryptor = TripleDES.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(crypted, 0, crypted.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

    }
}
