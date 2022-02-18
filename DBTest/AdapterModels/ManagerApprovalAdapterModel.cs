using InspectionBlazor.DataModels;
using System;
using System.Collections.Generic;

namespace InspectionBlazor.AdapterModels {
    public class ManagerApprovalAdapterModel {
        public long Id { get; set; }
        public long ManagerApprovalId { get; set; }
        public int PersonId { get; set; }
        public string IsApproval { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Memo { get; set; }

        public long? ManagerApprovalParentId { get; set; }
        public int ManagerApprovalLevel { get; set; }
        public long? ManagerApprovalSourceId { get; set; }
        public long ManagerApprovalExpandId { get; set; }
        public int PatrolPathId { get; set; }
        public string PatrolPathName { get; set; }
        public int ApprovalPeopleId { get; set; }
        public string ApprovalPeople { get; set; }  
        public string Manager { get; set; }
        public string Status { get; set; }
        public string Guid { get; set; }
        public int MyselfId { get; set; }
        public int 無到位巡檢項目 { get; set; }
        public int 異常巡檢項目 { get; set; }
        public string 是否簽核 { get; set; }

        public string 時段 { get; set; }

        public List<主管簽核全流程Model> 主管簽核全流程 { get; set; }
    }
}
