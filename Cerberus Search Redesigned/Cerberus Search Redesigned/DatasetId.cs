using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public class DatasetId //DONE
    {
        public int Id { get; private set; }
        public List<HXT264Log> Dataset { get; private set; }
        public DatasetId(int id, List<HXT264Log> dataset)
        {
            Id = id;
            Dataset = dataset;
        }
    }
}
