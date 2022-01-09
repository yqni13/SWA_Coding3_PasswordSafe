using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string _masterPasswordPath;
        private readonly string _masterPwdName = ConfigurationManager.AppSettings["name_masterPassword"];        
        

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this._masterPasswordPath = masterPasswordPath;
        }

        internal bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            
            _masterPasswordPath = _masterPasswordPath + "/" + _masterPwdName;
            var hash = File.ReadAllBytes(this._masterPasswordPath);

            // Get hash value from console and master password to compare [task#1 from instructions].
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(masterPwToCompare);            
            var hashCompare = new MD5CryptoServiceProvider().ComputeHash(tmpSource);    

            var i = 0;
            if((File.Exists(this._masterPasswordPath)) && (hash.Length == hashCompare.Length))
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

            // Hash master password to disable reading in plaintext [task#1 from instructions].
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(masterPw);              
            var hash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);    
            
            // Master password is set to location chosen via config file [task#3 from instructions].
            File.WriteAllBytes(this._masterPasswordPath + "/" + _masterPwdName, hash);            
        }

        internal void CheckDirectoryExists()
        {
            if (!Directory.Exists(this._masterPasswordPath))
            {
                Directory.CreateDirectory(this._masterPasswordPath);
            }
        }

    }
}