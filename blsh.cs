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
      Console.WriteLine(Initialize.checkIni());
      Initialize newInit = new Initialize();
      Session newSession = new Session (newInit.GetPath(), newInit.GetBinaries());  
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
        Promulgate(newSession, command, args); //Magic
      } while (input != "exit");

    }

    public static void Promulgate(Session currentSession, string command, string args)
    {  
      string external = command + ".exe"; // works for MacOS native without .exe. when init class is finished we can get rid of this.

      if (command == "exit")
      {
        Console.WriteLine("Exiting...");
      }
      else if (BuiltIns.builtins.ContainsKey(command))
      {
        BuiltIns.runBuiltIns(currentSession, command, args);  
      }
      else
      {
        try
        {
          Process process = new Process();
         
          process.StartInfo = new ProcessStartInfo(currentSession.GetBin() + external, args );
          // {

          //   // UseShellExectute = false
          // };
          
          process.StartInfo.UseShellExecute = false;
          process.Start();
          process.WaitForExit();

  
        }
        catch (Win32Exception w)
        {
          Console.WriteLine("General failure reading drive.");
        }
      }
    }
    
  }
}
