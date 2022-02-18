using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ManagerApprovalDetail
    {
        public long Id { get; set; }
        public string ManagerApprovalGuid { get; set; }
        public long ExpandExamItemId { get; set; }
        public string DoubleCheck { get; set; }
        public string StrengthenInspection { get; set; }
        public string Memo { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual OutCome ExpandExamItem { get; set; }
        public virtual ExpandExamItem ExpandExamItemNavigation { get; set; }
    }
}
