using System;
using System.Collections.Generic;
using System.IO;

class touch
{
    public static Dictionary<string,bool> bools = new Dictionary<string,bool>()
    {
        {"-a", false},//access time
        {"-c", false},//do not create
        {"-f", false},//force update
        {"-m", false},//change modification time
        {"-t", false}//use a specific time
    };
    static int Main(string[] args)
    {
        List<string> arguments = new List<string>();
        List<string> filename = new List<string>();
        foreach (string arg in args)
        {
            if (arg.StartsWith("-") )
            {
                if (bools.ContainsKey(arg))
                {
                    bools[arg] = true;
                }
                else{
                    Console.WriteLine("Invalid argument: "+arg);
                    return 1;
                }
            }
            else
            {
                filename.Add(arg);
            }
        }
        foreach (string arg in arguments)
        {
            Console.WriteLine(arg);
        }
        foreach (string file in filename)
        {
            Console.WriteLine(file);
        }
        return 0;
    }
}