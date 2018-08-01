using System;
using System.Threading;

namespace Charlie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from charlie.");
            new Thread(DoMainWork) { IsBackground = true, Name = "Die Hard with a Vengeance" }.Start();
            ProtectProcess.DieHard.IAmJohnMcClane();            
        }

        static void DoMainWork()
        {
            using (var handle = new EventWaitHandle(false, EventResetMode.AutoReset, "Live Free or Die Hard"))
            {
                if (handle.WaitOne())
                {
                    Console.WriteLine("Welcome to the party, pal.");
                }
            }
        }
    }
}
