using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionBlocking
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockingCollection<int> jobs = new BlockingCollection<int>();

            Task producer = Task.Run(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    jobs.Add(i);
                    Console.WriteLine($"Produced - {i}");
                    Thread.Sleep(1000);
                }
                jobs.CompleteAdding();
            });

            Task consumer = Task.Run(() =>
            {
                foreach (int item in jobs.GetConsumingEnumerable())
                {
                    Console.WriteLine($"Consumed - {item}");
                }
            });


            Task.WaitAll(producer, consumer);

            Console.ReadLine();
        }
    }
}
