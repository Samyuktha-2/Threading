using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWhenAll
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task1 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                return 20;
            });
            Task<int> task2 = Task.Run(() =>
            {
                Thread.Sleep(3000);
                return 30;
            });
            Task<int> completedTask = Task.WhenAny(task1, task2).GetAwaiter().GetResult();
            Console.WriteLine(completedTask.Result);

            Task t1 = Task.Delay(2000);
            Task t2 = Task.Delay(1000);
            Task t3 = Task.Delay(3000);

            Task.WhenAll(t1, t2, t3).GetAwaiter().GetResult(); //prints after 3sec

            Console.WriteLine("All task completed");

            Task<int> t4 = Task.Run(() => 10);
            Task<int> t5 = Task.Run(() => 20);
            Task<int> t6 = Task.Run(() => 30);
            int[] result = Task.WhenAll(t4, t5, t6).GetAwaiter().GetResult();

            foreach(var v in result)
            {
                Console.Write(v + " ");  // 10 20 30
            }

            Console.ReadLine();
        }
    }
}
