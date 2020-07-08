using System.Collections.Generic;
using System.IO;

namespace Gmail_Force
{
    class IO
    {
        public static List<string> Passwords = new List<string>();

        public static bool Exist(string FilePath)
        {
            if(File.Exists(FilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Read(string FilePath)
        {
            if(Exist(FilePath))
            {
                foreach(string l in File.ReadLines(FilePath))
                {
                    Passwords.Add(l);
                }
            }
        }
    }
}
