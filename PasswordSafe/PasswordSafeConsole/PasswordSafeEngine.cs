using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    internal class PasswordSafeEngine
    {
        private string path;
        private CipherFacility cipherFacility;

        public PasswordSafeEngine(string path, CipherFacility cipherFacility)
        {
            this.path = path;
            this.cipherFacility = cipherFacility;
        }

        internal IEnumerable<string> GetStoredPasswords()
        { 
            if (!Directory.Exists(this.path))
            {
                return Enumerable.Empty<string>();
            }

            /// return list of all file names assembled in dir
            return Directory.GetFiles(this.path).ToList().
                Select(f => Path.GetFileName(f)).
                Where(f => f.EndsWith(".pw")).
                Select(f => f.Split(".")[0]);
        }

        internal string GetPassword(string passwordName)
        {
            byte[] password = File.ReadAllBytes(Path.Combine(this.path, $"{passwordName}.pw"));
            return this.cipherFacility.Decrypt(password);
        }

        internal void AddNewPassword(PasswordInfo passwordInfo)
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            File.WriteAllBytes(
                Path.Combine(this.path, $"{passwordInfo.PasswordName}.pw"),
                this.cipherFacility.Encrypt(passwordInfo.Password));
        }

        internal void DeletePassword(string passwordName)
        {
            if(!Directory.Exists(this.path) || !File.Exists($"{this.path}/{passwordName}.pw"))
            {
                Console.WriteLine("Password not found.");
                return;
            }
            File.Delete(Path.Combine(this.path, $"{passwordName}.pw"));
        }
 
    }
}