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
        Session.AddCommand(command);//add to history
        Session methodReturn = DoesItExist(command, newSession);
        newSession.SetPath(methodReturn.GetPath());
      } while (command != "exit");

    }


    public static Session DoesItExist(string input, Session currentSession)
    {  
      // Can this section be written more elegantly?
      string[] comm = input.Split(" ");           //split shell input into an array
      currentSession.SetCommand(comm[0]);                //take first array item and treat it as executable
      string[] noExe  = comm.Skip(1).ToArray();     //create new array without executable
      currentSession.SetArgs(string.Join(" ", noExe));   //and join it to string b/c ProcessStartInfo() only takes strings as arguments
      string commandsDir = @"Users/brian/code/blsp/bin/";
      string external = currentSession.GetCommand() + ".exe"; // change later to handle binaries native to MacOS
      /// reall, fix this garbage Brian.
      if (input == "exit")
      {
        Console.WriteLine("Exiting...");
      }
      else if (BuiltIns.builtins.ContainsKey(currentSession.GetCommand()))
      {
        Console.WriteLine(currentSession.GetCommand());
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
