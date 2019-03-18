using System;
using System.Collections.Generic;


namespace Blsh
{
  public class Session
  {
    private string _path;
    private string _prevPath;
    private string _binPath;
    private string _env;
    // private string _command;
    // private string _args;

    private static Dictionary<string, string> _available = new Dictionary<string, string > {};
    private static List<string> _history = new List<string> {};

    public Session(string path, string bin)
    {
      _path = path;
      _binPath = bin;
      // _available = _available;
      
    }
    public void SetPath(string path)
    {
      _path = path;
    }
    public string GetPath()
    {
      return _path;
    }
    public string GetBin()
    {
      return _binPath;
    }
    // public void SetCommand(string command)
    // {
    //   _command = command;
    // }
    // public string GetCommand()
    // {
    //   return _command;
    // }
    // public void SetArgs(string args)
    // {
    //   _args = args;
    // }
    // public string GetArgs()
    // {
    //   return _args;
    // }
    public void setBinsDict()
    {
      
    }
    public static void AddCommand(string command)
    {
      _history.Add(command);
    }
    public static List<string> GetCommands()
    {
      return _history;
    }
  }
}
