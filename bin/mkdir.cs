using System;
using System.IO;

class Mkdir
{
    public static void Main(string[] args)
    {
        string arg = args[0];
        try
        {
            if (Directory.Exists(arg))
            {
                Console.WriteLine("The path {0} already exitsts.", arg);
            }
            else{
                Directory.CreateDirectory(arg);
                switch (args[1])
                {
                    case "-v":
                    Console.WriteLine("mkdir: {0} created in {1}", args[0], Directory.GetCurrentDirectory());
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
        }
    }
}
// TO ADD:
// -v or --verbose
// -m or --mode to set file mode
// 