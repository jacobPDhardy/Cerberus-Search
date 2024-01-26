using Cerberus_Search_Redesigned;

async Task<List<HXT264Log>> AdvancedLogSearch(string search)
{
    SearchStatement searchStatement = new SearchStatement(search);
    List<HXT264Log> searchResults = await searchStatement.Solve();
    return searchResults;
}

List<HXT264Log> searchResults = await AdvancedLogSearch("(\"garbage\" & !\"information\")"); //("garbage" & !"information")
await CSearchUtilities.OutputDataset(searchResults);