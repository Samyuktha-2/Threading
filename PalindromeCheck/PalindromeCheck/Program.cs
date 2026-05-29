using System;
using System.Threading;

namespace PalindromeCheck
{
    class Program
    {
        static string[] words;
        static string[] result;
        static void Main(string[] args)
        {
            string paragaph = "deed madam level hello radar world civic racec run viv hello nikin true not non ar apple noon python refer kayak deed programming stats wow rotator banana malayalam computer pop eye";
            words = paragaph.Split(' ');
            result = new string[words.Length];
             
            int threadCount = 5;
            Thread[] thread = new Thread[threadCount];
            for(int  i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                thread[i] = new Thread(() => Process(threadIndex, threadCount));
                thread[i].Start(); 
            }

            foreach (var t in thread)
            {
                t.Join();
            }

            //Parallel.ForEach(words,
            //    new ParallelOptions
            //    {
            //        MaxDegreeOfParallelism = 5
            //    },
            //    word =>
            //    {
            //        IsPlaindromeCheck(word);
            //    });

            foreach(var i in result)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        static void Process(int start, int step)
        {
            for (int i = start; i < words.Length; i += step)
            {
                bool isPlaindrome = IsPlaindromeCheck(words[i]);
                result[i] = $"{words[i]} - {isPlaindrome} - {Thread.CurrentThread.ManagedThreadId}";
            }
        }

        static bool IsPlaindromeCheck(string word)
        {
            
            for (int i = 0; i < word.Length / 2; i++)
            {
                if (word[i] != word[word.Length - 1 - i])
                {
                    return false;
                    
                }
            }
            return true; 
        } 
    }
}

