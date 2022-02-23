using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringConsoleApp
{
    class ProcessMonitoring
    {
        private string processName;
        private int processLifetime;
        private int processCheckInterval;

        public ProcessMonitoring(string name, string lifeTime, string interval)
        {
            try
            {
                processName = name;
                processLifetime = Convert.ToInt32(lifeTime);
                processCheckInterval = Convert.ToInt32(interval);

                WriteLog("Контроль запущен");
                Console.WriteLine("Контроль запущен");

                ProcessControl();
            }
            catch (Exception ex)
            {
                WriteLog("Некорректные входные данные");
                Console.WriteLine(ex.Message);
            }
        }

        // Запись логов в файл
        private void WriteLog(string text)
        {
            string filePath = @"log.txt";

            try
            {
                using (StreamWriter stream = new StreamWriter(filePath, true))
                {
                    stream.WriteLine($"{processName}: {text} - {DateTime.Now.ToString("G")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Метод контролирующий процесс
        private void ProcessControl()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);

                int time = 0;
                bool control = true;

                while (control == true)
                {
                    if (processes.Length == 0 && time >= processLifetime)
                    {
                        WriteLog("Процесс отсутствует");
                        Console.WriteLine("Процесс отсутствует");

                        WriteLog("Программа завершена по истечению времени");
                        Console.WriteLine("Программа завершена по истечению времени");
                        break;
                    }
                    else if (processes.Length != 0 && time >= processLifetime)
                    {
                        foreach (var p in processes)
                            p.Kill();

                        WriteLog("Процесс завершен по истечению времени");
                        Console.WriteLine("Процесс завершен по истечению времени");
                        break;
                    }

                    Thread.Sleep(60000 * processCheckInterval);
                    time++;
                    processes = Process.GetProcessesByName(processName);
                    WriteLog($"{time} / {processLifetime}");
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Ошибка: {ex.Message}");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                WriteLog("Контроль завершен");
                Console.WriteLine("Контроль завершен");
            }
        }

        // Метод закрывающий приложение
        private void CloseProgram()
        {
            Console.WriteLine("Чтобы завершить программу, нажмите 'Q'");
            string closePro = Console.ReadLine();

            if (closePro.ToUpper() == "Q")
                Environment.Exit(0);
        }
    }
}
