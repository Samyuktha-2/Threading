using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.For
            Parallel.For(1, 5, i =>
            {
                Console.WriteLine($"{i} Thread: {Thread.CurrentThread.ManagedThreadId}");
            });

            //Parallel ForEach
            List<string> name = new List<string>()
            {
                "John", "Charlie", "Adam"
            }; 
            Parallel.ForEach(name, n =>
            {
                Console.WriteLine($"{n}- Thread: { Thread.CurrentThread.ManagedThreadId}");
            });

            //Parallel.Invoke
            Parallel.Invoke(MethodA, MethodB, MethodC);

            //ParallelOptions
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2
            };
            Parallel.For(1, 5, options, i =>
            {
                   Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            });

            ////PLINQ
            //List<int> num = Enumerable.Range(1, 500).ToList();
            //Console.WriteLine("\nPlinq\n");
            ////method 1
            //var result = num.AsParallel().Where(x => x % 2 == 0).ToList();
            //Parallel.ForEach(result, r =>
            //{
            //    Console.WriteLine(r);
            //});

            //Thread.Sleep(4000);

            //Console.WriteLine("----------------------------------");
            ////Method 2
            //num.AsParallel().Where(x => x % 5 == 0).ForAll(x => { Console.WriteLine(x); });

            //AsOrdered() & UnOrdered()
            Console.WriteLine("Un - Ordered");
            Enumerable.Range(1, 50).AsParallel().Where(x => x % 7 == 0).ForAll(x => { Console.WriteLine(x); });

            Console.WriteLine("Ordered");
            Enumerable.Range(1, 50).AsParallel().Where(x => x % 7 == 0).AsOrdered().ForAll(x => { Console.WriteLine(x); });

            Console.ReadLine();
        }

        static void MethodA()
        {
            Console.WriteLine("\nParallel Invoke 1");
        }
        static void MethodB()
        {
            Console.WriteLine("Parallel Invoke 2");
        }
        static void MethodC()
        {
            Console.WriteLine("Parallel Invoke 3\n");
        }
    }
}
