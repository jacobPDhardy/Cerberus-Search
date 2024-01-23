namespace Cerberus_Search_Tests
{
    [TestClass]
    public class LowLevelParsingTests
    {
        [TestMethod]
        public async Task ValidOperationTest()
        {
            Operation operation = LowLevelParser.ParseOperation("\"garbage\" ^ \"information\""); //Input = "garbage" & "information"
            if(operation.Operator != Gates.XOR)
            {
                Assert.Fail("Incorrect Operator");
            }
            if (operation.Searches[0].SearchString != "garbage" || operation.Searches[1].SearchString != "information")
            {
                Assert.Fail("Search strings are incorrect");
            }
            List<Log> results = await operation.Solve();
            if(results.Count == 0)
            {
                Assert.Fail("No results found");
            }
        }

        [TestMethod]
        public async Task ValidNotOperationTest()
        {
            Operation operation = LowLevelParser.ParseOperation("!\"garbage\" ^ !\"information\""); //Input = "garbage" & "information"
            if (operation.Operator != Gates.XOR)
            {
                Assert.Fail("Incorrect Operator");
            }
            if (operation.Searches[0].Not != true || operation.Searches[1].Not != true)
            {
                Assert.Fail("Not statuses are incorrect");
            }
            List<Log> results = await operation.Solve();
            if (results.Count == 0)
            {
                Assert.Fail("No results found");
            }
        }
    }
}
