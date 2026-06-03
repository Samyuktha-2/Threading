using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexConcept
{
    class Program
    {
        static bool createdNew;
        static Mutex newMutex = new Mutex(true, "MyUniqueApp", out createdNew);
        //static Mutex mutex = new Mutex();
        static void Work()
        {
            //mutex.WaitOne();
            //try
            //{
            //    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            //    Thread.Sleep(3000);
            //}
            //finally
            //{
            //    mutex.ReleaseMutex();
            //}
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Work);
            Thread t2 = new Thread(Work);


           

            try{
                if (!createdNew)
                {
                    Console.WriteLine("Application already running");
                    return;
                }
                Console.WriteLine("Application started");

                Console.WriteLine("To end application press enter");
            }
            finally
            {
                newMutex.ReleaseMutex();
                newMutex.Dispose();
            }

            Console.ReadLine();
        }
    }
}
