using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.ComponentModel;

namespace Blsh
{
  public class theShell
  {
    private string _path = null;
    private string _binPath = null;
    private string _configPath = null;
     private string _usr = null;
     private string _machine = null;
    public static void Init()
    {
      Initialize.Ini();
      Console.WriteLine(Initialize.iniContents[1]);
    }
    public static void Main()
    {
      
      string input = null;
      Console.Clear();
      Init();
      Session newSession = new Session ("/");  
      Directory.SetCurrentDirectory(newSession.GetPath());

      do
      {
        Console.Write(newSession.GetPath() + " $ ");
        input = Console.ReadLine();

        string[] splitCommand = input.Split(" ");

        string command = splitCommand[0];
        string[] argsArray = splitCommand.Skip(1).ToArray();
        string args = string.Join(" ", argsArray);

        Session.AddCommand(input);//add to history

        Session methodReturn = Promulgate(newSession, command, args); //Magic

        newSession.SetPath(methodReturn.GetPath());
      } while (input != "exit");

    }


    public static Session Promulgate(Session currentSession, string command, string args)
    {  
      string commandsDir = @"/Users/brian/code/blsp/bin/";//this will be changed later when the init function is finished.
      
      string external = command + ".exe"; // works for MacOS native without .exe. when init class is finished we can get rid of this.

      if (command == "exit")
      {
        Console.WriteLine("Exiting...");
      }
      else if (BuiltIns.builtins.ContainsKey(command))
      {
        // Runs currentSession through builtins and comes back with returnToMain, SHOULD be the same object
        Session returnToMain = BuiltIns.runBuiltIns(currentSession, command, args);
        
        return returnToMain;
      }
      else
      {
        try
        {
          var process = new Process();
          process.StartInfo = new ProcessStartInfo(commandsDir + external, args );
          {
            // UseShellExectute = false
          };
          process.Start();
          process.WaitForExit();

          return currentSession;
        }
        catch (Win32Exception w)
        {
          Console.WriteLine();
        }
      }
      return currentSession;
    }
    // public static void Initialize()
  }
}
