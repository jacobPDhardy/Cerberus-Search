namespace Cerberus_Search_Complete
{
    public class Search //Done
    {
        public string SearchString { get; private set; }
        public bool Not { get; private set; }
        public Search(string searchString, bool not = false)
        {
            SearchString = searchString;
            Not = not;
        }

        public async Task<List<Log>> GetDataset()
        {
            List<Log> dataset = await LogsDatabase.SimpleSearch(SearchString);
            if(Not)
            {
                List<Log> globalDataset = await LogsDatabase.SimpleSearch("");
                dataset = GateSolver.NOT(globalDataset,dataset);
            }
            return dataset;
        }
    }
}
