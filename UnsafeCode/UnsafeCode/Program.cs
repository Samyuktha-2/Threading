using System;

namespace UnsafeCode
{
    class Program
    {
        static void Main(string[] args)
        {
            UnsafeFunction();
            FixedFunction();
            StackAllocFunction();
            Console.ReadLine();
        }

        static void UnsafeFunction()
        { 
            unsafe
            {
                Console.WriteLine("Unsafe Function");

                int x = 10;
                int* ptr = &x;

                Console.WriteLine(x);
                Console.WriteLine(*ptr);

                x = 20;
                Console.WriteLine(x);
                Console.WriteLine(*ptr);

                *ptr = 30;
                Console.WriteLine(x);
                Console.WriteLine(*ptr);
                Console.WriteLine();
            }
        }

        static void FixedFunction()
        {
            unsafe
            {
                Console.WriteLine("Fixed Funciton");
                int[] num = { 1, 2, 3, 4, 5 }; 
                fixed (int* ptr = num)
                {
                    Console.WriteLine(*ptr);
                    for(int i = 0; i < num.Length; i++)
                    {
                        Console.WriteLine(ptr[i]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void StackAllocFunction()
        {
            unsafe
            {
                Console.WriteLine("Stackalloc Function");

                int* num = stackalloc int[5];

                for(int i = 1; i <=5; i++)
                {
                    num[i - 1] = i * 10;
                }
                for(int i = 0; i < 5; i++)
                {
                    Console.WriteLine(num[i]);
                }
            }
        }
    }
}
