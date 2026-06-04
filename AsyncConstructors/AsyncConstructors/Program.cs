using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConstructors
{
    //Async Constructor
    class UserService
    {
        public string Data { get; private set; }
        public UserService() { }
        public static async Task<UserService> CreateAsync()
        {
            var service = new UserService();
            await service.LoadAsync();
            return service;
        }
        private async Task LoadAsync()
        {
            await Task.Delay(2000);
            Data = "User not loaded";
        }
    }

    //Async Property
    class User
    {
        public async Task<string> GetNameAsync()
        {
            await Task.Delay(2000);
            return "Sam";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = UserService.CreateAsync().GetAwaiter().GetResult();
            Console.WriteLine(userService.Data);

            User user = new User();
            var name = user.GetNameAsync().GetAwaiter().GetResult();
            Console.WriteLine(name);
            Console.ReadLine();
        }
    }
}
