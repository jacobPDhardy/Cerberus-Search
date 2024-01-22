using Single_Layer_Cerberus_Search;

//string operationString = "\"garbage\" & !\"Information\" & \"2023\""; //Very bad case includes huge datasets
string operationString = "\"garbage\" & !\"Information\"";
Operation operation = LowLevelParser.ParseOperation(operationString);
//Console.WriteLine(operation);

List<Log> dataset = await operation.Solve();
await DatasetUtilities.OutputDataset(dataset);

//Console.WriteLine(ParseOperation("(!\"garbage\" & !\"collection\" ^ \"\\\"cookies\\\"\")"));