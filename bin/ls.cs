using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class ls
{
    static int Main(string[] args)
    {
        List<string> newArgs = new List<string>();
        if (args.Length ==0  || args[0].StartsWith("-"))
        {
            newArgs.Add(Directory.GetCurrentDirectory());
        } 
        foreach (string arg in args)
        {
            newArgs.Add(arg);
        }
        
        if(Directory.Exists(newArgs[0]) == false)//maybe make the following try-catch
        {
            Console.WriteLine("No such directory exists");
            // Environment.Exit(1);
            return 1;
        }
        else 
        {
            List<KeyValuePair<int, string>> contents = new List<KeyValuePair<int, string>>();
            contents = getContents(newArgs[0]); 
            if (newArgs.Count == 1)
            {
                newArgs.Add("default");
            }
            foreach (string arg in newArgs.Skip(1))
            {
                switch (newArgs[1])
                {
                    case "-a":
                    output(contents);
                    return 0;
                    case "-d":
                    output(dirsOnly(contents));
                    return 0;
                    case "default":
                    output(hideDots(contents));
                    return 0;
                }
            } 
        }
        return 0;
    }
    //--------------------grab all files in dir------------------------//
    public static List<KeyValuePair<int,string>> getContents(string path)
    {
        List<KeyValuePair<int, string>> contents = new List<KeyValuePair<int,string>>();
        IEnumerable<string> dirs = Directory.EnumerateDirectories(path); 
        string[] files = Directory.GetFiles(path);
        foreach (string item in dirs)
        {
            contents.Add(new KeyValuePair<int,string>(0, item));
        }
        foreach (string item in files)
        {
            contents.Add(new KeyValuePair<int,string>(1, item));
        }
        return contents;
    }
//----------------------Default behavior - hide hidden---------------//
//not the best solution, but works for now
    public static List<KeyValuePair<int, string>> hideDots(List<KeyValuePair<int, string>> contents)
    {   
        List<KeyValuePair<int,string>> notRemoved = new List<KeyValuePair<int,string>>();
        foreach (KeyValuePair<int, string> item in contents)
        {
            if (item.Value.Substring(item.Value.LastIndexOf("\\")+1).StartsWith("."))
            {
                continue;
            }
            else
            {
                notRemoved.Add(new KeyValuePair<int,string>(item.Key, item.Value ));
            }
        }
    return notRemoved;
    }    
//----------------------Hide Files----------------------------//

    public static List<KeyValuePair<int,string>> dirsOnly(List<KeyValuePair<int,string>> contents)
    {
        List<KeyValuePair<int, string>> dirs = new List<KeyValuePair<int, string>>();
        foreach (KeyValuePair<int,string> item in contents)
        {
            if (item.Key == 0)
                dirs.Add(new KeyValuePair<int,string>(item.Key, item.Value));
        }
        return dirs;
    }
//----------------------output function ----------------------//
    public static void output(List<KeyValuePair<int,string>> contents)
    {
        var longest = contents.Aggregate((max, cur)=>max.Value.Length > cur.Value.Length ? max:cur);
        int winWidth = Console.WindowWidth;
        Console.WriteLine(winWidth/longest.Value.Length);
        string output = null;
        foreach (KeyValuePair<int,string> item in contents)
        {
            if (item.Key == 0)
            {
                Console.Write(item.Value.Substring(item.Value.LastIndexOf("\\")) + "\t");
            }
            if (item.Key == 1)
            {
                Console.Write(item.Value.Substring(item.Value.LastIndexOf("\\")+1) + "\t");
            }
        }
    }
}