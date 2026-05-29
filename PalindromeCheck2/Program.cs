using System;
using System.Collections.Generic;
using System.Threading;

namespace PalindromeCheck2
{
    class Program
    {
        static Queue<string> wordQueue = new Queue<string>();
        static void Main(string[] args)
        {
            string paragaph = "deed madam level hello radar world civic racec run viv hello nikin true not non ar apple noon python refer kayak deed programming stats wow rotator banana malayalam computer pop eye";
            string[] words = paragaph.Split(' ');

            foreach(var word in words)
            {
                wordQueue.Enqueue(word);
            }

            int threadCount = 5;
            List<ThreadInfo> threads = new List<ThreadInfo>();

            for(int i = 0; i < threadCount; i++)
            {
                ThreadInfo info = new ThreadInfo();
                info.ThreadId = i + 1;
                info.Status = "Free";
                info.Thread = new Thread(() => Worker(info));
                threads.Add(info);
            }

            foreach(var t in threads)
            {
                t.Thread.Start();
            }

            foreach(var t in threads)
            {
                t.Thread.Join();
            }
            Console.WriteLine("All thread completed");
            Console.ReadLine();
        }
        static void Worker(ThreadInfo thread)
        {
            while (true)
            {
                string word = null;
                lock (wordQueue)
                {
                    if(wordQueue.Count == 0)
                    {
                        thread.Status = "Finished";
                        return;
                    }

                    word = wordQueue.Dequeue();
                }

                thread.Status = "Running";
                thread.AssignedWord = word;
                bool result = isPalindrome(word);

                Console.WriteLine($"Thread: {thread.ThreadId} | Status: {thread.Status} | Word: {thread.AssignedWord} | IsPalindrome: {result}");

                Thread.Sleep(1000);

                thread.Status = "Free";
                thread.AssignedWord = "";
            }
        }

        static bool isPalindrome(string word)
        {
            for(int i = 0; i < word.Length/2; i++)
            {
                if(word[i] != word[word.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    class ThreadInfo
    {
        public int ThreadId;
        public string Status;
        public string AssignedWord;
        public Thread Thread; 
    }
}   
 
  


