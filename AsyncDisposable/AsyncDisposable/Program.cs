using System;
using System.Threading.Tasks;

namespace AsyncDisposable
{
    class MyResource : IAsyncDisposable
    {
        public async Task DisposeAsync()
        {
            await Task.Delay(2000);
            Console.WriteLine("Task Completed");
        } 
    }
    class Program
    {
        //IAsyncDisposable
        static void Main(string[] args)
        {
            RunAsync();
        }
        static async Task RunAsync()
        {
            var resource = new MyResource();
            Console.WriteLine("Working........");
            resource.DisposeAsync().GetAwaiter().GetResult();
            Console.ReadLine();
        } 
    } 
    public interface IAsyncDisposable
    {
        Task DisposeAsync();
    }
}
