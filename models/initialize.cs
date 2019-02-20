// static class for setting up shell environment at startup
//Look for default path, bin path (compiles list)

// the ini file should just have list of builtin commands just like bash. so setting default path will look like "cd /path/to/home"
//same with command folders, use export
using System;
using System.IO;
using System.Text;
using System.Reflection;
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


        public static string iniFile = "blsh.ini";
        public static string[] iniContents;
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
            iniContents = File.ReadAllLines(iniFile);
            foreach(string line in iniContents)
            {
                if(line.Contains(getEnv))
                {
                    return line.Substring(line.LastIndexOf('=')+1);
                }
            }
            return null;
        }
        // check if there is an ini file, create one if not // move default to root later maybe?
        public static void checkIni()
        {
            try 
            {
                File.ReadLines("blsh.ini");
            }
            catch (System.IO.FileNotFoundException err)
            {
                Console.WriteLine(err.Message);
                Console.WriteLine("Creating...");

                string path = @"blsh.ini"; 
                string vars = "PATH =/Users/brian/code/blsp/" + Environment.NewLine + "BIN =/Users/brian/code/blsp/bin";
                using(FileStream fs = File.Create(path))
                {
                    Byte[] fle = new UTF8Encoding(true).GetBytes(vars);
                    fs.Write(fle,0,fle.Length);
                    
                };
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
