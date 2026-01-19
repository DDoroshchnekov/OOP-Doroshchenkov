using System;
using System.IO;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string readmePath = "README.md";

            if (File.Exists(readmePath))
            {
                string readmeContent = File.ReadAllText(readmePath);
                Console.WriteLine(readmeContent);
            }
            else
            {
                Console.WriteLine("README.md не знайдено!");
            }
        }
    }
}
