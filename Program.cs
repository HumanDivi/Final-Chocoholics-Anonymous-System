using System;

namespace Chocoholics_Anonymous
{
    class Program
    {
        static void Main(string[] args)
        {
            CA_System.System system = new CA_System.System();

            if (args.Length != 0)
                system.runAutoTask(args);
            else
                system.launchTerminal();

            Environment.Exit(0);
        }
    }
}