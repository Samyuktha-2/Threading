using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            //no parameter
            Thread t1 = new Thread(Work1);
            t1.Start();

            //ParameterizedThreadStart
            Thread t2 = new Thread(new ParameterizedThreadStart(Work2));
            t2.Start("Hello\n");

            //lambda 
            Thread t3 = new Thread(() =>
            {
                Work2("Hello");
            });
            t3.Start();

            int x = 10;
            t3 = new Thread(() =>
            {
                Console.WriteLine(x + "\n");
            });
            t3.Start();
            x = 30;

            //DTO
            Thread t4 = new Thread(Work3);
            t4.Start(new UserData
            {
                Id = 1,
                Name = "Samyuktha"
            }); 

            //modern .NET 
            Task.Run(() =>
            {
                Work3(new UserData
                {
                    Id = 2,
                    Name = "Samyu"
                });
            }); 
            Console.ReadLine();
        } 

        static void Work1()
        {
            Console.WriteLine("Hello without parameter\n");
        }

        static void Work2(object data)
        {
            Console.WriteLine(data);
        }

        static void Work3(object obj)
        {
            UserData user = (UserData)obj;
            Console.WriteLine($"{user.Id} - {user.Name}");
        }
    }

    class UserData
    {
        public int Id;
        public string Name;
    }
     
}
