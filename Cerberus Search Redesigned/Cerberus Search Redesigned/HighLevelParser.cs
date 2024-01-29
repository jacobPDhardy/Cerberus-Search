using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public static class HighLevelParser
    {
        public static List<IntermediateSearchStatement> ParseStatement(string search)
        {
            if (IsSingleLayer(search))
            {
                return new List<IntermediateSearchStatement> { new IntermediateSearchStatement(search, false) };
            }
            else
            {
                return ExtractTopLevel(search);
            }
        }

        private static bool IsSingleLayer(string search)
        {
            if ((string.IsNullOrEmpty(search)) || (search.Length > 1 && search.StartsWith("\"")) || (search.Length > 2 && search.StartsWith("!\""))) //If single layer or empty
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<IntermediateSearchStatement> ExtractTopLevel(string search)
        {
            char backslash = char.Parse("\\");
            string[] discardCharacters = { "!", "[", "]" };

            bool not = false;
            bool escapeSequence = false;
            string searchFragment = "";
            int bracketCount = 0;
            List<IntermediateSearchStatement> searchFragments = new List<IntermediateSearchStatement>();

            string unwrappedSearch = Unwrap(search);
            foreach (var character in unwrappedSearch)
            {
                if (!escapeSequence)
                {
                    if (character == '[')
                    {
                        not = true;
                    }
                    else if (character == ']')
                    {
                        not = false;
                    }
                    else if (character != backslash)
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
                            if (!string.IsNullOrWhiteSpace(searchFragment) && !discardCharacters.Contains(searchFragment))
                            {
                                searchFragments.Add(new IntermediateSearchStatement(searchFragment, not));
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

        private static string Unwrap(string search)
        {
            if (search.StartsWith("((") && search.EndsWith("))"))
            {
                return search.Substring(1, search.Length - 2);
            }
            else
            {
                return search;
            }
        }
    }
}
