using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordSafeConsole.Interfaces
{
    interface IEncoding
    {
        byte[] Encryption(byte[] key, string plain);


        string Decryption(byte[] key, byte[] crypted);
    }
}
