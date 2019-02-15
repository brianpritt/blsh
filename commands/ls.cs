// args
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
      string[] contents =  Directory.GetFiles(Directory.GetCurrentDirectory());
      foreach (string item in contents)
      {
        Console.WriteLine(item);

      }
      Console.WriteLine(" ");
    }
  }
}
