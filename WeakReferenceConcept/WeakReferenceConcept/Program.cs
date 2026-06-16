using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakReferenceConcept
{
    class Program
    {
        static WeakReference<Employee> weak;
        static void Main(string[] args)
        {
            CreateEmployee();

            Employee emp1 = new Employee();
            WeakReference<Employee> weak1 = new WeakReference<Employee>(emp1);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(); 
            Employee e;
            Console.WriteLine(weak.TryGetTarget(out e) ? "Method emp is Alive" : "Method emp is Collected");

            Employee e1;
            Console.WriteLine(weak1.TryGetTarget(out e1) ? "Alive" : "Collected");
            Console.ReadLine();
        }

        static void CreateEmployee()
        {
            Employee emp = new Employee();
            weak = new WeakReference<Employee>(emp); 
        }
    } 
    class Employee
    {
        public string Name = "Sam";
    }
}
