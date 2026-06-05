using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenAndSource
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = new CancellationToken();

            cts.CancelAfter(5000);
            Task task = Task.Run(() =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancelled");
                        return;
                    }
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                }
            }, token);
  
            task.Wait();

            Console.ReadLine();
        }
    }
}
