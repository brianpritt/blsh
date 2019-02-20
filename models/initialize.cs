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
        private string _arch;
        private string _os;
        private string _homeDrive;

        // setting up an array at init seems more secure than willy nilly adding apps


        public static string iniFile = "blsp.ini";
        public static string[] iniContents = File.ReadAllLines(iniFile);
        public Initialize()
        {
            _user = Environment.UserName;
            _machine = Environment.MachineName;
            _home = SetEnv("PATH");
            _binaries = SetEnv("BIN");
            _arch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            _os = System.Environment.GetEnvironmentVariable("OS");
            _homeDrive = System.Environment.GetEnvironmentVariable("HOMEDRIVE");
        }
        public string SetEnv(string getEnv)
        {
            foreach(string line in iniContents)
            {
                if(line.Contains(getEnv))
                {
                    return line.Substring(line.LastIndexOf('=')+1);
                }
            }
            return null;
        }
        public static bool checkIni()
        {
            if (File.Exists(iniFile))
            {
                return true;
            }
            // else if(!(File.Exists(iniFile)))
            // {

            // }
            return false;
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
