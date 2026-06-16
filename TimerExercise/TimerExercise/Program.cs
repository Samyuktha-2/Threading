using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TimerExercise
{
    class Program
    {
        static Timer timer = new Timer(10);
        static Timer timer1 = new Timer(10);
        static Stopwatch sw = Stopwatch.StartNew();

        static int i = 1;
        static int i1 = 1;

        static void Main(string[] args)
        {
            timer.Elapsed += PrintTime;
            timer.AutoReset = true;
            timer.Start();

            timer1.Elapsed += PrintTime1;
            timer1.Start();

            Console.ReadLine();
        }

        private static void PrintTime(object sender, ElapsedEventArgs e)
        {
            if (i > 9)
            {
                timer.Stop();
                timer.Dispose();
                return;
            } 

            Console.WriteLine(i++);
        }

        private static void PrintTime1(object sender,ElapsedEventArgs e)
        {
            Console.WriteLine( $"Tick: {i1++} | " + $"Elapsed: {sw.ElapsedMilliseconds} ms");
        }
    }

}
