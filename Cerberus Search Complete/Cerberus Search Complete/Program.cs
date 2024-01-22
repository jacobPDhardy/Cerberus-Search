using Cerberus_Search_Complete; //Done

string search = "(\"garbage\" & !\"Information\") ^ !(\"2023-11-16\" & \"drive stages synced\")";

SearchStatement searchStatement = new SearchStatement(search);
await searchStatement.Solve();

await DatasetUtilities.OutputDataset(searchStatement.ResultDataset);