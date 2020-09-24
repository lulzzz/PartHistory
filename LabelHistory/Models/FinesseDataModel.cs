using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.Models
{
    public class FinesseDataModel
    {
        public int RejectID { get; set; }
        public string PaintBarcode { get; set; }
        public string PartNumber { get; set; }
        public string FinesseStatus { get; set; }
        public string FinesseOperatorBadge { get; set; }
        public string FinneseOperatorName { get; set; }
        public DateTime Time { get; set; }
        public int StyleNumber { get; set; }
        public int ColorNumber { get; set; }
        public string ColorDescription { get; set; }
        public string MoldBarcode { get; set; }
        public string FinesseWorkArea { get; set; }
        public string FinessePrinter { get; set; }
        public List<Defect> Defects { get; set; }
    }
    public class Defect
    {
        public int DefectID { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Category { get; set; }
    }

}
