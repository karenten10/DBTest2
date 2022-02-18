using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InspectionBlazor.DataModels
{
    public class PFMasterQueryConditionDataModel
    {
        public DateTime? Begin { get; set; }//時間
        public DateTime? End { get; set; }//時間
        public int? PartId { get; set; }
        public string FormType { get; set; } 
        public string PartName { get; set; }

    }
}
