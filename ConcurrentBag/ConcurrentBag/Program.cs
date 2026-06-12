using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentBag
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();

            bag.Add(10);
            bag.Add(20);
            bag.Add(30);

            if(bag.TryTake(out int value))
            {
                Console.WriteLine(value);
            }

            Console.ReadLine();
        }
    }
}
