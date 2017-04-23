using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace StreamingVideo
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var inputFiles = new List<string>
            {
                "example",
                "me_at_the_zoo",
                "videos_worth_spreading",
                "trending_today",
                "kittens"
            };

            inputFiles.ForEach(file =>
            {
                var cachePlanner = new CachePlanner(file);

                cachePlanner.Plan();
                cachePlanner.PrintPlan();
            });

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
