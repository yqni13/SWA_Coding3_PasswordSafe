using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeConsole
{
    internal static class CheckPwEquality
    {        
        public static bool CompareNewPwd(string originPwd)
        {

            // Check every new password for equality [task#2 from instructions].
            Console.WriteLine("Enter password again to compare");

            if(originPwd != Console.ReadLine())
            {
                Console.WriteLine("Password did not match. Password NOT added.");
                return false;
            }

            return true;
        }
    }
}
