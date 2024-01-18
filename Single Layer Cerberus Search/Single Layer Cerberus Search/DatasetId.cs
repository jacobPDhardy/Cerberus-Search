namespace Single_Layer_Cerberus_Search
{
    public class DatasetId
    {
        public int Id { get; private set; }

        public List<Log> Dataset { get; private set; }

        public DatasetId(int id,List<Log> dataset)
        {
            Id = id;
            Dataset = dataset;
        }
    }
}
