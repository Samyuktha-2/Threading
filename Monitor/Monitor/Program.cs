using System;
using System.Collections.Generic;
using System.Threading;

namespace MonitorSP
{
    class Program
    {
        static Queue<int> queue = new Queue<int>();
        static object myLock = new object();

        static void Producer()
        {
            for (int i = 0; i < 5; i++)
            {
                bool acquired = false;
                try
                {
                    Monitor.TryEnter(myLock, 2000, ref acquired);

                    if (acquired)
                    {
                        queue.Enqueue(i);
                        Console.WriteLine($"Produced: {i}");
                        Monitor.Pulse(myLock);
                    }
                    else
                    {
                        Console.WriteLine("Producer TimedOut");
                    }
                }
                finally
                {
                    if (acquired)
                    {
                        Monitor.Exit(myLock);
                    }
                }

                Thread.Sleep(1000);

                Monitor.Enter(myLock);
                try
                {
                    Console.WriteLine("Producer Completed");
                    Monitor.PulseAll(myLock);
                }
                finally
                {
                    Monitor.Exit(myLock);
                }
            }
        }

        static void Consumer()
        {
            while (true)
            {
                Monitor.Enter(myLock);

                try
                {
                    if (queue.Count == 0)
                    {
                        Console.WriteLine("Consumer Waiting.....");
                        Monitor.Wait(myLock);
                    }

                    var item = queue.Dequeue();
                    Console.WriteLine($"Consumed: {item}");
                }
                finally
                {
                    Monitor.Exit(myLock);
                }
                Thread.Sleep(1500);
            }
        }
        static void Main(string[] args)
        {
            Thread p1 = new Thread(Producer);
            Thread c1 = new Thread(Consumer);
            p1.Start();
            c1.Start();
            p1.Join();
            c1.Join();
            Console.WriteLine("Finished");

            TimeOut timeout = new TimeOut();
            Thread thread = new Thread(timeout.RunTimeout);
            Thread thread2 = new Thread(timeout.RunTimeout);
            thread.Start();
            thread2.Start();

            thread.Join();
            thread2.Join();
            Console.ReadLine();
        }
    }
    class TimeOut
    {
        public TimeOut() { }

        static object myLock = new object();
        public void RunTimeout()
        {
            bool acquired = false;
            try
            {
                Monitor.TryEnter(myLock, 2000, ref acquired);
                if (acquired)
                {
                    Thread.Sleep(3500);
                    Console.WriteLine("Acquired..");
                }
                else
                {
                    Console.WriteLine("Timed Out.....");
                }
            }
            finally
            {
                if (acquired)
                {
                    Monitor.Exit(myLock);
                }
            }
        }
    }

}
//Consumer -> Queue Empty? -> Wait() 
//Producer -> Add Item -> Pulse() 
//Consumer Wakes -> Consumes Item






