using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels {
    public class MyFaultImproveModel {
        public string FaultImproveGuid { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int ExamItemId { get; set; } 
        public string ExamItemName { get; set; }
        public string ApprovalRemark { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long FaultImproveId { get; set; }
    }
}
