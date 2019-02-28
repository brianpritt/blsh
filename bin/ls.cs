// The output of this application needs to be trimmed
// Changing this to a switch might make it worthwhile.

//make default to skip files that start with "."

using System;
using System.IO;
using System.Collections.Generic;
class Ls
{
  static void Main(string[] args)
  {
    if (args.Length == 0)
    {
      string path = Directory.GetCurrentDirectory();
      string[] files =  Directory.GetFiles(Directory.GetCurrentDirectory());
      IEnumerable<string> folders = Directory.EnumerateDirectories(path);
      foreach(string folder in folders)
      {

        Console.WriteLine(folder.Substring(folder.LastIndexOf("/")+1));
      }
      foreach (string file in files)
      {
        Console.WriteLine(file.Substring(file.LastIndexOf("/")+1));
      }
      Console.WriteLine(" ");
    }
    else
    {
      string path = args[0];
      IEnumerable<string> folders = Directory.EnumerateDirectories(path);
      string[] files = Directory.GetFiles(path);
      
      foreach (string item in folders)
      {

        Console.WriteLine(item.Substring(item.LastIndexOf("/")+1));
      }
      foreach (string item in files)
      {
        Console.WriteLine(item.Substring(item.LastIndexOf("/")+1));
      }
      Console.WriteLine(" ");
    }
  }
  // public static string[] GetLS(string path)
  // {

  // }
}
//OPTIONS to add:
//-a show all files
//--author
//-C columns
//