using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProtectProcess
{
    public static class DieHard
    {
        private static List<int> MonitoredPIDs = new List<int>();
        private const string _threadName = "Now I have a machine gun, ho ho ho";
        private static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private static int _sleep = 100;
        private const int _max = 2;

        public static void DeploySgtAlPowell(string processName)
        {
            var self = Process.GetCurrentProcess();
            do
            {
                Console.WriteLine("{0} ({1}) searching for processes named {2}", self.ProcessName, self.Id, processName);
                var targets = Process.GetProcessesByName(processName);
                foreach (var target in targets)
                {
                    if (MonitoredPIDs.Contains(target.Id))
                    {
                        continue;
                    }
                    new Thread(Nakatomi) { IsBackground = true, Name = _threadName }.Start(target);
                    _waitHandle.WaitOne();
                    MonitoredPIDs.Add(target.Id);
                }
                Thread.Sleep(_sleep);
            }
            while (true);
        }

        public static void IAmJohnMcClane()
        {
            Console.WriteLine("Was always kinda partial to Roy Rogers actually.");
            var self = Process.GetCurrentProcess();
            do
            {
                var processes = new Process[0];
                try
                {
                    using (var mutex = new Mutex(false, "Die Hard with a Vengeance"))
                    {
                        if (mutex.WaitOne())
                        {
                            processes = Process.GetProcessesByName(self.ProcessName);
                        }
                    }
                } catch (AbandonedMutexException) { }
                if (processes.Count() == 1)
                {
                    Console.WriteLine("Die Hard " + DateTime.Now);
                    var exec = self.MainModule.FileName;
                    new Thread(Spawn) { IsBackground = true, Name = _threadName }.Start(exec);
                    Console.WriteLine("Yippee-ki-yay!");
                }
                if (processes.Count() > _max)
                {
                    try
                    {
                        using (var mutex = new Mutex(false, "Die Hard with a Vengeance"))
                        {
                            if (mutex.WaitOne(TimeSpan.FromSeconds(2), false))
                            {
                                Console.WriteLine("Too many instances, dying.");
                                Process.GetCurrentProcess().Kill();
                            }
                        }
                    } catch (AbandonedMutexException) { }
                }
                if (processes.Count() > 1)
                {
                    Console.WriteLine("Die Harder " + DateTime.Now);
                    foreach (var process in processes)
                    {
                        if (process.Id != self.Id && !MonitoredPIDs.Contains(process.Id))
                        {
                            new Thread(Nakatomi) { IsBackground = true, Name = _threadName }.Start(process);
                            MonitoredPIDs.Add(process.Id);
                        }
                    }
                }
                Thread.Sleep(_sleep);
            } while (true);
        }

        private static void StripPermissions()
        {
            new Thread(Permissions.Strip) { IsBackground = true, Name = _threadName }.Start();
        }

        static void Nakatomi(object process)
        {
            try
            {
                Console.WriteLine(_threadName);
                var self = Process.GetCurrentProcess();
                var proc = (Process)process;
                var exec = proc.MainModule.FileName;
                Console.WriteLine("{0} ({1}) watching for {2} ({3}) to die", self.ProcessName, self.Id, proc.ProcessName, proc.Id);
                proc.WaitForExit();
                Spawn(exec);
                _waitHandle.Set();
            } catch { }
        }

        static void Spawn (object exec)
        {
            Spawn(exec as string);
        }

        static void Spawn (string exec)
        {
            Console.WriteLine("Hey Roy, how you feeling?");
            try
            {
                using (var mutex = new Mutex(false, "Die Hard with a Vengeance"))
                {
                    if (!mutex.WaitOne(TimeSpan.FromSeconds(2), false))
                    {
                        return;
                    }
                    else
                    {
                        var processes = Process.GetProcessesByName(exec);                        
                        new Thread(WMI.Run) { IsBackground = true, Name = _threadName }.Start(exec);
                        Thread.Sleep(_sleep);
                        Console.WriteLine("Pretty unappreciated, Al.");
                    }
                }
            } catch (AbandonedMutexException) { }
        }
    }
}
