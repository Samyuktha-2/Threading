using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeadLock
{
    class Program
    {
        static object myLock1 = new object();
        static object myLock2 = new object();
        static void Thread1()
        {
            lock (myLock1)
            {
                Console.WriteLine("Acquired lock a1");
                Thread.Sleep(1000);
                lock (myLock2)
                {
                    Console.WriteLine("Acquired lock b1");
                }
            }
        }
        static void Thread2()
        {
            lock (myLock2)
            {
                Console.WriteLine("Acquired lock b2");
                Thread.Sleep(1000);
                lock (myLock1)
                {
                    Console.WriteLine("Acquired lock a2");
                }
            }
            //lock (myLock1)
            //{
            //    Console.WriteLine("Acquired lock a from 2");
            //    Thread.Sleep(1000);
            //    lock (myLock2)
            //    {
            //        Console.WriteLine("Acquired lock b from 2");
            //    }
            //}
        }
        static void Main(string[] args)
        {
            new Thread(Thread1).Start();
            new Thread(Thread2).Start();
            Console.ReadLine();
        }
    }
}
