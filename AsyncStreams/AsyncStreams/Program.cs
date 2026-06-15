using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            DoWork().GetAwaiter().GetResult();
            Console.ReadLine(); 
        }

        public static async Task DoWork()
        {
            await foreach (var n in GetNumberAsync())  //this syntax is accepted only in .NET 8
            {
                Console.WriteLine(n);
            }
        }
        public static async IAsyncEnumerable<int> GetNumberAsync()
        {
            for (int i = 0; i <= 5; i++)
            {
                await Task.Delay(1000);
                yield return i;
            }
        }


    }
}
