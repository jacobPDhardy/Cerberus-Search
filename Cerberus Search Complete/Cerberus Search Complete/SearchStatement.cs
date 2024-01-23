using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Complete
{
    public class SearchStatement //Done - Algorithm needs more testing
    {
        public List<Log> ResultDataset { get; private set; } = new List<Log>();

        private List<SearchStatement> childStatements = new List<SearchStatement>();

        private readonly string _search;
        public SearchStatement(string search)
        {
            _search = search;
            Map(_search);
            Console.WriteLine($"Mapping statement:\n{this}");
        }

        private char @operator;
        private bool not;
        private bool isRoot;
        public void Map(string search)
        {
            List<string> searchFragments = ExtractTopLevel(search);
            if (!IsRoot(searchFragments))
            {
                isRoot = false;
                not = IsNot(search);
                @operator = FindOperator(searchFragments);
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
                isRoot = true;
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

        private bool IsNot(string search)
        {
            if (search.StartsWith("!((") || search.StartsWith("!(!") || search.StartsWith("(!("))
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

        private List<string> ExtractTopLevel(string search)
        {
            char backslash = char.Parse("\\");
            bool escapeSequence = false;

            string searchFragment = "";
            List<string> searchFragments = new List<string>();
            int bracketCount = 0;

            if (search.Length > 2 && search.StartsWith("!\""))
            {
                searchFragments.Add(search);
                return searchFragments;
            }
            else if (search.Length > 1 && search.StartsWith("\""))
            {
                searchFragments.Add(search);
                return searchFragments;
            }

            string unwrappedStatement = Unwrap(search);
            foreach (var character in unwrappedStatement)
            {
                if (!escapeSequence)
                {
                    if (character != backslash)
                    {
                        searchFragment += character;
                        if (character == '(')
                        {
                            bracketCount++;
                        }
                        else if (character == ')')
                        {
                            bracketCount--;
                        }
                        if (bracketCount == 0)
                        {
                            if (!string.IsNullOrWhiteSpace(searchFragment))
                            {
                                searchFragments.Add(searchFragment);
                            }
                            searchFragment = "";
                        }
                    }
                    else
                    {
                        escapeSequence = true;
                    }
                }
                else
                {
                    searchFragment += character;
                    escapeSequence = false;
                }
            }
            return searchFragments;
        }

        private string Unwrap(string search)
        {
            if (search.StartsWith("((") && search.EndsWith("))"))
            {
                return search.Substring(1, search.Length - 2);
            }
            else if (search.StartsWith("!(") && search.EndsWith(")"))
            {
                return search.Substring(2, search.Length - 3);
            }
            else
            {
                return search;
            }
        }

        public async Task Solve()
        {
            if (isRoot)
            {
                Operation operation = LowLevelParser.ParseOperation(_search);
                ResultDataset = await operation.Solve();
                Console.WriteLine($"Solved statement:\n{this}\n");
            }
            else
            {
                List<List<Log>> childDatasets = new List<List<Log>>();
                foreach(var childStatement in childStatements)
                {
                    await childStatement.Solve();
                    childDatasets.Add(childStatement.ResultDataset);
                }
                ResultDataset = await GateSolver.AutoSolve(childDatasets, @operator, not) ;
                Console.WriteLine($"Solved statement:\n{this}\n");
            }
        }

        public override string ToString()
        {
            string searchStatementString = $"{_search}\nIsRoot: {isRoot}\n";
            if (!isRoot)
            {
                searchStatementString += $"Operator: {@operator}\nNot Status: {not}\n";
            }
            return searchStatementString;
        }
    }
}
