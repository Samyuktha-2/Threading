using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadStaticTopic
{
    class Program
    {
        [ThreadStatic]
        static int counter = 10; 
        static void Main(string[] args)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {counter}"); 
            new Thread(Work).Start();
            new Thread(Work).Start();
            counter++;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {counter}"); 
            Console.ReadLine();
        } 
        static void Work()
        {
            counter++;
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {counter}");
        }
    }
}
