using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iprogress
{
    class Program
    {
        static void Main(string[] args)
        {
            IProgress<int> progress = new Progress<int>(value =>
            {
                Console.WriteLine($"Progress: {value}%");
            });
            DownloadAsync(progress).GetAwaiter().GetResult();
            Console.ReadLine();
        }
        static async Task DownloadAsync(IProgress<int> progress)
        {
            for(int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                progress.Report(i * 10);
            }
        }
    }
}
