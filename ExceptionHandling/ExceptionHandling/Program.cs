using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() =>
            {
                throw new Exception("Something went wrong");
            });
 
            try
            { 
                //task.Wait();
                RunAsync().GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            await Task.Run(() =>
            {
                throw new Exception("Something went wrong");
            });
        }
    }
}
