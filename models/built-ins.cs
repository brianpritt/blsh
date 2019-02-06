//mehtods that need to be finished: help, cd, for-do, history, jobs, dirs, echo, and lots of others that build the scripting language portion of the shell.
using System;
using System.Collections.Generic;
using System.IO;

namespace Blsh
{
  public class BuiltIns
  {
    public delegate Session myDelegate<Session> (Session session, string args); 
    public static Dictionary<string, myDelegate<Session>> builtins = new Dictionary<string, myDelegate<Session>>
    //I wanted to call a method from an input string. It took me waaaay to long to figure out.  I tried using Reflections, but generic delegates are the way to go. myDelegate passed into the Dictionary will use the string as a method and pass along with it a Session object.  
    {
      {"clear", clear},
      {"pwd", pwd},
      {"whoami",whoami}
    };

    public static Session runBuiltIns(Session thisSession, string com, string args)
    {
    
      Session returnSession = builtins[com](thisSession, args);
      return returnSession;
      return thisSession;
    }
    public static Session clear(Session thisSession, string args)
    {
      Console.Clear();
      return thisSession;
    }
    public static Session whoami(Session thisSession, string args)
    {
      //the Session object actually has properites for this. but whatever.  I'm waiting for the init method class to be built to figure out where this will get assigned. For now. Just be happy that it's here.
      Console.WriteLine("{0} on {1}", Environment.UserName, Environment.MachineName);
      return thisSession;
    }
    public static Session pwd(Session thisSession, string args)
    {
      Console.WriteLine(thisSession.GetPath());
      return thisSession;
    }
  } 
}
