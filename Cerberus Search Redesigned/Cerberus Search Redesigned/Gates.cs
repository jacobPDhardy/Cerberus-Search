using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public static class Gates //DONE
    {
        public static string AND { get; } = "&";
        public static string OR { get; } = "|";
        public static string XOR { get; } = "^";
        public static string NOT { get; } = "!";
        public static string[] operators { get; } = { AND, OR, XOR, NOT };

        public static string NULL { get; } = "NULL";

        public static bool IsGate(string @operator)
        {
            if (operators.Contains(@operator))
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
