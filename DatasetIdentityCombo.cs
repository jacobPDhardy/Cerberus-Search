namespace Gate_Solver
{
    public class DatasetIdentityCombo
    {
        public int PrimaryValue { get; private set; }

        public int SecondaryValue { get; private set; }

        public DatasetIdentityCombo(int primaryValue, int secondaryValue)
        {
            PrimaryValue = primaryValue;
            SecondaryValue = secondaryValue;
        }

        public bool CompareIdentityMatch(DatasetIdentityCombo comparisonMatch)
        {
            if (PrimaryValue == comparisonMatch.PrimaryValue && SecondaryValue == comparisonMatch.SecondaryValue)
            {
                return true;
            }
            else if(PrimaryValue == comparisonMatch.SecondaryValue && SecondaryValue == comparisonMatch.PrimaryValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
