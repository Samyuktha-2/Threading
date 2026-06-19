using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafe
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee("Samyu", 21);
            Console.WriteLine(emp); 
            Console.WriteLine(emp.Name + emp.Age);  
            //emp.Name = "Sam";  -->Raises error, cuz name and age are threadsafe and it can't be modified
            Console.WriteLine(emp.Name + emp.Age);
            Console.ReadLine();
        }
    }

    class Employee
    {
        public string Name { get; }
        public int Age { get; }

        public Employee(string name,int age)
        {
            Name = name;
            Age = age;
        }
    }
}
