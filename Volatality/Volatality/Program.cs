using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Volatality
{
    class Program
    {
        //ensure guranteed visibility among threads
        static volatile bool Stop = false; 
        static void Work()
        {
            while (!Stop)
            {
                Console.WriteLine("Program Working.....");
                Thread.Sleep(500);
            }

            Console.WriteLine("Program Stopped....");
        } 
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Work);
            t1.Start();
            Thread.Sleep(5000);
            Console.WriteLine("From main asking work to stop");
            Stop = true;
            t1.Join();
            Console.WriteLine("Work completed");
            Console.ReadLine();
        }
    }
}
