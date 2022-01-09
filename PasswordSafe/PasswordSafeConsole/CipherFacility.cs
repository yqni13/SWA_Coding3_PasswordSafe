using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PasswordSafeConsole
{
    internal class CipherFacility
    {
        private string masterPw;
        private int encoderMethod;
        EncodingMethodsCollection encoder = new EncodingMethodsCollection();

        public CipherFacility() { }

        public CipherFacility(string masterPw)
        {
            this.masterPw = masterPw;

            /// check if config file commits correct format for chosen encoder method 
            try
            {
                encoderMethod = int.Parse(ConfigurationManager.AppSettings["encoder"]);                
            }
            catch (System.FormatException)
            {
                Console.WriteLine("\n\nERROR. INVALID CONFIG FILE INPUT.\nCheck key-value pair for 'encoder'!\n");                
                Environment.Exit(0);            /// end program or else en/decryption will fail and destroy file
            }
            
        }

        public string Decrypt(byte[] crypted)
        {
            var key = GetKey(this.masterPw);
            
            /// choose encoding method by int from config file [task#4 from instructions]
            switch (encoderMethod)
            {
                case (0):                    
                    return encoder.DecryptionAES(key, crypted);                 
                case (1):                    
                    return encoder.Decryption3DES(key, crypted);
                /*case (2):
                    //decrypt with new method from EncodingMethodsCollection
                    break;*/
                default:                    
                    return encoder.DecryptionAES(key, crypted);
            }

        }

        public byte[] Encrypt(string plain)
        {
            var key = GetKey(this.masterPw);
            
            /// choose encoding method by int from config file [task#4 from instructions]
            switch (encoderMethod)
            {
                case (0):
                    return encoder.EncryptionAES(key, plain);
                case (1):
                    return encoder.Encryption3DES(key, plain);
                /*case (2):
                    //encrypt with new method from EncodingMethodsCollection
                    break;*/
                default:
                    Console.WriteLine("\nInvalid config input 'encoder'.\nDefault encryption method chosen.\n");
                    return encoder.EncryptionAES(key, plain);
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