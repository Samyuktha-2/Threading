using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atomicity
{
    //atomicity achieved through Interlocked
    class Program
    {
        static int Counter = 0;
        static int Counter2 = 0;

        //non-atomic function
        static void Increment()
        {
            for(int i = 0; i < 10000; i++)
            {
                //counter++ is non-atomic as it can be interrupted by scheduler
                Counter++; //CPU does this in three steps: Read -> Modify -> Write
            }
        }

        //atomic function
        static void Increment2()
        {
            for(int i = 0; i < 10000; i++)
            {
                Interlocked.Increment(ref Counter2);
            }
        }
        static int DecCounter = 100;
        static void Decrement()
        {
            for(int i = 100; i >= 50; i--)
            {
                Interlocked.Decrement(ref DecCounter);
            }
        }

        static int OldValue = 10;
        static int NewValue = 30;

        static void Exchange()
        {
            Interlocked.Exchange(ref OldValue, NewValue);
            Interlocked.Exchange(ref NewValue, 50);
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Increment);
            Thread t2 = new Thread(Increment);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
            Console.WriteLine($"Non-Atomic Counter: {Counter}  values might differ");

            Thread t3 = new Thread(Decrement);
            t3.Start();
            t3.Join();

            Console.WriteLine($"Dec Counter: {DecCounter}");

            Thread t4 = new Thread(Exchange);
            t4.Start();
            t4.Join();
            Console.WriteLine($"Exchanged value: {OldValue}");
            Console.WriteLine($"Exchanged value: {NewValue}");

            Thread t5 = new Thread(Increment2);
            Thread t6 = new Thread(Increment2);

            t5.Start();
            t6.Start();

            t5.Join();
            t6.Join();
            Console.WriteLine($"Atomic Incremented value = {Counter2}");
            Console.ReadLine();
        }
    }
}
