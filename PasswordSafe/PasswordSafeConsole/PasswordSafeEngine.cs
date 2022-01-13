using PasswordSafeConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    internal class PasswordSafeEngine
    {
        private string _path;
        private CipherFacility _cipherFacility;

        public PasswordSafeEngine(string path, CipherFacility cipherFacility)
        {
            this._path = path;
            this._cipherFacility = cipherFacility;
        }

        internal IEnumerable<string> GetStoredPasswords()
        { 
            if (!Directory.Exists(this._path))
            {
                Console.WriteLine("No passwords exist.");
                return Enumerable.Empty<string>();
            }

            // Return list of all file names assembled in dir.
            return Directory.GetFiles(this._path).ToList().
                Select(f => Path.GetFileName(f)).
                Where(f => f.EndsWith(".pw")).
                Select(f => f.Split(".")[0]);
        }

        internal string GetPassword(string passwordName)
        {
            if(!File.Exists($"{_path}/{passwordName}.pw"))
            {
                return "Password does not exist.";
                
            }
            byte[] password = File.ReadAllBytes(Path.Combine(this._path, $"{passwordName}.pw"));
            return this._cipherFacility.Decrypt(password);
        }

        internal void AddNewPassword(PasswordInfo passwordInfo)
        {
            if (!Directory.Exists(this._path))
            {
                Directory.CreateDirectory(this._path);
            }

            File.WriteAllBytes(
                Path.Combine(this._path, $"{passwordInfo.PasswordName}.pw"),
                this._cipherFacility.Encrypt(passwordInfo.Password));
        }

        internal void DeletePassword(string passwordName)
        {
            if(!Directory.Exists(this._path) || !File.Exists($"{this._path}/{passwordName}.pw"))
            {
                Console.WriteLine("Password not found.");
                return;
            }
            File.Delete(Path.Combine(this._path, $"{passwordName}.pw"));
        }
 
    }
}