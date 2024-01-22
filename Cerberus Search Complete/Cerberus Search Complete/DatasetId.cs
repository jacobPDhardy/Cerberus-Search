namespace Cerberus_Search_Complete
{
    public class DatasetId //DONE
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
