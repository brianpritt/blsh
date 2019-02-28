using System;
using System.IO;
using System.Collections.Generic;
class ls
{
    static void Main(string[] args)
    {
        List<string> newArgs = new List<string>();
        if (args.Length == 0 || args[0].StartsWith("-"))
        {
            newArgs.Add(Directory.GetCurrentDirectory());
        } 
        foreach (string arg in args)
        {
            newArgs.Add(arg);
        }
        foreach (string item in newArgs)
        {
        Console.WriteLine(item);
        }
        if(Directory.Exists(newArgs[0]) == false)
        {
            Console.WriteLine("No such directory exists");
            Environment.Exit(1);
        }
    }
    
}