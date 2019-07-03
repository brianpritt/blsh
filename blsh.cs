// Classes:
// Initialize: Static class for seting up shell environment
// Session: Creates environment variables
// Built-Ins: Built-in shell methods - see class for list
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.ComponentModel;
using System.Text;

namespace Blsh
{
  public class theShell
  {
    public static string ver = "0.001";
    public static string license = "GPLv3";
    public static string authors = "Brian Pritt";
    public static void Main()
    {
      string input = null;
      Console.Clear();
      Initialize.checkIni();//looks for .ini file makes one if does not exist
      Initialize newInit = new Initialize();
      newInit.ConfigEnv();
      Session newSession = new Session (newInit.GetPath(), newInit.GetBinaries());  
      Directory.SetCurrentDirectory(newSession.GetPath());
      ConsoleKeyInfo keyinf;
      do
      {
        Console.Write(newSession.GetPath() + " $ ");
        input = Console.ReadLine();
        
        string[] splitCommand = input.Split(" ");

        string command = splitCommand[0];
        string[] argsArray = splitCommand.Skip(1).ToArray();
        string args = string.Join(" ", argsArray);

        Session.AddCommand(input);//add to history
        Promulgate(newSession, command, args); //Magic
      } while (input != "exit");

    }

    public static void Promulgate(Session currentSession, string command, string args)
    {  
      //only works on exe files right now, need to add support for native MacOS / linux
      string external = command + ".exe" ; 
      int exit;
      if (command == "exit")
      {
        Console.WriteLine("Exiting...");
        EndSession(currentSession);
      }
      else if (BuiltIns.builtins.ContainsKey(command))
      {
       int code = BuiltIns.runBuiltIns(currentSession, command, args);  
      }
      else
      {
        //Cycle through arry of paths and 
        Process process = new Process();
        string[] paths = currentSession.GetBin();
        foreach (string bin in paths)
        {  
          try
          {
            process.StartInfo = new ProcessStartInfo(bin + external, args );
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();  
        
        // exit = process.ExitCode;
          }
          catch (Win32Exception w)
          {
            //figure out how to put exceptions output later, since it is likely going to throw many when it cycles through list
          }
        }
      }
    }
    public static void EndSession(Session currentSession){
      List<string> history = currentSession.GetCommands();
      string hist = @".blsh_history";
      if (File.Exists(hist))
      {
         using (System.IO.StreamWriter file = new System.IO.StreamWriter(hist, true))
         {
           foreach(string line in history)
           {
             file.WriteLine(line);
           }
         }
      }
      // try
      // {
      //   File.ReadLines(".blsh_history");
      //   File.Close();
      // }
      // catch(FileNotFoundException e)
      // {
      //   Console.WriteLine("Creating files nessicary for richer experience");
      //   string path = @".blsh_history";
      //   // only MacOS rn
      //   using(FileStream fs = File.Create(path))
      //   {
      //     Byte[] fle = new UTF8Encoding(true).GetBytes("");
      //     fs.Write(fle,0,fle.Length);
      //     fs.Dispose();
      //   }
      // }
      // using (FileStream fs = File.Open(@".blsh_history", FileMode.Open))
      // {
      //   foreach(string command in history)
      //   {
      //     fs.Write(command);
      //   }
      // }
    }
  }
}
