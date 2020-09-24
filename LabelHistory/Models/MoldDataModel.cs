using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.Models
{
    public class MoldDataModel
    {
        public long UniqueRefenceID { get; set; }
        public string MoldBarcode { get; set; }
        public string PartNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Press { get; set; }
        public int StatusCode { get; set; }
        public string MachineId { get; set; }
        public Guid TranVersion { get; set; }
        public Guid RowGuid { get; set; }
    }
}
