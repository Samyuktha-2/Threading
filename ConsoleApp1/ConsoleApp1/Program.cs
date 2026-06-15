using System;
using System.Threading;
using System.Timers;

namespace ConsoleApp1
{
    class Program
    {
        private static bool _isRunning = false;
        private static System.Timers.Timer timer;

        static void Main(string[] args)
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_isRunning)
            {
                Console.WriteLine($"Skipped: {DateTime.Now}");
                return;
            }

            _isRunning = true;

            try
            {
                Console.WriteLine($"Start:   {DateTime.Now}");
                Thread.Sleep(5000);
                Console.WriteLine($"Stop:   {DateTime.Now}");
            }
            finally
            {
                _isRunning = false;
            }
        }

        private static void Callback(object state)
        {
            Console.WriteLine($"Tick: {DateTime.Now}");
        }  
    }
}
