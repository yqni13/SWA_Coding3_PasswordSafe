using System.IO;
using System.Configuration;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string masterPasswordPath;
        static string masterPwdName = ConfigurationManager.AppSettings["name_masterPassword"];

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
        }

        internal bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            masterPasswordPath = masterPasswordPath + "/" + masterPwdName;
            return File.Exists(this.masterPasswordPath) && 
                masterPwToCompare == File.ReadAllText(this.masterPasswordPath);          
        }

        internal void SetMasterPassword(string masterPw)
        {
            CheckDirectoryExists();
            File.WriteAllText(this.masterPasswordPath + "/" + masterPwdName, masterPw);            
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