//mehtods that need to be finished: help, cd, for-do, history, jobs, dirs, echo, and lots of others that build the scripting language portion of the shell.
using System;
using System.Collections.Generic;
using System.IO;

namespace Blsh
{
  public class BuiltIns
  {
    public delegate void myDelegate (Session session, string args); 
    public static Dictionary<string, myDelegate> builtins = new Dictionary<string, myDelegate>
    //I wanted to call a method from an input string. It took me waaaay to long to figure out.  I tried using Reflections, but generic delegates are the way to go. myDelegate passed into the Dictionary will use the string as a method and pass along with it a Session object.  
    {
      {"clear", clear},
      {"pwd", pwd},
      {"whoami",whoami},
      {"cd",cd}
    };

    public static void runBuiltIns(Session thisSession, string com, string args)
    {
      builtins[com](thisSession, args);
    }

    ///Built-ins:
    //took out returns, need to change back so built-ins can give exit codes.  Delegate might have to change to do that.
    public static void clear(Session thisSession, string args)
    {
      Console.Clear();
    }
    public static void whoami(Session thisSession, string args)
    {
      //the Session object actually has properites for this. but whatever.  I'm waiting for the init method class to be built to figure out where this will get assigned. For now. Just be happy that it's here.
      Console.WriteLine("{0} on {1}", Environment.UserName, Environment.MachineName);
    }
    public static void pwd(Session thisSession, string args)
    {
      Console.WriteLine(thisSession.GetPath());
    }
    public static void cd(Session thisSession, string args)
    {
      if (args.Length == 0){
       Console.WriteLine();
      }
      else {  
        try
        {
          Directory.SetCurrentDirectory(args);
          thisSession.SetPath(Directory.GetCurrentDirectory());
         
        }
        catch (DirectoryNotFoundException dirx)
        {
          Console.WriteLine(dirx.Message);
         
        }
      }
    }
  } 
}
