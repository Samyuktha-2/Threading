using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaPhoreSlim
{
    class Program
    {
        static SemaphoreSlim sem = new SemaphoreSlim(1);
        static async Task Work(int id)
        {
            Console.WriteLine($"Task: {id}");
            await sem.WaitAsync();

            try
            {
                Console.WriteLine($"Task: {id}, entered");
                await Task.Delay(1500);
                Console.WriteLine($"Task: {id}, completed");

            }
            finally
            {
                sem.Release();
            }
        }

        static void Main(string[] args)
        {
            Task[] task = 
            {
                Work(1), Work(2), Work(3), Work(4), Work(5)
            };

            Task.WhenAll(task).GetAwaiter().GetResult();

            Console.ReadLine();
        }
    }
}
