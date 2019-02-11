// static class for setting up shell environment at startup
//Look for default path, bin path (compiles list)

// the ini file should just have list of builtin commands just like bash. so setting default path will look like "cd /path/to/home"
//same with command folders, use export
using System;
using System.IO;

namespace Blsh
{
    public class Initialize 
    {
        public static string iniFile = "blsp.ini";
        public static string[] iniContents = File.ReadAllLines(iniFile);
        
        public string path = null;

        public static void Ini()
        {
            if (File.Exists(iniFile))
        {
            
            foreach (string item in iniContents)
            {
                Console.WriteLine(item);
            }
        } else
        {
            Console.WriteLine("blsp.ini is not present.");
        }
        }
        // public string GetPath()
        // { 

        // }
        // public string GetBin()
        // {

        // }
        // public string GetUser()
        // {

        // }
        // public string GetMachine()
        // {

        // }
        
    }
} 
