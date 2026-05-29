using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Work);

            Thread t2 = new Thread(Work);
            Console.WriteLine(t1.ThreadState);

            t1.Start();
            t2.Start();

            Console.WriteLine(t1.ThreadState);

            t1.Join();

            Console.WriteLine(t1.ThreadState);
            Console.ReadLine();
        }

        public static void Work()
        { 
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }
    }
}
