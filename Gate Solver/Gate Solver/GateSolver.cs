using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gate_Solver
{
    public static class GateSolver
    {
        public static async Task OutputDataset(List<Log> dataset,int delay = 0)
        {
            foreach (var data in dataset)
            {
                Console.WriteLine(data);
                await Task.Delay(delay);
            }
        }

        public static List<Log> OR(List<List<Log>> datasets)
        {
            return new List<Log>(AddDatasets(datasets));
        }

        public static List<Log> AND(List<List<Log>> datasets)
        {
            return new List<Log>(FindMatches(datasets));
        }

        public static List<Log> XOR(List<List<Log>> datasets)
        {
            List<DatasetId> datasetIdentities = new List<DatasetId>(); //ALL datasets mapped to id

            List<Log> GetDataset(int id)
            {
                return datasetIdentities.Where(log => log.Id == id).Select(datasetId => datasetId.Dataset).FirstOrDefault() ?? new List<Log>() ;
                //foreach(var dataset in datasetIdentities)
                //{
                //    if (dataset.Id == id)
                //    {
                //        return dataset.Dataset;
                //    }
                //}
                //return new List<Log>();
            }

            int idCount = 0;
            foreach(var dataset in datasets)
            {
                datasetIdentities.Add(new DatasetId(idCount,dataset));
                idCount++;
            }  //ALL datasets mapped to id

            List<DatasetIdentityCombo> uniqueCombos = new List<DatasetIdentityCombo>();
            for (int count = 0;count< datasetIdentities.Count;count++)
            {
                List<int> remainingIds = datasetIdentities.Select(datasetId => datasetId.Id).Where(id => id != count).ToList();
                foreach(var remainingId in remainingIds)
                {
                    Console.WriteLine($"combo found{count},{remainingId}");
                    DatasetIdentityCombo identityMatch = new DatasetIdentityCombo(count, remainingId);
                    if (!uniqueCombos.Any(combo => combo.CompareIdentityMatch(identityMatch)) || uniqueCombos.Count == 0)
                    {
                        Console.WriteLine($"valid combo found{count},{remainingId}");
                        uniqueCombos.Add(identityMatch);
                    }
                }
            } //All unique combos found

            //find matches of all unique combos

            List<List<Log>> allMatches = new List<List<Log>>();
            foreach (var combo in uniqueCombos)
            {
                allMatches.Add(FindMatches(new List<List<Log>>() { GetDataset(combo.PrimaryValue), GetDataset(combo.SecondaryValue) })); //map the datasets
            }

            List<Log> result = SortAscending(SubtractDatasets(AddDatasets(datasets),new List<List<Log>>() { RemoveDuplicates(AddDatasets(allMatches)) }));




            return result;
        }
        
        public static List<Log> NOT(List<Log> globalDataset,List<Log> dataset)
        {
            return SubtractDatasets(globalDataset, new List<List<Log>>() { dataset });
        }


        private static List<Log> AddDatasets(List<List<Log>> datasets)
        {
            List<Log> result = new List<Log>();
            foreach (var dataset in datasets)
            {
                foreach (var data in dataset)
                {                  
                    result.Add(data);  
                }
            }
            return SortAscending(RemoveDuplicates(result));
        }

        private static List<Log> SubtractDatasets(List<Log> subjectDataset,List<List<Log>> negativeDatasets)
        {
            List<int> negativeIds = AddDatasets(negativeDatasets).Select(log => log.Id).ToList();
            subjectDataset.RemoveAll(log => negativeIds.Contains(log.Id));
            return subjectDataset;
        }

        private static List<Log> FindMatches(List<List<Log>> datasets)
        {
            static List<List<Log>> FilterMatches(List<List<Log>> datasets)
            {
                List<List<Log>> matches = new List<List<Log>>();
                for (int count = 0; count < datasets.Count - 1; count++)
                {
                    matches.Add(new List<Log>());
                    foreach (var firstData in datasets[count])
                    {
                        foreach (var comparisonData in datasets[count + 1])
                        {
                            if (firstData.Id == comparisonData.Id)
                            {
                                matches[count].Add(firstData);
                                break;
                            }
                        }
                    }
                }
                if (matches.Count == 1)
                {
                    return matches;
                }
                else
                {
                    return FilterMatches(matches);
                }
            }
            return SortAscending(RemoveDuplicates(FilterMatches(datasets)[0]));
        }

        private static List<Log> SortAscending(List<Log> dataset)
        {
            return new List<Log>(dataset.OrderBy(log => log.Id).ToList());
        }

        private static List<Log> RemoveDuplicates(List<Log> dataset)
        {
            return new List<Log>(dataset.Distinct().ToList());
        }
    }
}
