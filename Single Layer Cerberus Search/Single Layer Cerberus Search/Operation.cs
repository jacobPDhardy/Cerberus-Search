namespace Single_Layer_Cerberus_Search
{
    public class Operation
    {
        private char @operator;
        public bool SetOperator(char gate)
        {
            if (@operator == '\0')
            {
                @operator = gate;
                return true;
            }
            else if(gate == @operator)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Search> searches = new List<Search>();
        public void AddSearch(Search search)
        {
            searches.Add(search);
        }

        public override string ToString()
        {
            string operationString = "";
            foreach (Search search in searches)
            {
                if (search.Not)
                {
                    operationString += Gates.NOT;
                }
                operationString += search.SearchString;
                operationString += @operator;

            }
            return operationString.Remove(operationString.Length-1);
        }

        private async Task<List<List<Log>>> GetDatasets()
        {
            List<List<Log>> datasets = new List<List<Log>>();
            foreach (Search search in searches)
            {
                var dataset = await search.GetDataset();
                datasets.Add(dataset);
            }
            return datasets;
        }

        public async Task<List<Log>> Solve()
        {
            List<List<Log>> datasets = await GetDatasets();

            if (@operator == Gates.AND)
            {
                return GateSolver.AND(datasets);
            }
            else if (@operator == Gates.OR)
            {
                return GateSolver.OR(datasets);
            }
            else if(@operator == Gates.XOR)
            {
                return GateSolver.XOR(datasets);
            }
            else
            {
                return datasets[0];
            }
        }
    }
}
