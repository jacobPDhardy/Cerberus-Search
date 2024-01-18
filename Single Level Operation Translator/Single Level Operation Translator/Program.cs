using Single_Level_Operation_Translator;

Operation ParseOperation(string search)
{
    char speechmark = '"';
    char backslash = char.Parse("\\");

    bool escapeSequence = false;
    bool searchSequence = false;
    string searchString = "";
    bool not = false;

    Operation operation = new Operation();

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
                    if (!operation.SetGate(character))
                    {
                        throw new Exception("Gates on the same level must match excluding not");
                    }
                }
            }

        }
    }
    return operation;
}

Console.WriteLine(ParseOperation("(((!\"garbage\" & !\"collection\" & \"\\\"cookies\\\"\")))"));
//Console.WriteLine(ParseOperation("(!\"garbage\" & !\"collection\" ^ \"\\\"cookies\\\"\")"));