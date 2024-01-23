using Cerberus_Search_Complete; //Done

string search = "(\"garbage\" & !\"Information\") ^ !(\"2023-11-16\" & \"drive stages synced\")";
string search2 = "\"halo\" & \"2024\"";
string search3 = "(\"garbage\" & !\"information\") ^ (\"2021-09-09T14:05:19\")";

SearchStatement searchStatement = new SearchStatement(search3);
await searchStatement.Solve();

await DatasetUtilities.OutputDataset(searchStatement.ResultDataset);