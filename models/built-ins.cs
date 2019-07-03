//mehtods that need to be finished: help, for-do, jobs, dirs, echo, and lots of others that build the scripting language portion of the shell.
using System;
using System.Collections.Generic;
using System.IO;

namespace Blsh
{
  public class BuiltIns
  {
    public delegate int myDelegate (Session session, string args); 
    public static Dictionary<string, myDelegate> builtins = new Dictionary<string, myDelegate>
    //I wanted to call a method from an input string. It took me waaaay to long to figure out.  I tried using Reflections, but generic delegates are the way to go. myDelegate passed into the Dictionary will use the string as a method and pass along with it a Session object.  
    {
      {"clear", clear},
      {"pwd", pwd},
      {"whoami",whoami},
      {"cd",cd},
      {"history",history},
      {"help",help}
    };

    public static int runBuiltIns(Session thisSession, string com, string args)
    {
      int code = builtins[com](thisSession, args);
      return code;
    }

    ///Built-ins:
    public static int clear(Session thisSession, string args)
    {
      Console.Clear();
      return 0;
    }
    public static int whoami(Session thisSession, string args)
    {
      //the initialize object actually has properites for this. but whatever.  I'm waiting for the init method class to be built to figure out where this will get assigned. For now. Just be happy that it's here.
      Console.WriteLine("{0} on {1}", Environment.UserName, Environment.MachineName);
      return 0;
    }
    public static int pwd(Session thisSession, string args)
    {
      try 
      {
        Console.WriteLine(thisSession.GetPath());
        return 0;
      }
      catch
      {
        return 1;
      }
    }
    public static int cd(Session thisSession, string args)
    {
      if (args.Length == 0){
       Console.WriteLine();
       return 0;
      }
      else {  
        try
        {
          Directory.SetCurrentDirectory(args);
          thisSession.SetPath(Directory.GetCurrentDirectory());
          return 0;
        }
        catch (DirectoryNotFoundException dirx)
        {
          Console.WriteLine(dirx.Message);
          return 1;
        }
      }
    }
    public static int history(Session thisSession, string args)
    {
      List<string> history = thisSession.GetCommands();
      foreach (string item in history)
      {
        Console.WriteLine(item);
      }
      return 0;
    }
    public static int help(Session thisSession, string args)
    {
      Console.WriteLine();
      Console.WriteLine("The Brian Lee Shell - blsh - ver {0} ", theShell.ver);
      Console.WriteLine("Licensed under {0}", theShell.license);
      Console.WriteLine();
      Console.WriteLine("The following commands are available:");
      foreach (KeyValuePair<string, Blsh.BuiltIns.myDelegate> item in BuiltIns.builtins)
      {
        Console.WriteLine(item.Key);
      }
      Console.WriteLine();
      return 0;
    }
  } 
}
