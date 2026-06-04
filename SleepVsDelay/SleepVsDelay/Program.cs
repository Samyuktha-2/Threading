using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepVsDelay
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Thread.Sleep");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine("\nTask.Delay");
            RunTask().GetAwaiter().GetResult();

            Console.ReadLine();
        }

        static async Task RunTask()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(2000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
