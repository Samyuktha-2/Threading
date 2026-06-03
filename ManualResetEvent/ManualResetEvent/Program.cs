using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventConcept
{
    class Program
    {
        static ManualResetEvent mre = new ManualResetEvent(false);
        static void Work()
        {
            Console.WriteLine("Worker waiting........");
            mre.WaitOne();
            Console.WriteLine("Worker Proceeding....");
        }
        static void Main(string[] args)
        {
            new Thread(Work).Start();
            new Thread(Work).Start();
            mre.Set();
            mre.Reset();
            new Thread(Work).Start();

            Console.WriteLine("Main signals GO");

            mre.Set();
            mre.Reset();

            Console.ReadLine();
        }
    }
}
