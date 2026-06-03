using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventConcept

{
    class Program
    {
        static AutoResetEvent are = new AutoResetEvent(false);

        static void Work()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting");
            are.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is working");
        }
        static void Main(string[] args)
        {
            new Thread(Work).Start();
            are.Set();
            new Thread(Work).Start();
            new Thread(Work).Start();
            new Thread(Work).Start();
            new Thread(Work).Start();

        }
    }
}
