using System;
using System.Collections.Generic;
using System.Threading;

namespace ReadWriter
{
    class Program
    {
        static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        static int data = 100;
        static void Reader()
        {
            rw.EnterReadLock();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} data is: {data}");
                Thread.Sleep(2000);
            }
            finally
            {
                rw.ExitReadLock();
            }
        }

        static void Writer()
        {
            rw.EnterWriteLock();
            try
            {
                data++;
                Console.WriteLine($"Writer updated data to {data}");
                Thread.Sleep(2000);
            }
            finally
            {
                rw.ExitWriteLock();
            }
        }

        static Dictionary<int, string> cache = new Dictionary<int, string>();
     
        static void GetData(int id)
        {
            rw.EnterUpgradeableReadLock();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} reading....");
                if (cache.ContainsKey(id))
                {
                    Console.WriteLine($"Found id: {id}");
                }
                else
                {
                    Console.WriteLine($"Id {id} missing");
                    rw.EnterWriteLock();
                    try
                    {
                        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is writing.....");
                        cache[id] = $"Watermelon";
                    }
                    finally
                    {
                        rw.ExitWriteLock();
                    }
                }
                Console.WriteLine("The data read");
                foreach (var v in cache)
                {
                    Console.WriteLine($"{v.Key} {v.Value}");
                } 
            }
            finally
            {
                rw.ExitUpgradeableReadLock();
            }
        }

        static void Main(string[] args)
        {

            //new Thread(Reader).Start();
            //new Thread(Reader).Start();
            //new Thread(Writer).Start();
            //new Thread(Writer).Start();
            //new Thread(Reader).Start();
            //new Thread(Writer).Start();

            cache.Add(1, "Apple");
            cache.Add(2, "Banana");
            cache.Add(3, "Cherry");
            cache.Add(4, "Date");
            cache.Add(5, "Elderberry");

            Thread t1 = new Thread(() => GetData(1));
            Thread t2 = new Thread(() => GetData(2));
            new Thread(() => GetData(6)).Start();

            t1.Start();
            t2.Start();
            Console.ReadLine();
        }

        
    }
}
