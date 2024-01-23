using Cerberus_Search_Complete; //Done

string search = "(\"garbage\" & !\"Information\") ^ !(\"2023-11-16\" & \"drive stages synced\")";
string search2 = "\"halo\" & \"2024\"";
SearchStatement searchStatement = new SearchStatement(search2);
await searchStatement.Solve();

await DatasetUtilities.OutputDataset(searchStatement.ResultDataset);