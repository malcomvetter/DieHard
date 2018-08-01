using System;
using System.Threading;

namespace Alpha
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from alpha.");
            ProtectProcess.DieHard.DeploySgtAlPowell("bravo");
            do
            {
                Thread.Sleep(500000);
            } while (true);
        }
    }
}
