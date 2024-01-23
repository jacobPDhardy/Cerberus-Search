using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Complete
{
    public static class HighLevelParser
    {
        public static List<string> ExtractTopLevel(string search)
        {
            char backslash = char.Parse("\\");
            bool escapeSequence = false;

            string searchFragment = "";
            List<string> searchFragments = new List<string>();
            int bracketCount = 0;

            if ((string.IsNullOrEmpty(search)) || (search.Length > 1 && search.StartsWith("\"")) || (search.Length > 2 && search.StartsWith("!\"")))
            {
                searchFragments.Add(search);
            }
            else
            {
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
            }
            return searchFragments;
        }

        private static string Unwrap(string search)
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
    }
}
