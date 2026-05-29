using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WhyThread
{
    //Responsiveness, Parallelism, Concurrent, Throughoutput
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine($"Logical processor: {Environment.ProcessorCount}");
            ConcurrentDictionary<int, bool> Threadused = new ConcurrentDictionary<int, bool>();

            Console.WriteLine("Photoshop");

            Task uiTask = new Task(async() =>
            {
                while (true)
                {
                    Console.WriteLine("UI Responsive");
                    await Task.Delay(2000);
                    Console.WriteLine("Delayed\n");

                }
            });

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 10
            };

            Parallel.For(1, 21, imageid =>
            {
                int tId = Thread.CurrentThread.ManagedThreadId;

                Threadused.TryAdd(tId, true);

                Console.WriteLine($"Processing image: {imageid} on thread {tId}");
                Thread.Sleep(2000);
            });

            Console.WriteLine("Image exported");

            Console.WriteLine($"Threads used: {Threadused.Count}");
            Console.ReadLine();
        }
    }
}
