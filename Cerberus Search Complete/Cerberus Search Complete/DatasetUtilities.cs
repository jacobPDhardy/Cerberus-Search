using System;
using System.Diagnostics;

namespace Cerberus_Search_Complete
{
    public static class DatasetUtilities //DONE
    {
        public static async Task OutputDataset(List<Log> dataset, int delay = 0)
        {
            foreach (var data in dataset)
            {
                Console.WriteLine(data);
                await Task.Delay(delay);
            }
        }

        public static async Task<TimeSpan> Benchmark(string benchmarkSearch = "(\"mcr\" ^ \"garbage\" ^ \"information\" ^ \"identity\" ^ \"2024\")")
        {
            var timer = new Stopwatch();
            timer.Start();

            SearchStatement searchStatement = new SearchStatement(benchmarkSearch);
            List<Log> results = await searchStatement.Solve();

            timer.Stop();

            TimeSpan timespan = timer.Elapsed;
            Console.WriteLine($"{results.Count} results found in {timespan.ToString(@"m\:ss\.fff")}"); //Expected results = 53433
            return timespan;
        }

        public static async Task BenchmarkAverage(string benchmarkSearch = "(\"mcr\" ^ \"garbage\" ^ \"information\" ^ \"identity\" ^ \"2024\")", int iterations = 10)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            for (int count = 0;count < iterations;count++)
            {
                Console.WriteLine($"Running test {count} out of {iterations}");
                times.Add(await Benchmark(benchmarkSearch));
            }
            var total = times.Sum(time => time.TotalMicroseconds);
            var average = total / iterations;
            Console.WriteLine($"{iterations} tests ran in {total.ToString(@"m\:ss\.fff")} with an average test time of {average.ToString(@"m\:ss\.fff")}");
        }
    }
}
