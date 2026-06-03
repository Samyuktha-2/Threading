using System;
using System.Threading;

namespace RaceCondition
{
    class Program
    {
        static object myLock = new object();
        static int Balance1 = 1000;
        static int Balance2 = 1000;

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                Work1(100, 1);
            });
            Thread t2 = new Thread(() =>
            {
                Work1(200, 2);
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"Final balance(Lock): {Balance1}");

            Thread t3 = new Thread(() =>
            {
                Work2(100, 1);
            });

            Thread t4 = new Thread(() =>
            {
                Work2(200, 2);
            });

            t3.Start();
            t4.Start();

            t3.Join();
            t4.Join();

            Console.WriteLine($"Final Balance(Without Lock): {Balance2}");

            Console.ReadLine();
        }

        static void Work1(int data, int threadNum)
        {
            lock (myLock)
            {
                Console.WriteLine($"Thread {threadNum} started");

                int temp = Balance1;

                Thread.Sleep(1000);

                temp -= data;

                Thread.Sleep(1000);

                Balance1 = temp;

                Console.WriteLine($"Balance : {Balance1}");

                Thread.Sleep(2000);
            }
        }

        static void Work2(int data, int threadNum)
        {
            Console.WriteLine($"Thread {threadNum} started");

            int temp = Balance2;

            Thread.Sleep(1000);
             
            temp -= data;

            Thread.Sleep(1000);

            Balance2 = temp;

            Console.WriteLine($"Balance : {Balance2}");

            Thread.Sleep(2000);
        }
    }


}
