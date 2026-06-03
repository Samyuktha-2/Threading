using System;
using System.Threading;

namespace SemaphoreConcept
{
    class Program
    {
        static Semaphore sem = new Semaphore(2, 2);
        static void Work()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} waiting");
            Thread.Sleep(500);
            sem.WaitOne();

            try
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} has entered");
                Thread.Sleep(3000);
            }
            finally
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} leaving");
                sem.Release();
            }
        }
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
            {
                new Thread(Work).Start();
            }

            Console.ReadLine();
        }
    }
}
