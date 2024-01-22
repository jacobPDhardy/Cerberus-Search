namespace Tree_Builder
{
    public class SearchTree
    {
        public string ParentStatement { get; private set; }
        public SearchTree(string statement)
        {
            ParentStatement = statement;
            Console.WriteLine($"Mapping {statement}");
            Map(statement);
        }

        public List<SearchTree> ChildStatements { get; private set; } = new List<SearchTree>();

        public void Map(string statement)
        {
            List<string> topLevelStatements = ExtractTopLevel(statement);
            if (!IsRoot(topLevelStatements))
            {
                foreach (var topLevelStatement in topLevelStatements)
                {
                    ChildStatements.Add(new SearchTree(topLevelStatement)); //Recursively calls contructor to map tree
                }
            }
        }

        private bool IsRoot(List<string> statements)
        {
            if (statements.Count <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsWrapped(string statement)
        {
            if (!string.IsNullOrEmpty(statement))
            {
                int statementLength = statement.Length;
                if (string.Concat(statement[0], statement[1]) == "((" && string.Concat(statement[statementLength - 1], statement[statementLength - 2]) == "))")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string Unwrap(string statement)
        {
            if (IsWrapped(statement))
            {
                int statementLength = statement.Length;
                return statement.Substring(1, statement.Length - 2);
            }
            else
            {
                return statement;
            }
        }

        private List<string> ExtractTopLevel(string input)
        {
            string unwrappedInput = Unwrap(input);

            string statement = "";
            List<string> statements = new List<string>();
            int openBracketCount = 0;
            foreach (var character in unwrappedInput)
            {
                statement = statement + character;
                if (character == '(')
                {
                    openBracketCount++;
                }
                else if (character == ')')
                {
                    openBracketCount--;
                }
                if (openBracketCount == 0)
                {
                    if (statement != " ")
                    {
                        statements.Add(statement);
                    }
                    statement = "";
                }
            }
            statements = ScreenStatements(statements);
            return statements;
        }

        private List<string> ScreenStatements(List<string> statements)
        {
            List<string> screenedStatements = new List<string>();
            string command = "";
            foreach (var statement in statements)
            {
                if (!statement.Contains("("))
                {
                    command = command + statement;
                }
                else
                {
                    if (string.IsNullOrEmpty(command))
                    {
                        screenedStatements.Add(statement);
                    }
                    else if (statement.Contains("("))
                    {
                        screenedStatements.Add(command);
                        screenedStatements.Add(statement);
                        command = "";
                    }
                }
            }
            return screenedStatements;
        }

        public void Solve()
        {
            foreach (var statement in ChildStatements)
            {
                if (statement.ChildStatements.Count == 1 )
                {
                    //if search and not operator
                    //Solve
                }
            }
        }


    }
}
