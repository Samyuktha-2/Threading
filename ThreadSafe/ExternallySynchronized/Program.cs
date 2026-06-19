using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternallySynchronized
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();
            Parallel.For(0, 4, i =>
            {
                c.Increment();
            });

            Console.ReadLine();
        }
    }

    class Counter
    {
        public int Value;
        public void Increment()
        {
            Value++;
            Console.WriteLine(Value);
        }
    }
}
