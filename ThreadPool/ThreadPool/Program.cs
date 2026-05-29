using System;
using System.Threading;

namespace Threadpool
{
    class Program
    {  
        static void Main()
        {
            for(int i = 1; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Work,i);
            }
            Console.ReadLine();
        } 
        static void Work(object i)
        {
            Console.WriteLine($"Running {i} on thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Closed {i} on thread - {Thread.CurrentThread.ManagedThreadId}");
        }
    }
     
}

