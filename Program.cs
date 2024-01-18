using Gate_Solver;

var dataset1 = await SearchLogs.SimpleSearch("garbage");
var dataset2 = await SearchLogs.SimpleSearch("information");
var dataset3 = await SearchLogs.SimpleSearch("watchdog");
var dataset4 = await SearchLogs.SimpleSearch("10:42");
var dataset5 = await SearchLogs.SimpleSearch("2024");
var globalDataset = await SearchLogs.SimpleSearch("");

await GateSolver.OutputDataset(new List<Log>(dataset1));
Console.WriteLine("TEST 1");
var ORdataset = GateSolver.OR(new List<List<Log>> { dataset1, dataset2, dataset3 });
await GateSolver.OutputDataset(ORdataset);

Console.WriteLine("TEST 2");
var ANDdataset = GateSolver.AND(new List<List<Log>> { dataset1, dataset2, dataset3, dataset4 });
await GateSolver.OutputDataset(ANDdataset);

Console.WriteLine("TEST 3");
await GateSolver.OutputDataset(globalDataset);

Console.WriteLine("TEST 4");
var NOTdataset = GateSolver.NOT(globalDataset, dataset2);
await GateSolver.OutputDataset(NOTdataset);


Console.WriteLine("TEST 5");
var XORdataset = GateSolver.XOR(new List<List<Log>>() {dataset1,dataset5});
await GateSolver.OutputDataset(XORdataset,100);