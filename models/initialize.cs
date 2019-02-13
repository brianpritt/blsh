// static class for setting up shell environment at startup
//Look for default path, bin path (compiles list)

// the ini file should just have list of builtin commands just like bash. so setting default path will look like "cd /path/to/home"
//same with command folders, use export
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Blsh
{
    public class Initialize 
    {
        private string _user;
        private string _machine;
        private string _home;
        private string _binaries;
        private string[] _external;
        // setting up an array at init seems more secure than willy nilly adding apps


        public static string iniFile = "blsp.ini";
        public static string[] iniContents = File.ReadAllLines(iniFile);
        // Regex afterEquls = new Regex()
        public Initialize()
        {
            _user = Environment.UserName;
            _machine = Environment.MachineName;
        }
        public void Setters()
        {
            foreach(string line in iniContents)
            {
                if (line.Contains("PATH"))
                {
                    _home = line.Substring(line.LastIndexOf('=')+1);
                    break;
                }
                if (line.Contains("BIN"))
                {
                    _binaries = line.Substring(line.LastIndexOf('=')+1);
                }
            }
        }
    
        public string GetPath()
        {
            return _home;
        }
        public string GetBinaries()
        {
            return _binaries;
        }
        // public static void Ini()
        // //only run after 
        // {
        //     if (File.Exists(iniFile))
        // {
            
        //     foreach (string item in iniContents)
        //     {
        //         Console.WriteLine(item);
        //     }
        // } else
        // {
        //     Console.WriteLine("blsp.ini is not present.");
        // }
        // }
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

        
        
    }
} 
