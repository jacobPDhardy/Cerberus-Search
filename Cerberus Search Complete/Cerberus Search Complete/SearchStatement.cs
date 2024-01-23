using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Complete
{
    public class SearchStatement //Done - Algorithm needs more testing
    {
        private List<SearchStatement> childStatements = new List<SearchStatement>();

        public List<Log> ResultDataset { get; private set; } = new List<Log>();
        public bool Root { get; private set; }
        public char TopLevelOperator { get; private set; }

        private readonly string _search;
        public SearchStatement(string search)
        {
            _search = search;
            Map(_search);
            //Console.WriteLine($"Mapping statement:\n{this}");
        }

        public void Map(string search)
        {
            List<string> searchFragments = HighLevelParser.ExtractTopLevel(search);
            if (!IsRoot(searchFragments))
            {
                Root = false;
                TopLevelOperator = FindOperator(searchFragments);
                foreach (string fragment in searchFragments)
                {
                    if (!char.TryParse(fragment, out char _))
                    {
                        childStatements.Add(new SearchStatement(fragment));
                    }
                }
            }
            else
            {
                Root = true;
                TopLevelOperator = '\0';
            }  
        }

        public bool IsRoot(List<string> statements)
        {
            if (statements.Count <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private char FindOperator(List<string> searchFragments)
        {
            foreach (var searchFragment in searchFragments)
            {
                if (char.TryParse(searchFragment, out char searchFragmentChar))
                {
                    if (Gates.IsGate(searchFragmentChar))
                    {
                        if (searchFragmentChar != Gates.NOT)
                        {
                            return searchFragmentChar;
                        }
                    }
                }
            }
            throw new FormatException("No operator found");
        }

        public async Task<List<Log>> Solve()
        {
            if (Root)
            {
                Operation operation = LowLevelParser.ParseOperation(_search);
                ResultDataset = await operation.Solve();
                //Console.WriteLine($"Solved statement:\n{this}\n");
            }
            else
            {
                List<List<Log>> childDatasets = new List<List<Log>>();
                foreach(var childStatement in childStatements)
                {
                    await childStatement.Solve();
                    childDatasets.Add(childStatement.ResultDataset);
                }
                ResultDataset = await GateSolver.AutoSolve(childDatasets, TopLevelOperator) ;
                //Console.WriteLine($"Solved statement:\n{this}\n");
            }
            return ResultDataset;
        }

        public override string ToString()
        {
            string searchStatementString = $"{_search}\nIsRoot: {Root}\n";
            if (!Root)
            {
                searchStatementString += $"Operator: {TopLevelOperator}";
            }
            return searchStatementString;
        }
    }
}
