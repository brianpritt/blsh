using System;
using System.Collections.Generic;
using System.IO;

namespace Blsh
{
  public class BuiltIns
  {
    public delegate Session myDelegate<Session> (Session session); 
    public static Dictionary<string, myDelegate<Session>> builtins = new Dictionary<string, myDelegate<Session>>
    //I wanted to call a method from an input string. It took me waaaay to long to figure out.  I tried using Reflections, but generic delegates are the way to go. myDelegate passed into the Dictionary will use the string as a method and pass along with it a Session object.  
    {
      {"clear", clear},
      {"pwd", pwd},
      {"whoami",whoami}
    };

    public static Session runBuiltIns(Session thisSession)
    {
      string command = thisSession.GetCommand();
      //string args = thisSession.GetArgs();
    
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
    // public static Session cd(Session thisSession)
    // {
      
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
