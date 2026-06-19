using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TimerExercise
{
    class Program
    {
        static Timer timer = new Timer(10); 
        static int i = 1;  
        static void Main(string[] args)
        {
            timer.Elapsed += PrintTime;
            timer.AutoReset = true;
            timer.Start();  
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
    }
}
