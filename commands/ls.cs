// The output of this application needs to be trimmed
// Changing this to a switch might make it worthwhile.
using System;
using System.IO;
using System.Collections.Generic;
class Ls
{
  static void Main(string[] args)
  {
  
    if (args.Length > 0)
    {
      string path = args[0];
      IEnumerable<string> folders = Directory.EnumerateDirectories(path);
      string[] files = Directory.GetFiles(path);
      
      foreach (string item in folders)
      {

        Console.Write(item + "  ");
      }
      foreach (string item in files)
      {
        Console.Write(item + "  ");
      }
      Console.WriteLine(" ");
    }
    else
    {
      string[] files =  Directory.GetFiles(Directory.GetCurrentDirectory());
      string[] folders = Directory.GetDirectories(Directory.GetCurrentDirectory());
      foreach(string folder in folders)
      {
        Console.WriteLine(folder);
      }
      foreach (string file in files)
      {
        Console.WriteLine(file);
      }
      Console.WriteLine(" ");
    }
  }
}
