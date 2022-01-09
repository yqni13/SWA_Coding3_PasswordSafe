using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PasswordSafeConsole
{
    internal class CipherFacility
    {
        private string _masterPw;
        private int _encoderMethod;
        ///EncodingMethodsCollection encoder = new EncodingMethodsCollection();

        public CipherFacility() { }

        public CipherFacility(string masterPw)
        {
            this._masterPw = masterPw;

            // Check if config file commits correct format for chosen encoder method. 
            try
            {
                _encoderMethod = int.Parse(ConfigurationManager.AppSettings["encoder"]);                
            }
            catch (System.FormatException)
            {
                Console.WriteLine("\n\nERROR. INVALID CONFIG FILE INPUT.\nCheck key-value pair for 'encoder'!\n");

                // End program or else en/decryption will fail and destroy file.
                Environment.Exit(0);            
            }
            
        }

        public string Decrypt(byte[] crypted)
        {
            var key = GetKey(this._masterPw);
            
            // Choose encoding method by int from config file [task#4 from instructions].
            switch (_encoderMethod)
            {
                case (0):                    
                    return EncodingMethodsCollection.DecryptionAES(key, crypted);                 
                case (1):                    
                    return EncodingMethodsCollection.Decryption3DES(key, crypted);
                // case (2):
                    // Decrypt with new method from EncodingMethodsCollection [PLACEHOLDER].
                    // break;
                default:                    
                    return EncodingMethodsCollection.DecryptionAES(key, crypted);
            }

        }

        public byte[] Encrypt(string plain)
        {
            var key = GetKey(this._masterPw);
            
            // Choose encoding method by int from config file [task#4 from instructions].
            switch (_encoderMethod)
            {
                case (0):
                    return EncodingMethodsCollection.EncryptionAES(key, plain);
                case (1):
                    return EncodingMethodsCollection.Encryption3DES(key, plain);
                // case (2):
                    // Encrypt with new method from EncodingMethodsCollection [PLACEHOLDER].
                    // break;
                default:
                    Console.WriteLine("\nInvalid config input 'encoder'.\nDefault encryption method chosen.\n");
                    return EncodingMethodsCollection.EncryptionAES(key, plain);
            }

        }

        public byte[] GetKey(string password)
        {
            var keyBytes = Encoding.UTF8.GetBytes(password);
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(keyBytes);
            }
        }

    }
}