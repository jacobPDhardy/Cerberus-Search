namespace Cerberus_Search_Complete
{
    public class Operation //Done
    {
        public char Operator { get; private set; }
        public bool SetOperator(char gate)
        {
            if (Operator == '\0')
            {
                Operator = gate;
                return true;
            }
            else if(gate == Operator)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Search> Searches { get; private set; } = new List<Search>();
        public void AddSearch(Search search)
        {
            Searches.Add(search);
        }

        public async Task<List<Log>> Solve()
        {
            List<List<Log>> datasets = await GetDatasets();
            return await GateSolver.AutoSolve(datasets, Operator);
        }

        private async Task<List<List<Log>>> GetDatasets()
        {
            List<List<Log>> datasets = new List<List<Log>>();
            foreach (Search search in Searches)
            {
                var dataset = await search.GetDataset();
                datasets.Add(dataset);
            }
            return datasets;
        }

        public override string ToString()
        {
            string operationString = "";
            foreach (Search search in Searches)
            {
                if (search.Not)
                {
                    operationString += Gates.NOT;
                }
                operationString += search.SearchString;
                operationString += Operator;

            }
            return operationString.Remove(operationString.Length - 1);
        }
    }
}
