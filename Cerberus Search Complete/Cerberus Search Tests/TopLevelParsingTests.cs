namespace Cerberus_Search_Tests
{
    [TestClass]
    public class TopLevelParsingTests
    {
        [TestMethod]
        public async Task EmptySearchTest()
        {
            SearchStatement statement = new SearchStatement(""); //Input = 
            List<Log> results = await statement.Solve();
            if (results.Count == 0)
            {
                Assert.Fail("Empty statements should get all logs. Its either not working or db is empty/not attached properly");
            }
        }

        [TestMethod]
        public async Task EmptyStatementTest()
        {
            SearchStatement statement = new SearchStatement("(\"\")"); //Input = ("")
            List<Log> results = await statement.Solve();
            if (results.Count == 0)
            {
                Assert.Fail("Empty statements should get all logs. Its either not working or db is empty/not attached properly");
            }
        }

        [TestMethod]
        public void RootTest() //Input = ("garbage" & "information")
        {
            SearchStatement statement = new SearchStatement("(\"garbage\" & \"information\")");
            if (!statement.Root)
            {
                Assert.Fail("Root status incorrect");
            }
        }

    }
}
