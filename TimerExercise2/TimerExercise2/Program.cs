using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TimerExercise2
{
    class Program
    {
        static Timer timer = new Timer(1000);
        static Stopwatch sw = Stopwatch.StartNew();
        static ThreadLocal<int> threadCounter = new ThreadLocal<int>(() => 0);
        static int tickCount = 0;
        static WeakReference<Employee> weakRef;

        static void Main(string[] args)
        {
            CreateWeakReference();
            timer.Elapsed += TimerElapsed;
            timer.Start(); 

            Console.ReadLine();
        } 
        static void CreateWeakReference()
        {
            Employee emp = new Employee();
            weakRef = new WeakReference<Employee>(emp);
            Console.WriteLine("Employee Created");
        } 
        static void TimerElapsed(object sender,ElapsedEventArgs e)
        {
            tickCount++;
            threadCounter.Value++; 
            Console.WriteLine($"\nTick: {tickCount}" +
                $"\nElapsed: {sw.ElapsedMilliseconds}ms" +
                $"\nThread: {Thread.CurrentThread.ManagedThreadId}" +
                $"\nThreadLocal Count: {threadCounter.Value}"); 

            for(int i = 0; i< 100; i++)
            {
                byte[] buffer = new byte[1024];
            } 

            if (tickCount == 5)
            {
                Console.WriteLine("Forcing GC......"); 
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect(); 
                Employee emp; 
                bool alive = weakRef.TryGetTarget(out emp); 
                Console.WriteLine(alive ? "Emp is Alive" : "Emp is Dead");
            } 
            if (tickCount == 10)
            {
                timer.Stop();
                timer.Dispose();
                Console.WriteLine("\nTimer Stopped");
                CheckWeakRefAlive();
            }
        }
        static void CheckWeakRefAlive()
        {
            Employee emp = new Employee();
            WeakReference<Employee> weakRef = new WeakReference<Employee>(emp);
            Employee e;
            Console.WriteLine(weakRef.TryGetTarget(out e) ? "Alive" : "Dead");
        }
    } 
    class Employee
    {
        public int Id; 
        ~Employee()
        {
            Console.WriteLine($"FINALIZER => Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
