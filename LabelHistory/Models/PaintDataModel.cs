using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.Models
{
    public class PaintDataModel
    {
        //from paint exit
        public long PaintExitID { get; set; }
        public string PaintBarcode { get; set; }
        public DateTime PaintLabelCreatedAt { get; set; }
        public DateTime PaintExitStatusChangedAt { get; set; }
        public int StyleNumber { get; set; }
        public string StyleDescription { get; set; }
        public int ColorNumber { get; set; }
        public string ColorDescription { get; set; }
        public string ScanType { get; set; }
        public string PrintSide { get; set; }
        public string PaintCarrier { get; set; }
        public int PaintCarrierPosition { get; set; }
        public int PaintRound { get; set; }
        public string PaintExitScanStatus { get; set; }

        //FROM [Production].[dbo].[styles_xref]
        public string RawPartNumber { get; set; }
        public string Program { get; set; }
        public string Set { get; set; }
        public int PartsPerCarrier { get; set; }
    }
    public class PaintHistory
    {
        public string PaintCarrier { get; set; }
        public int PaintCarrierPosition { get; set; }
        public int PaintRound { get; set; }
        public string PaintExitScanStatus { get; set; }
        public DateTime PaintExitStatusChangedAt { get; set; }
    }
}
