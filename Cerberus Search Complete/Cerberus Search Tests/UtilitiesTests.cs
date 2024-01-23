using NuGet.Frameworks;

namespace Cerberus_Search_Tests
{
    [TestClass]
    public class UtilitiesTests 
    { 
        [TestMethod]
        public async Task TestDemo()
        {
            try
            {
                await CSearchUtilities.RunDemo();           
            }
            catch
            { 
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task TestBenchmark()
        {
            try
            {
                TimeSpan average = await CSearchUtilities.Benchmark();
                if(average > TimeSpan.FromSeconds(120))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}