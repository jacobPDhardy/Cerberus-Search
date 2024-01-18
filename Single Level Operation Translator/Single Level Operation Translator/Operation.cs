namespace Single_Level_Operation_Translator
{
    public class Operation
    {
        public char Operator { get; private set; }

        public bool SetGate(char gate)
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
                operationString += Operator;

            }
            return operationString.Remove(operationString.Length-1);
        }

        //public List<string> Solve()
        //{
        //    switch (Gate.SelectedGate)
        //    {
        //        case Gates.AND:
        //            break;

        //        case Gates.OR:
        //            break;

        //        case Gates.XOR:
        //            break;

        //    }
        //    return new List<string>();
        //}
    }
}
