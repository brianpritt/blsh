// static class for setting up shell environment at startup
//Look for default path, bin path (compiles list)

// the ini file should just have list of builtin commands just like bash. so setting default path will look like "cd /path/to/home"
//same with command folders, use export
// TODO: add comma seperated values for multiple BIN locations
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
        private int _forgroundColor;
        private string _dirStructure;

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
            _dirStructure = SetStructure();
        }
        //Add check to see if required variable actually exist, and add a default if they do not
        public string SetEnv(string getEnv)
        {
            iniContents = File.ReadAllLines(iniFile);
            foreach(string line in iniContents)
            {
                if(line.Contains(getEnv))
                {
                    string noSpace = line.Replace(" ", "");
                    return noSpace.Substring(noSpace.LastIndexOf('=')+1);
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
            catch (FileNotFoundException err)
            {
                string path = @"blsh.ini";
                string vars = null;
                Console.WriteLine(err.Message);
                Console.WriteLine("Creating...");
                char os = Path.DirectorySeparatorChar;

                if ( os == '\\')
                { 
                    vars = "PATH = C:\\Users\\"+Environment.UserName+"\\" + Environment.NewLine + "BIN = C:\\Users\\BrianPritt\\briCode\\blsh\\bin\\";
                }
                if (os == '/')
                {
                    vars = "PATH = /Users/brian/code/blsh/" + Environment.NewLine + "BIN = /Users/brian/code/blsh/bin/";
                }
                using(FileStream fs = File.Create(path))
                {
                    Byte[] fle = new UTF8Encoding(true).GetBytes(vars);
                    fs.Write(fle,0,fle.Length);
                    
                };
            }
        }

        public string SetStructure()
        // checks to see if OS uses '/' or '\' for Directory seperator, since Environment.GetEnvironmentVariable("OS") does not work on MacOS
        {
            char style = Path.DirectorySeparatorChar;
            if (style == '\\')
            {
                return "win";
            }
            if (style == '/')
            {
                return "nix";
            }
            return "nix";
        }
        public string GetStructure()
        {
            return _dirStructure;
        }
        public string GetPath()
        {
            return _home;
        }
        public string GetBinaries()
        {
            return _binaries;
        }
        public int GetForegroundColor()
        {
            return _forgroundColor;
        }
        public void ConfigEnv()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        
        // public string GetBin()
        // {

        // }
        // public string GetUser()
        // {
        // }
        // public string GetMachine()
        // {
        // }
        // public string GetPath()
        // { 
        // }


    }
} 
