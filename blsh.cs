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
      
      string input = null;
      Console.Clear();
      Initialize newInit = new Initialize();
      newInit.theSetter();
      Session newSession = new Session (newInit.GetPath());  
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
      // string commandsDir = @"/Users/brian/code/blsp/bin/";//this will be changed later when the init function is finished.
      string commandsDir = @"\Users\BrianPritt\briCode\blsh\commands\";
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
          Console.WriteLine("in try");
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
    
  }
}
