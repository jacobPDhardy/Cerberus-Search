using Cerberus_Search_Redesigned;

async Task<List<HXT264Log>> AdvancedLogSearch(string search)
{
    SearchStatement searchStatement = new SearchStatement(search);
    List<HXT264Log> searchResults = await searchStatement.Solve();
    return searchResults;
}

List<HXT264Log> searchResults = await AdvancedLogSearch("(\"garbage\" & !\"information\")"); //("garbage" & !"information")
await CSearchUtilities.OutputDataset(searchResults);

//var results2 = await AdvancedLogSearch("![(\"garbage\" & \"information\")] & (\"debug\")");
//await CSearchUtilities.OutputDataset(results2);

//await CSearchUtilities.Benchmark();