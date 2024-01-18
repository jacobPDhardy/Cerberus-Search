using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single_Layer_Cerberus_Search
{
    public static class LowLevelParser
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

            if (string.IsNullOrEmpty(search))
            {
                operation.AddSearch(new Search(""));
            }

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
                    if (Gates.IsGate(character))
                    {
                        if (character == Gates.NOT)
                        {
                            not = true;
                        }
                        else
                        {
                            if (!operation.SetOperator(character))
                            {
                                throw new Exception("Gates on the same level must match excluding not"); //TEMPORARY ERROR THROW BECAUSE I DONT HAVE A GUI TO DISPLAY ERRORS DO NOT MARK ME DOWN -_-
                            }
                        }
                    }

                }
            }
            return operation;
        }

    }
}
