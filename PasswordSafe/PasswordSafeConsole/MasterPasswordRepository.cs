using System.IO;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string masterPasswordPath;

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
        }

        internal bool MasterPasswordIsEqualTo(string masterPath, string masterPwToCompare)
        {
            masterPath = masterPath + "/master.pw";
            /*
            if(!File.Exists(this.masterPasswordPath))
            {
                return false;
            }
            CipherFacility decoder = new CipherFacility(0, "");

            var masterPw = decoder.Decrypt(File.ReadAllBytes(this.masterPasswordPath));
            return masterPwToCompare == masterPw;
            */

            return File.Exists(masterPath) && masterPwToCompare == File.ReadAllText(masterPath);

            /*
            return File.Exists(this.masterPasswordPath) && 
                masterPwToCompare == File.ReadAllText(this.masterPasswordPath);
            */
        }

        internal void SetMasterPassword(string masterPw)
        {
            /*
            CipherFacility encoder = new CipherFacility(1, masterPw);
            
            File.WriteAllBytes(this.masterPasswordPath, encoder.Encrypt(masterPw));
            */
            AdaptPath pathfinder = new AdaptPath();

            File.WriteAllText(pathfinder.AddPath(this.masterPasswordPath) + "/master.pw", masterPw);
            //File.WriteAllText(this.masterPasswordPath, masterPw);
        }
    }
}