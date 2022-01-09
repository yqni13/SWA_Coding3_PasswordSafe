using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeConsole
{
    internal class CheckPwEquality
    {
        public CheckPwEquality() { }

        
        public bool CompareNewPwd(string originPwd)
        {

            /// check every new password for equality [task#2 from instructions]
            Console.WriteLine("Enter password again to compare");
            var comparePwd = Console.ReadLine();

            if(originPwd != comparePwd)
            {
                Console.WriteLine("Password did not match. Password NOT added.");
                return false;
            }

            return true;
        }
    }
}
