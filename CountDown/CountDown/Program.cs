using System;
using System.Threading;

namespace CountDown
{
    class Program
    {
        static CountdownEvent cde = new CountdownEvent(4); //Total number of Signal() calls must match the CountdownEvent count.
        static void Work()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting......");
            Thread.Sleep(2000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is working.....");
         
            cde.Signal();
        }
        static void Main(string[] args)
        {
            new Thread(Work).Start();
            new Thread(Work).Start(); 
            new Thread(Work).Start();
            new Thread(Work).Start(); 

            Console.WriteLine("Main thread started");  
            cde.Wait();
            Console.WriteLine("All workds finished");

       
            Console.ReadLine();
        }
    }
}
