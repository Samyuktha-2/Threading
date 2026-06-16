using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StopTheWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(StartWorker).Start();
            Console.ReadLine();
        } 
        static void StartWorker()
        {
            int count = 0;
            Stopwatch sw = Stopwatch.StartNew();
            bool condition = true;
            while (condition)
            {
                Console.WriteLine(sw.ElapsedMilliseconds);
                for (int i = 1; i < 10; i++)
                {
                    byte[] data = new byte[1024];
                }
                count++;
                Thread.Sleep(10);
                if (count > 21)
                {
                    condition = false;
                }
            }
        }
    }
}
