using System;
using System.Collections.Generic;
using System.IO;

namespace Blsh
{
  public class BuiltIns
  {
    public delegate Session myDelegate<Session> (Session session); 
    public static Dictionary<string, myDelegate<Session>> builtins = new Dictionary<string, myDelegate<Session>>
    //This previous line took me waaaay to long to figure out.  I tried using Reflections, and also straight up declaring and instantiating my own Delegate before I found the Action delegate. 
    {
      {"clear", clear},
      {"pwd", pwd},
      {"whoami",whoami}
    };

    public static Session runBuiltIns(Session thisSession)
    {
      string command = thisSession.GetCommand();
      string args = thisSession.GetArgs();
    
      Session returnSession = builtins[command](thisSession);
      return returnSession;
      return thisSession;
    }
    public static Session clear(Session thisSession)
    {
      Console.Clear();
      return thisSession;
    }
    // public static string help()
    // {
    //
    // }
    // public static string cd(string[] args)
    // {
    //
    // }
    public static Session whoami(Session thisSession)
    {
      Console.WriteLine("{0} on {1}", Environment.UserName, Environment.MachineName );
      return thisSession;
    }
    public static Session pwd(Session thisSession)
    {
      Console.WriteLine(thisSession.GetPath());
      return thisSession;
    }
  } 
}
