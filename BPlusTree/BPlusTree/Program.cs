using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BPlusTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory;
            do{
                directory = getDirectory();
            }while(directory != "-1");

            
        }
        public static string getDirectory(){
            Console.WriteLine("Please enter the directory of the files you wish to put into a Bplus tree (ex: C:/Users/John/Documents/Files): ");
            string filePath = Console.ReadLine();
            if (Directory.Exists(filePath) && Directory.GetFiles(filePath).Length != 0)
            {
                return filePath;
            }
            else if (!Directory.Exists(filePath))
            {
                Console.WriteLine("There is no directory at " + filePath);
                return "-1";
            }
            else
            {
                Console.WriteLine("There are no files in the directory " + filePath);
                return "-1";
            }
        }
    }
}
