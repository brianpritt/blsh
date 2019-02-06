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
    public static void Main()
    {

      string command = null;
      Console.Clear();
      Session newSession = new Session ("/");  
      Directory.SetCurrentDirectory(newSession.GetPath());

      do
      {
        Console.Write(newSession.GetPath() + " $ ");
        command = Console.ReadLine();

        string[] splitCommand = command.Split(" ");
        newSession.SetCommand(splitCommand[0]);
        string[] args = splitCommand.Skip(1).ToArray();
        newSession.SetArgs(string.Join(" ", args));
        Session.AddCommand(command);//add to history

        Session methodReturn = Promulgate(newSession); //Magic

        newSession.SetPath(methodReturn.GetPath());
      } while (command != "exit");

    }


    public static Session Promulgate(Session currentSession)
    {  
      string commandsDir = @"Users/brian/code/blsp/bin/";//this will be changed later when the init function is finished.
      
      string external = currentSession.GetCommand() + ".exe"; // works for MacOS native without .exe. when init class is finished we can get rid of this.

      if (currentSession.GetCommand() == "exit")
      {
        Console.WriteLine("Exiting...");
      }
      else if (BuiltIns.builtins.ContainsKey(currentSession.GetCommand()))
      {
        // Runs currentSession through builtins and comes back with returnToMain, SHOULD be the same object
        Session returnToMain = BuiltIns.runBuiltIns(currentSession);
        Console.WriteLine(returnToMain.GetCommand());
        return returnToMain;
      }
      else
      {
        try
        {
          var process = new Process();
          process.StartInfo = new ProcessStartInfo(commandsDir + external, currentSession.GetArgs() );
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
