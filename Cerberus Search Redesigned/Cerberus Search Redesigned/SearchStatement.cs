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
        public string Operator { get; private set; } = Gates.NULL;
        public bool Not { get; private set; } = false;
        public bool Root { get; private set; } = false;

        private List<SearchStatement> childStatements = new List<SearchStatement>();

        private readonly string _search;
        public SearchStatement(string search, bool? not = null)
        {
            _search = search;
            if (not == null)
            {
                if (HighLevelParser.ParseNot(search))
                {
                    Not = true;
                    _search = _search.Remove(0, 2);
                }
            }
            else
            {
                Not = not ?? false;
            }
            Map(_search);
            Console.WriteLine($"Mapped statement:\n{this}\n");
        }

        public void Map(string search)
        {
            List<IntermediateSearchStatement> searchFragments = HighLevelParser.ParseStatement(search);
            if (!IsRoot(searchFragments))
            {
                foreach (var searchFragment in searchFragments)
                {
                    childStatements.Add(new SearchStatement(searchFragment.Search, searchFragment.Not));
                }
            }
            else
            {
                Root = true;
            }
        }

        public bool IsRoot(List<IntermediateSearchStatement> statementFragments)
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

        public async Task<List<HXT264Log>> Solve()
        {
            if (Root)
            {
                Operation operation = LowLevelParser.ParseOperation(_search);
                await Task.Delay(10);
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
                foreach (var childStatement in childStatements)
                {
                    await childStatement.Solve();
                    childDatasets.Add(childStatement.ResultDataset);
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
