using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.Models
{
    public class Part
    {
        public string ImageURL { get; set; }     

        public bool isSingleScan { get; set; }

        public string MoldBarcode { get; set; }
        public string PaintBarcode { get; set; }


        public FinesseDataModel FinesseData { get; set; }
        public RackDataModel RackData { get; set; }
        public MoldDataModel MoldData { get; set; }
        public PartMasterData PartMasterData { get; set; }
        public PaintDataModel PaintData { get; set; }
    }
    public class PartMasterData
    {
        //from Decostar.dbo.pt_mstr_sql
        public string PartNumber { get; set; }
        public string PartDescription1 { get; set; }
        public string PartDescription2 { get; set; }
        public string ProdLine { get; set; }
        public string PartGroup { get; set; }
        public string PartType { get; set; }
        public string PartStatus { get; set; }
        public string PartLocation { get; set; }
        public string PartSite { get; set; }

        //from Decostar.dbo.cp_mstr_sql
        public string FullDescription { get; set; }
        public string OptionDescription { get; set; }
        public string PartTypeDescription { get; set; }
        public string Position { get; set; }
        public string CustomerPartNumber { get; set; }
        public string CPComment { get; set; }
    }
   
    

}
