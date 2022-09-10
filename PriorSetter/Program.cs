using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace PriorSetter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fe_a = false;
            bool fe_u = false;
            int ec_a = 0;
            int ec_u = 0;

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                        if (process.PriorityClass != ProcessPriorityClass.Normal && process.PriorityClass != ProcessPriorityClass.Idle && process.PriorityClass != ProcessPriorityClass.BelowNormal)
                        process.PriorityClass = ProcessPriorityClass.Normal;
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    ec_a++;
                    fe_a = true;
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    ec_u++;
                    fe_u = true;
                    Console.WriteLine(e.Message + " - by " + process.ProcessName);
                }
            }
            if (fe_a)
            {
                Console.WriteLine("Исключений отказа в доступе: " + ec_a);
                if (!fe_u)
                     Console.ReadKey();
            }
            if (fe_u)
            {
                Console.WriteLine("Других исключений: " + ec_u);
                Console.ReadKey();
            }
        }
    }
}