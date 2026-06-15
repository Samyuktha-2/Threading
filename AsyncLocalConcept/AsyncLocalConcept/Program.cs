using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalConcept
{
    class Program
    {
        //static ThreadLocal<string> user = new ThreadLocal<string>();
        static AsyncLocal<string> user = new AsyncLocal<string>(); 
        static void Main(string[] args)
        {
            Work().GetAwaiter().GetResult(); 
            Console.ReadLine();
        } 
        static async Task Work()
        {
            user.Value = "Sam"; 
            await Task.Delay(1500);
            Console.WriteLine(user.Value);
        }
    }
}
