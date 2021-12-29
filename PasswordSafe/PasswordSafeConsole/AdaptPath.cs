using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeConsole
{
    internal class AdaptPath
    {
        public AdaptPath() { }

        public string AddPath(string defaultPath)
        {
            while(true)
            {
                Console.WriteLine("Would you like to select a customized path for pw? [Y/n]");
                var condition = Console.ReadLine().ToUpper();
                
                if (condition == "Y")
                {
                    Console.WriteLine("Please enter the path:");                   
                    var path = Console.ReadLine();
                    if(!CheckPathExisting(path))
                    {
                        Directory.CreateDirectory(path);                       
                    }
                    
                    return path;
                }
                else if (condition == "N")
                {
                    Console.WriteLine("Default path was chosen.");
                    break;
                }
                else
                {
                    Console.WriteLine("Input incorrect.");                  
                }
            }

            return defaultPath;
        }

        public bool CheckPathExisting(string path)
        {
            return Directory.Exists(path) ? true : false ;
        }
    }
}
