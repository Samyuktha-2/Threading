using System;

namespace GarbageCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee e = new Employee();
            e = null; 
            Console.WriteLine("Before GC");  
            Console.WriteLine("After GC"); 
            Console.ReadLine();
        }
    } 
    class Employee
    {
        public int Id; 
        ~Employee()
        {
            Console.WriteLine("Destroyed");
        }
    }
}
