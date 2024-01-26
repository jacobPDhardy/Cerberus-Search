using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public class DatasetIdCombo //DONE
    {
        public int PrimaryValue { get; private set; }
        public int SecondaryValue { get; private set; }
        public DatasetIdCombo(int primaryValue, int secondaryValue)
        {
            PrimaryValue = primaryValue;
            SecondaryValue = secondaryValue;
        }

        public bool CompareIdentityMatch(DatasetIdCombo comparisonMatch)
        {
            if (PrimaryValue == comparisonMatch.PrimaryValue && SecondaryValue == comparisonMatch.SecondaryValue)
            {
                return true;
            }
            else if (PrimaryValue == comparisonMatch.SecondaryValue && SecondaryValue == comparisonMatch.PrimaryValue)
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
