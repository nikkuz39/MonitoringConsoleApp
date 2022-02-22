using System;

namespace MonitoringConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMonitoring monitoring = new ProcessMonitoring(args[0], args[1], args[2]);

            //ProcessMonitoring monitoring = new ProcessMonitoring("mspaint", "5", "1");
        }
    }
}
