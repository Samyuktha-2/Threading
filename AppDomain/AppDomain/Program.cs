using System;
using System.Diagnostics;

namespace AppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting current app domain detials
            AppDomain appDomain1 = AppDomain.CurrentDomain;
            Console.WriteLine($"Id: {appDomain1.Id}");
            Console.WriteLine($"Current AppDomain: {appDomain1.FriendlyName}");
            Console.WriteLine($"Base Directory: {appDomain1.BaseDirectory}");
            Console.WriteLine();

            //created a new domain
            AppDomain appDomain2 = AppDomain.CreateDomain("New Domain");
            Console.WriteLine($"New domain: {appDomain2.FriendlyName}");
            Console.WriteLine();

            //Loading code into appDomain
            AppDomain appDomain3 = AppDomain.CreateDomain("Worker Domain");

            Worker worker = (Worker)appDomain3.CreateInstanceAndUnwrap(typeof(Worker).Assembly.FullName, typeof(Worker).FullName);  //used to instantiate an object inside differnet appdomain
            worker.DoWork(); 
            AppDomain.Unload(appDomain3); //unloads all assemblies loaded into that appdomain and frees the memory
            Console.WriteLine("Domain unloaded.");
            Console.WriteLine();

            //process vs appdomain
            Console.WriteLine($"Process: {Process.GetCurrentProcess().ProcessName}");  //OS Layer - Process (Threads)
            Console.WriteLine($"AppDomain: {AppDomain.CurrentDomain.FriendlyName}");   //CLR Layer - AppDomain (Threads.exe)

            Console.ReadLine();
        }
    }

    class Worker : MarshalByRefObject   //base class to communicate across appdomain boundaries
    {
        public void DoWork()
        {
            Console.WriteLine($"Running in {AppDomain.CurrentDomain.FriendlyName}");
        }
    }

}
