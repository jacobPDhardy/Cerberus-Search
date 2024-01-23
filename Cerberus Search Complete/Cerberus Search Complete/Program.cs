using Cerberus_Search_Complete; //Done

//string search = "(\"garbage\" & !\"Information\") ^ !(\"2023-11-16\" & \"drive stages synced\")";
//string search2 = "\"halo\" & \"2024\"";
//string search3 = "(\"garbage\" & !\"information\") ^ (\"2021-09-09T14:05:19\")";
//string search4 = "(\"\")";
//string search5 = "";
////string ultimateTestSearch = "!(\"garbage\" | (!\"mcr\" & \"halo) | (!\"\"))";
//string benchmarkSearch = "!((\"garbage\") ^ (\"halo\" ^ \"mcr\" ^ \"2023\") )";
//SearchStatement searchStatement = new SearchStatement(benchmarkSearch);
//await searchStatement.Solve();

//await DatasetUtilities.OutputDataset(searchStatement.ResultDataset);

await DatasetUtilities.BenchmarkAverage();