namespace Single_Layer_Cerberus_Search
{
    public static class DatasetUtilities
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
