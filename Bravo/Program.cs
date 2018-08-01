using System;
using System.Threading;

namespace Bravo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from bravo.");
            ProtectProcess.DieHard.DeploySgtAlPowell("alpha");
            do
            {
                Thread.Sleep(500000);
            } while (true);
        }
    }
}
