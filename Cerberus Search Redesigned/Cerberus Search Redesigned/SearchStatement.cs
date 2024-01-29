using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public class IntermediateSearchStatement
    {
        public string Search { get; private set; }
        public bool Not { get; private set; }
        public IntermediateSearchStatement(string search, bool not)
        {
            Search = search;
            Not = not;
        }
    }

    public class SearchStatement
    {
        public List<HXT264Log> ResultDataset { get; private set; } = new List<HXT264Log>();
        public string Operator { get; private set; } = "";
        public bool Not { get; private set; } = false;
        public bool Root { get; private set; } = false;

        private List<SearchStatement> childSearches = new List<SearchStatement>();

        private readonly string _search;
        public SearchStatement(string statement, bool not = false)
        {
            _search = statement;
            Not = not;
            Map(_search);
            Console.WriteLine($"Mapped statement:\n{this}\n");
        }

        public void Map(string search)
        {
            bool IsRoot(List <IntermediateSearchStatement> statementFragments)
            {
                if (statementFragments.Count <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            bool IsOperator(IntermediateSearchStatement searchFragment)
            {
                if (Gates.IsGate(searchFragment.Search))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            List<IntermediateSearchStatement> searchFragments = HighLevelParser.ParseStatement(search);
            if (!IsRoot(searchFragments))
            {
                foreach (var searchFragment in searchFragments)
                {
                    if (!IsOperator(searchFragment))
                    {
                        childSearches.Add(new SearchStatement(searchFragment.Search, searchFragment.Not));
                    }
                    else
                    {
                        Operator = searchFragment.Search;
                    }
                }
            }
            else
            {
                Root = true;
            }
        }

        public async Task<List<HXT264Log>> Solve()
        {
            if (Root)
            {
                Operation operation = LowLevelParser.ParseOperation(_search);
                List<HXT264Log> results = await operation.Solve();
                if (Not)
                {
                    ResultDataset = GateSolver.NOT(await LogsDatabase.GetAllLogs(), results);
                }
                else
                {
                    ResultDataset = results;
                }
            }
            else
            {
                List<List<HXT264Log>> childDatasets = new List<List<HXT264Log>>();
                foreach (var childSearch in childSearches)
                {
                    childDatasets.Add(await childSearch.Solve());
                }
                ResultDataset = await GateSolver.AutoSolve(childDatasets, Operator, Not);

            }
            return ResultDataset;
        }

        public override string ToString()
        {
            return $"{_search}\nIsRoot: {Root}\nOperator: {Operator}\nNot Status: {Not}";
        }
    }
}
