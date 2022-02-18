using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels {
    public class MyManagerApprovalModel {
        public string ManagerApprovalGuid { get; set; }
        public long ExpandExamItemId { get; set; }
        public int PatrolPlaceId { get; set; }
        public int? TreeGridParentId { get; set; }
        public string PatrolPlaceName { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int ExamItemId { get; set; } 
        public string ExamItemName { get; set; }
        public string ExamConditionName { get; set; }
        public decimal? Value1 { get; set; }
        public decimal? Value2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public decimal? ReturnValue1 { get; set; }
        public string ReturnValue2 { get; set; }
        public string ReturnRemark { get; set; }
        public string ApprovalRemark { get; set; }
        public string DoubleCheck { get; set; }
        public string IsAbnormal { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string 審核依據 { get; set; }
        public string 紀錄數值 { get; set; }
        public string 唯一ID { get; set; }
        public bool? 加強巡檢 { get; set; }
        public long ManagerApprovalDetailId { get; set; }
        public string 開立部門 { get; set; } = "";

        public string DisplayValue { get; set; } // 巡檢數值 (異常數值)

        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }
    }
   
}
