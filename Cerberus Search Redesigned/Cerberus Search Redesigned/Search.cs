using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
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

        public async Task<List<HXT264Log>> GetDataset()
        {
            List<HXT264Log> dataset = await LogsDatabase.SimpleSearch(SearchString);
            if (Not)
            {
                List<HXT264Log> globalDataset = await LogsDatabase.SimpleSearch("");
                dataset = GateSolver.NOT(globalDataset, dataset);
            }
            return dataset;
        }
    }
}
