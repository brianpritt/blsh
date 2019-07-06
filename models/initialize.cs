// static class for setting up shell environment at startup
//Look for default path, bin path (compiles list)

// the ini file should just have list of builtin commands just like bash. so setting default path will look like "cd /path/to/home"
//same with command folders, use export
//ToDo: add access to environment variables mcuh like bash $PATH or printenv
using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Blsh
{
    public class Initialize 
    {
        private string _user;
        private string _machine;
        private string _home;
        private string[] _binaries;
        private string[] _external;
        private string _arch;
        private string _os;
        private string _homeDrive;
        private int _forgroundColor;
        private string _dirStructure;
        private string[] _test;
        private static Dictionary<string, string> _env = new Dictionary<string, string>{};

        // setting up an array at init seems more secure than willy nilly adding apps
        public static string iniFile = "blsh.ini";
        public static string[] iniContents;
        public Initialize()
        {
            _user = Environment.UserName;
            _machine = Environment.MachineName;
            _home = SetPath("PATH");
            _binaries = SetBinaries("BIN");
            _arch = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            _os = System.Environment.GetEnvironmentVariable("OS");
            _homeDrive = System.Environment.GetEnvironmentVariable("HOMEDRIVE");
            _dirStructure = SetStructure();
        }
        //Add check to see if required variable actually exist, and add a default if they do not
        public string SetPath(string getEnv)
        //was using for both _bin and _path but bin now uses .SetBinaries() may use that one for _path too
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
        public string[] SetBinaries(string getBins)
        {
            string[] theBins = null;
            iniContents = File.ReadAllLines(iniFile);
            foreach(string line in iniContents)
            {
                if(line.Contains(getBins))
                    {
                        string noSpace = line.Replace(" ", "");
                        string ready = noSpace.Substring(noSpace.LastIndexOf('=')+1);
                        theBins = ready.Split(':');
                    }

            }
            return theBins;
        }
        // the following is to replace all code that checks/makes a file
        public static void makeFile(string fileName)
        {
            if(!File.Exists(fileName))
            {
                using(FileStream fs = File.Create(fileName))
                {
                    Byte[] newFl = new UTF8Encoding(true).GetBytes(""); 
                    fs.Write(newFl,0,newFl.Length);
                    fs.Close();
                    Console.WriteLine(fileName + " does not exist\n Creating...");

                }
            }    
        }
        // Writes basic blsh.ini file contents if file had to be created.
        //Would be great to make this more concise.
        //wrwritten 7/5/2019
        
        public static void checkIni()
        {
            string path = @"blsh.ini";
            string vars = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true)
            { 
                vars = "PATH = C:\\Users\\"+Environment.UserName+"\\" + Environment.NewLine + "BIN = C:\\Users\\BrianPritt\\briCode\\blsh\\bin\\";
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) == true)
            {
                vars = "PATH = /Users/brian/code/blsh/" + Environment.NewLine + "BIN = /Users/brian/code/blsh/bin/";
            }
            //Add a statement for Linux here
            string [] contents = File.ReadAllLines(path);
            if (contents.Length == 0)
            {
                File.WriteAllText(@path, vars);
            };
        }

        public string SetStructure()
        // checks to see if OS uses '/' or '\' for Directory seperator, since Environment.GetEnvironmentVariable("OS") does not work on MacOS 
        // currently no check working specifically for gnu/Linux
        // Works, but also... garbage
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
        public string[] GetBinaries()
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
