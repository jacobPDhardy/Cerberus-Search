namespace Cerberus_Search_Tests
{
    [TestClass]
    public class UtilitiesTests 
    { 
        [TestMethod]
        public async Task TestDemo()
        {
            await CSearchUtilities.RunDemo();           
        }

        [TestMethod]
        public async Task TestBenchmark()
        {
            TimeSpan average = await CSearchUtilities.Benchmark();
            if(average > TimeSpan.FromSeconds(120))
            {
                Assert.Fail();
            }
        }
    }
}