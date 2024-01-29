using System;
using System.Diagnostics;

namespace Cerberus_Search_Redesigned
{
    public static class CSearchUtilities //DONE
    {
        public static async Task OutputDataset(List<HXT264Log> dataset, int delay = 0)
        {
            foreach (var data in dataset)
            {
                Console.WriteLine(data);
                await Task.Delay(delay);
            }
        }

        public static async Task<List<HXT264Log>> RunDemo(string search = "!((\"garbage\" & !\"Information\") & !(\"watchdog\"))")
        {
            SearchStatement searchStatement = new SearchStatement(search);
            List<HXT264Log> results = await searchStatement.Solve();
            await OutputDataset(results);
            return results;
        }

        public static async Task<TimeSpan> Benchmark(string benchmarkSearch = "(\"mcr\" ^ \"garbage\" ^ \"information\" ^ \"identity\" ^ \"2024\")")
        {
            var timer = new Stopwatch();
            timer.Start();

            SearchStatement searchStatement = new SearchStatement(benchmarkSearch);
            List<HXT264Log> results = await searchStatement.Solve();

            timer.Stop();

            TimeSpan timespan = timer.Elapsed;
            Console.WriteLine($"{results.Count} results found in {timespan.ToString(@"m\:ss\.fff")}"); //Expected results = 53433 or with new db results = 54059
            return timespan;
        }

        public static async Task<double> BenchmarkAverage(string benchmarkSearch = "(\"mcr\" ^ \"garbage\" ^ \"information\" ^ \"identity\" ^ \"2024\")", int iterations = 5)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            for (int count = 0;count < iterations;count++)
            {
                Console.WriteLine($"Running benchmark {count} out of {iterations}\n");
                times.Add(await Benchmark(benchmarkSearch));
                Console.WriteLine();
            }
            var total = times.Sum(time => time.TotalMicroseconds);
            var average = total / iterations;
            Console.WriteLine($"{iterations} benchmarks ran in {total} with an average benchmark time of {average}");
            return average;
        }
    }
}
