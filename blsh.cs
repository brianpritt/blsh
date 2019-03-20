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
    public static string ver = "0.001";
    public static string license = "GPLv3";
    public static string authors = "Brian Pritt";
    public static string input = null;
    public static void Main()
    {
      // string input = "";
      string capture = null;
      Console.Clear();
      Initialize.checkIni();
      Initialize newInit = new Initialize();
      newInit.ConfigEnv();

      Session newSession = new Session (newInit.GetPath(), newInit.GetBinaries());  
      Directory.SetCurrentDirectory(newSession.GetPath());
      ConsoleKey key;
      int iteration=0;//use to cycle through past commands
      do
      {
        Console.Write(newSession.GetPath() + " $ ");
        do{
          
          key = Console.ReadKey().Key;
          if (key == ConsoleKey.UpArrow)
            {
              
              string history = upCommand(newSession, iteration);
              Console.Write(history);
              iteration++;
            }
            else if (key == ConsoleKey.Enter)
            {
              break;
            }
            else
            {
                input = (input + key.ToString()).ToLower();
            }
            Console.SetCursorPosition(Console.CursorLeft-(input.Length-1), Console.CursorTop);
            Console.Write(input);
        } while (key != ConsoleKey.Enter);
        if(key == ConsoleKey.UpArrow)
        {
                    Console.WriteLine("uparrow");

        }
        // if (Console.ReadKey().Key == ConsoleKey.UpArrow)
        // {
        //   Console.WriteLine(key);
        // }
        // else {

        
        // input = Console.ReadLine();
        // if (Console.ReadKey().Key == ConsoleKey.UpArrow)
        // {
        //   Console.WriteLine("History");
        // }
        Console.WriteLine();
        string[] splitCommand = input.Split(" ");

        string command = splitCommand[0];
        string[] argsArray = splitCommand.Skip(1).ToArray();
        string args = string.Join(" ", argsArray);

        Session.AddCommand(input);//add to history
        Promulgate(newSession, command, args); //Magic
        // input = null;  
      } while (input != "exit");
      

    }

    public static void Promulgate(Session currentSession, string command, string args)
    {  
      string external = command +".exe"; // works for MacOS native without .exe. when init class is finished we can get rid of this.
      int exit;
      if (command == "exit")
      {
        Console.WriteLine("Exiting...");
      }
      else if (BuiltIns.builtins.ContainsKey(command))
      {
       int code = BuiltIns.runBuiltIns(currentSession, command, args);  
      }
      else
      {
        try
        {
          Process process = new Process();
         
          process.StartInfo = new ProcessStartInfo(currentSession.GetBin() + external, args );
          // process.StartInfo.UseShe+llExecute = false;
          process.Start();
          process.WaitForExit();  
          
          // exit = process.ExitCode;
        }
        catch (Win32Exception w)
        {
          Console.WriteLine(command + ": does not exist in this context");
        }
      }
      input = null;
    }
    public static string upCommand(Session currentSession, int iteration)
    {
      if (iteration >= 0)
      {
      return "";
      }
      else
      {
        return currentSession.GetCommands()[iteration];
      }
    }
  }
}
