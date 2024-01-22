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
    }
}
