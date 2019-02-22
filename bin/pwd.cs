using System.IO;
using System;

class Pwd
{
  static void Main()
  {
    var direct = Directory.GetCurrentDirectory();
    Console.WriteLine(direct);
  }
}
