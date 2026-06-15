using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalThread
{
    class Program
    {
        static ThreadLocal<int> counter = new ThreadLocal<int>(()=>10); 
        //static int counter = 0;
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Work);
            Thread t2 = new Thread(Work);
            Thread t3 = new Thread(Work);

            counter.Value = 10;

            t1.Start();
            t1.Join(); 

            t2.Start();
            t2.Join();

            t3.Start();
            t3.Join();
            Console.WriteLine(counter.Value);
            Console.WriteLine(counter.IsValueCreated);

            counter.Dispose(); //after disposing cannot access the counter variable

            Console.ReadLine();
        } 
        private static void Work()
        {
            counter.Value += 1;
            //counter += 1;
            Console.WriteLine(counter.Value);
        }
    }
}
