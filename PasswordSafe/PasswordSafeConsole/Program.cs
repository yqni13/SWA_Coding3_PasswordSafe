using System.Configuration;
using System;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    public class Program
    {
        // Loading settings from configuration (xml) file.
        private static string _masterPwdPath = ConfigurationManager.AppSettings["path_masterPassword"];        
        private static string _regularPwdPath = ConfigurationManager.AppSettings["path_regularPasswords"];
        
        private static MasterPasswordRepository _masterRepository = new MasterPasswordRepository(_masterPwdPath);
        private static PasswordSafeEngine _passwordSafeEngine = null;
        

        public static void Main(String[] args)
        {
            Console.WriteLine("Welcome to Passwordsafe");
            
            bool abort = false;
            bool unlocked = false;
            while (!abort) 
            {
                Console.WriteLine("Enter master (1), show all (2), show single (3), add (4), delete(5), set new master (6), Abort (0)");
                int input = 0;
                if (!int.TryParse(Console.ReadLine(), out input))
                { 
                    input = -1;
                }
                switch (input) 
                {
                     case 0: 
                     {
                        abort = true;
                        break;
                     }
                     case 1: 
                     {                        
                        Console.WriteLine("Enter master password");
                        String masterPw = Console.ReadLine();
                        unlocked = _masterRepository.MasterPasswordIsEqualTo(masterPw);
                        if (unlocked) 
                        {
                            // Regular passwords are created in location chosen via config file [task#3 from instructions].
                            _passwordSafeEngine = new PasswordSafeEngine(_regularPwdPath, new CipherFacility(masterPw));
                            Console.WriteLine("unlocked"); 
                        } else
                        {
                            Console.WriteLine("master password did not match ! Failed to unlock.");
                        }
                        break;
                     }
                     case 2: 
                     {
                        if (unlocked)
                        {
                            _passwordSafeEngine.GetStoredPasswords().ToList().ForEach(pw=>Console.WriteLine(pw));
                        }
                        else
                        {
                            Console.WriteLine("Please unlock first by entering the master password.");
                        }
                        break;
                    }
                    case 3: {
                        if (unlocked)
                        {
                            Console.WriteLine("Enter password name");
                            String passwordName = Console.ReadLine();
                            Console.WriteLine(_passwordSafeEngine.GetPassword(passwordName));
                        }
                        else
                        {
                            Console.WriteLine("Please unlock first by entering the master password.");
                        }
                        break;
                    }
                    case 4: 
                    {
                        if (unlocked)
                        {
                            Console.WriteLine("Enter new name of password");
                            String passwordName = Console.ReadLine();                            
                            
                            Console.WriteLine("Enter password");
                            var password = Console.ReadLine();

                            if (!CheckPwEquality.CompareNewPwd(password))
                                break;

                            _passwordSafeEngine.AddNewPassword(new PasswordInfo(password, passwordName));                           
                        }
                        else
                        {
                            Console.WriteLine("Please unlock first by entering the master password.");
                        }
                        break;
                    }
                    case 5: 
                    {
                        if (unlocked)
                        {
                            Console.WriteLine("Enter password name");
                            String passwordName = Console.ReadLine();
                            _passwordSafeEngine.DeletePassword(passwordName);
                        }
                        else
                        {
                            Console.WriteLine("Please unlock first by entering the master password.");
                        }
                        break;
                    }
                    case 6:
                    {
                        unlocked = false;
                        _passwordSafeEngine = null;
                        Console.WriteLine("Enter new master password ! (Warning you will loose all already stored passwords)");
                        string masterPw = Console.ReadLine();

                        if (!CheckPwEquality.CompareNewPwd(masterPw))
                            break;

                        _masterRepository.SetMasterPassword(masterPw);

                        // Urgent hotfix delete old passwords after changing the master.
                        if (Directory.Exists(_regularPwdPath))
                        {
                            Directory.Delete(_regularPwdPath, true);
                        }

                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
                       
                }
            }

            Console.WriteLine("Good bye !");
        }
    }
}
