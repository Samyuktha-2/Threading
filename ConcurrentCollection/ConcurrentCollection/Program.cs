using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentCollection
{
    class Program
    { 
        static void Main(string[] args)
        {
            //BAG
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            bag.Add(10);
            bag.Add(20);
            bag.Add(30);
            bag.Add(40);
            int BagVal;
            bag.TryTake(out BagVal);
            Console.WriteLine(BagVal);

            bag.TryPeek(out BagVal);
            Console.WriteLine(BagVal);

            bag.TryTake(out BagVal);
            Console.WriteLine(BagVal);

            //THREAD-SAFE STACK
            ConcurrentStack<string> stack = new ConcurrentStack<string>();
            stack.Push("A");
            stack.Push("B");
            stack.Push("C");
            stack.Push("D");

            string item1;
            stack.TryPop(out item1);
            Console.WriteLine(item1);

            stack.TryPeek(out item1);
            Console.WriteLine(item1);

            stack.TryPop(out item1);
            Console.WriteLine(item1);

            //THREAD-SAFE QUEUE
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
            queue.Enqueue("AAA");
            queue.Enqueue("BBB");
            queue.Enqueue("CCC");
            string val;
            queue.TryDequeue(out val);
            Console.WriteLine(val);
            queue.TryDequeue(out val);
            Console.WriteLine(val);

            queue.TryPeek(out val);
            Console.WriteLine(val + "\n");


            //THREAD-SAFE DICTIONARY
            ConcurrentDictionary<int, string> employee = new ConcurrentDictionary<int, string>();

            Parallel.For(1, 6, i =>
                        {
                            employee.TryAdd(i, $"Emp - {i}");
                        });

            foreach (var item in employee)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            string value = employee.GetOrAdd(9, "Emp - 9");
            Console.WriteLine(value + "\n");

            string value2 = employee.GetOrAdd(5, "Emp - 10");
            Console.WriteLine(value2 + "\n");

            var value3 = employee.GetOrAdd(10, key =>
            {
                Console.WriteLine("Creating value");
                return "Emp - 10";
            });

            Console.WriteLine(value3 + "\n");

            foreach (var item in employee)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            Console.ReadLine();
        }
    }

}





