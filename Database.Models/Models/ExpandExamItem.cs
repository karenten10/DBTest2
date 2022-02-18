using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ExpandExamItem
    {
        public ExpandExamItem()
        {
            ManagerApprovalDetail = new HashSet<ManagerApprovalDetail>();
            OutComeHistory = new HashSet<OutComeHistory>();
        }

        public long Id { get; set; }
        public string IsCompleted { get; set; }
        public int PatrolPathId { get; set; }
        public long ExpandId { get; set; }
        public int EquipmentExamItemId { get; set; }

        public virtual EquipmentExamItem EquipmentExamItem { get; set; }
        public virtual Expand Expand { get; set; }
        public virtual OutCome OutCome { get; set; }
        public virtual ICollection<ManagerApprovalDetail> ManagerApprovalDetail { get; set; }
        public virtual ICollection<OutComeHistory> OutComeHistory { get; set; }
    }
}
