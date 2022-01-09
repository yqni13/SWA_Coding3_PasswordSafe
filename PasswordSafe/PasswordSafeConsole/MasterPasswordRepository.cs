using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string masterPasswordPath;
        static string masterPwdName = ConfigurationManager.AppSettings["name_masterPassword"];
        EncodingMethodsCollection encoder = new EncodingMethodsCollection();
        CipherFacility cipher = new CipherFacility();
        

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
        }

        internal bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            
            masterPasswordPath = masterPasswordPath + "/" + masterPwdName;
            var hash = File.ReadAllBytes(this.masterPasswordPath);

            /// get hash value from console and master password to compare [task#1 from instructions]
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(masterPwToCompare);            //convert string into byte[]
            var hashCompare = new MD5CryptoServiceProvider().ComputeHash(tmpSource);    //compute MD5 hash (size of 128bits)

            var i = 0;
            if((File.Exists(this.masterPasswordPath)) && (hash.Length == hashCompare.Length))
            {
                while((i < hashCompare.Length) && (hashCompare[i] == hash[i]))
                {
                    ++i;
                }
            }
            return i == hashCompare.Length;            
        }

        internal void SetMasterPassword(string masterPw)
        {
            CheckDirectoryExists();

            /// hash master password to disable reading in plaintext [task#1 from instructions]
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(masterPw);              //convert string into byte[]
            var hash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);    //compute MD5 hash (size of 128bits)
            
            /// master password is set to location chosen via config file [task#3 from instructions]
            File.WriteAllBytes(this.masterPasswordPath + "/" + masterPwdName, hash);            
        }

        internal void CheckDirectoryExists()
        {
            if (!Directory.Exists(this.masterPasswordPath))
            {
                Directory.CreateDirectory(this.masterPasswordPath);
            }
        }

    }
}