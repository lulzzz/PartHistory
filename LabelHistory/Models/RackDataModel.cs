using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.Models
{
    public class RackDataModel
    {

        public long ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RackCreatorOperatorBadge { get; set; }
        public string RackCreatorOperatorName { get; set; }

        public int Style { get; set; }
        public string PartNumber { get; set; }
        public int Quantity { get; set; }
        public bool Complete { get; set; }
        public int Status { get; set; }
        public string LastLocation { get; set; }
        public string ShipLocation { get; set; }
        public string AppUsed { get; set; }
        public List<RackItem> RackContents { get; set; }
        public List<RackActivityItem> RackActivityLog { get; set; }

    }


    public class RackActivityItem
    {
        public int ID { get; set; }
        public string RackID { get; set; }
        public string Action { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
    }

    public class RackItem
    {
        public long RackItemID { get; set; }
        public string PaintScan { get; set; }
        public string MoldScan { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public long RackID { get; set; }
    }
}
