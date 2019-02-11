using System;

namespace Blsh
{
    public class shell
    {
        private string _usr = null;
        private string _machine = null;
        private string _path = null;
        private string _binPath = null;

        public static Init()
        {

        }
        public static Run()
        {
            string input = null;
            Console.Clear();
            do{
                Console.Write("=>$");
                input = Console.ReadLine();

            } while (input != "exit");
        }
        public static builtInLookUp(string[] args)
        {
            if (BuiltIns.methods.ContiansKey(args[0]))
            {

            }
            else
            {
                Promulgate()
            }
        }
        public static Promulgate(string[] args)
        {

        }
        public static void Main()
        {
            Init();
            Run();
        }
    }
}