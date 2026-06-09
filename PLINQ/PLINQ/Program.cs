using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = Enumerable.Range(1, 1000).ToList();

            //LINQ
            var result1 = list1.Where(x => x % 9 == 0).ToList();

            //PLINQ
            var result2 = list1.AsParallel().Where(x => x % 9 == 0).ToList();

            Parallel.ForEach(result1, r1 =>
            {
                Console.Write(r1 + " ");
            });

            Console.WriteLine("\n-----------------------------");

            Parallel.ForEach(result2, r2 =>
            {
                Console.Write(r2 + " ");
            });

            Console.WriteLine("\n-----------------------------");

            Enumerable.Range(1, 500).AsParallel().Where(x => x % 10 == 0).ForAll(x => { Console.Write(x + " "); });

            Console.WriteLine("\n----------------------------- ORDERED");
            var result3 = list1.AsParallel().AsOrdered().Where(x => x % 15 == 0);
            foreach(var r3 in result3)
            {
                Console.Write(r3 + " ");
            }
            Console.WriteLine("\n----------------------------- UNORDERED");
            var result4 = list1.AsParallel().AsUnordered().Where(x => x % 15 == 0);
            Parallel.ForEach(result4, r4 =>
            {
                Console.Write(r4 + " ");
            });

            Console.WriteLine("\n-----------------------------");

            //WithDegreeOfParallelism
            Enumerable.Range(1, 50).AsParallel().WithDegreeOfParallelism(5).ForAll(x =>
            {
                Console.WriteLine($"Number:{x} on Thread:{Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500);
            });
            Console.WriteLine("\n-----------------------------");

            //Exception handling  --> LINQ
            var result5 = Enumerable.Range(1, 10).Select(x =>
            {
                if (x == 5)
                {
                    //throw new Exception("Boom");
                    return 0;
                }
                return x;
            });
            foreach(var r5 in result5)
            {
                Console.Write(r5 + " ");
            }
            Console.WriteLine("\n-----------------------------");


            //Exception handling  --> PLINQ
            var result6 = Enumerable.Range(1, 10).AsParallel().Select(x =>
            {
                if (x == 5)
                {
                    return 0;
                }
                if (x == 8)
                {
                    return 0;
                }
                return x;
            });
            foreach(var r6 in result6)
            {
                Console.Write(r6 + " ");
            }
            Console.WriteLine("\n-----------------------------");

            try
            {
                var result7 = Enumerable.Range(1, 10).AsParallel().Select(x =>
                {
                    if (x == 5)
                    {
                        throw new Exception("Boom");
                    }
                    if (x == 8)
                    {
                        throw new Exception("Boom");
                    }
                    return x;
                });
            }
            catch(AggregateException ex)
            {
                foreach(var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }
            

            Console.ReadLine();
        }
    }
}
