namespace Single_Level_Operation_Translator

{
    public static class Gates
    {
        public static char AND { get; } = '&';
        public static char OR { get; } = '|';
        public static char XOR { get; } = '^';
        public static char NOT { get; } = '!';
        public static char[] operators { get; } = { AND,OR,XOR,NOT };

        public static bool IsGate(char @operator)
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
