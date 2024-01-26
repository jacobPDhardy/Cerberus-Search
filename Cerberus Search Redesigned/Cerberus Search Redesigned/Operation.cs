using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public class Operation //Done
    {
        public string Operator { get; private set; }
        public bool SetOperator(string @operator)
        {
            if (string.IsNullOrEmpty(Operator))
            {
                Operator = @operator;
                return true;
            }
            else if (@operator == Operator)
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

        public async Task<List<HXT264Log>> Solve()
        {
            List<List<HXT264Log>> datasets = await GetDatasets();
            return await GateSolver.AutoSolve(datasets, Operator);
        }

        private async Task<List<List<HXT264Log>>> GetDatasets()
        {
            List<List<HXT264Log>> datasets = new List<List<HXT264Log>>();
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
