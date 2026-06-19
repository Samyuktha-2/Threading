using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternallySynchronized
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();

            Parallel.For(0, 4, i =>
            {
                c.Increment();
                c.GetValue();
            });

            Console.ReadLine();
        }
    }

    class Counter
    {
        private int value = 0;
        private readonly object _lock = new object();

        public void Increment()
        {
            lock (_lock)
            {
                value++;
            }
        }

        public void GetValue()
        {
            lock (_lock)
            {
                Console.WriteLine(value);
            }
        }
    }
}
