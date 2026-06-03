using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Starvation
{
    class Program
    {
        static object myLock = new object();
        static int Counter = 0;
        static void LogRunning()
        {
            lock (myLock)
            {
                Counter++;
                LogRunning();   //throws exception due to infinite loop
            }
        }
        static void Main(string[] args)
        {
            //Starvation
            new Thread(LogRunning).Start();
            new Thread(LogRunning).Start(); 

            //Priority Starvation
            Thread High = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("high");
                }
            });
            Thread Low = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("low");

                }
            });

            High.Priority = ThreadPriority.Highest;
            Low.Priority = ThreadPriority.Lowest;

            High.Start();
            Low.Start();
            Console.WriteLine(Counter);

            Console.ReadLine();
        }
    }
}
