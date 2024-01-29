using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public static class GateSolver
    {
        public static async Task<List<HXT264Log>> AutoSolve(List<List<HXT264Log>> datasets, string @operator, bool not = false)
        {
            List<HXT264Log> result = new List<HXT264Log>();
            if (@operator == Gates.AND)
            {
                result = AND(datasets);
            }
            else if (@operator == Gates.OR)
            {
                result = OR(datasets);
            }
            else if (@operator == Gates.XOR)
            {
                result = XOR(datasets);
            }
            else if (string.IsNullOrEmpty(@operator))
            {
                return datasets.First();
            }

            if (not)
            {
                List<HXT264Log> globalDataset = await LogsDatabase.GetAllLogs();
                result = NOT(globalDataset, result);
            }
            return result;
        }

        public static List<HXT264Log> OR(List<List<HXT264Log>> datasets)
        {
            return new List<HXT264Log>(SortAscending(AddDatasets(datasets)));
        }

        public static List<HXT264Log> AND(List<List<HXT264Log>> datasets)
        {
            return new List<HXT264Log>(SortAscending(FindMatches(datasets)));
        }

        public static List<HXT264Log> XOR(List<List<HXT264Log>> datasets)
        {
            List<DatasetId> datasetIdentities = new List<DatasetId>();

            List<HXT264Log> GetDataset(int id)
            {
                return datasetIdentities.Where(dataset => dataset.Id == id).Select(datasetId => datasetId.Dataset).FirstOrDefault() ?? new List<HXT264Log>();
            }

            int idCount = 0;
            foreach (var dataset in datasets)
            {
                datasetIdentities.Add(new DatasetId(idCount, dataset));
                idCount++;
            }

            List<DatasetIdCombo> uniqueCombos = new List<DatasetIdCombo>();
            for (int count = 0; count < datasetIdentities.Count; count++)
            {
                List<int> remainingIds = datasetIdentities.Select(datasetId => datasetId.Id).Where(id => id != count).ToList();
                foreach (var remainingId in remainingIds)
                {
                    DatasetIdCombo identityMatch = new DatasetIdCombo(count, remainingId);
                    if (!uniqueCombos.Any(combo => combo.CompareIdentityMatch(identityMatch)) || uniqueCombos.Count == 0)
                    {
                        uniqueCombos.Add(identityMatch);
                    }
                }
            }

            List<List<HXT264Log>> allMatches = new List<List<HXT264Log>>();
            foreach (var combo in uniqueCombos)
            {
                allMatches.Add(FindMatches(new List<List<HXT264Log>>() { GetDataset(combo.PrimaryValue), GetDataset(combo.SecondaryValue) }));
            }

            List<HXT264Log> result = SortAscending(SubtractDatasets(AddDatasets(datasets), new List<List<HXT264Log>>() { RemoveDuplicates(AddDatasets(allMatches)) }));

            return result;
        }

        public static List<HXT264Log> NOT(List<HXT264Log> globalDataset, List<HXT264Log> dataset)
        {
            return SortAscending(SubtractDatasets(globalDataset, new List<List<HXT264Log>>() { dataset }));
        }


        private static List<HXT264Log> AddDatasets(List<List<HXT264Log>> datasets)
        {
            List<HXT264Log> result = new List<HXT264Log>();
            foreach (var dataset in datasets)
            {
                foreach (var data in dataset)
                {
                    result.Add(data);
                }
            }
            return RemoveDuplicates(result);
        }

        private static List<HXT264Log> SubtractDatasets(List<HXT264Log> subjectDataset, List<List<HXT264Log>> negativeDatasets)
        {
            List<long> negativeIds = AddDatasets(negativeDatasets).Select(log => log.Id).ToList();
            subjectDataset.RemoveAll(log => negativeIds.Contains(log.Id));
            return subjectDataset;
        }

        private static List<HXT264Log> FindMatches(List<List<HXT264Log>> datasets)
        {
            static List<List<HXT264Log>> FilterMatches(List<List<HXT264Log>> datasets)
            {
                List<List<HXT264Log>> matches = new List<List<HXT264Log>>();
                for (int count = 0; count < datasets.Count - 1; count++)
                {
                    matches.Add(new List<HXT264Log>());
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
            return RemoveDuplicates(FilterMatches(datasets).First());
        }

        private static List<HXT264Log> SortAscending(List<HXT264Log> dataset)
        {
            return new List<HXT264Log>(dataset.OrderBy(log => log.Id).ToList());
        }

        private static List<HXT264Log> RemoveDuplicates(List<HXT264Log> dataset)
        {
            return new List<HXT264Log>(dataset.Distinct().ToList());
        }
    }
}
