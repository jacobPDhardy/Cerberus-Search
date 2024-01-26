using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public static class LowLevelParser //Done
    {
        public static Operation ParseOperation(string search)
        {
            char speechmark = '"';
            char backslash = char.Parse("\\");

            bool escapeSequence = false;
            bool searchSequence = false;
            string searchString = "";
            bool not = false;

            Operation operation = new Operation();

            if (string.IsNullOrEmpty(search) || search == "(\"\")")
            {
                operation.AddSearch(new Search(""));
            }
            else
            {
                foreach (var character in search)
                {
                    if (character == speechmark && !escapeSequence)
                    {
                        searchSequence = !searchSequence;
                        if (!string.IsNullOrEmpty(searchString))
                        {
                            operation.AddSearch(new Search(searchString, not));
                            searchString = "";
                            not = false;
                        }
                    }
                    else if (searchSequence)
                    {
                        if (character == backslash && !escapeSequence)
                        {
                            escapeSequence = true;
                        }
                        else if (escapeSequence)
                        {
                            searchString += character;
                            escapeSequence = false;
                        }
                        else
                        {
                            searchString += character;
                        }
                    }
                    else if (!searchSequence)
                    {
                        if (Gates.IsGate(character.ToString()))
                        {
                            if (character.ToString() == Gates.NOT)
                            {
                                not = true;
                            }
                            else
                            {
                                if (!operation.SetOperator(character.ToString()))
                                {
                                    throw new FormatException("GUI ERROR PLACEHOLDER - Gates on the same level must match excluding not");
                                }
                            }
                        }
                    }
                }
            }
            return operation;
        }
    }
}
