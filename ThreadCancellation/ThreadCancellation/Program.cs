using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadCancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task worker = Task.Run(() => Work(cts.Token));
            Console.WriteLine(worker.Status); //waiting to run
            Thread.Sleep(5000);
            cts.Cancel();
            worker.Wait();
            Console.WriteLine(worker.Status); //Ran to Completion

            cts = new CancellationTokenSource();
            worker = Task.Run(() => Work(cts.Token));
            Console.WriteLine(worker.Status); //waiting to run
            Thread.Sleep(5000);
            cts.Cancel();

            CancellationTokenSource cts2 = new CancellationTokenSource();
            Thread t1 = new Thread(() =>
            {
                Work(cts2.Token);
            });
            Console.WriteLine(t1.ThreadState);  //unstarted
            t1.Start();
            Console.WriteLine(t1.ThreadState);  //Running
            Thread.Sleep(5000);
            cts2.Cancel();
            Thread.Sleep(2000);
            Console.WriteLine(t1.ThreadState);  //Stopped
            Console.ReadLine();
        }
        static void Work(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Thread Cancelled");
                    break;
                }
                Console.WriteLine("Working......");
                Thread.Sleep(1000);
            }
        }
    }
}
