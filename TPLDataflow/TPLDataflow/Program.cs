using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLDataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<string> name = new Lazy<string>(() =>
            {
                Console.WriteLine("Created");
                return "Sam";
            });
            Console.WriteLine("Program created");
            Console.WriteLine(name.Value);
            Console.WriteLine(name.Value);
            Console.WriteLine(name.Value);
        }
    }
}
