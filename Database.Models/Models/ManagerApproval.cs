using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ManagerApproval
    {
        public ManagerApproval()
        {
            FaultImprove = new HashSet<FaultImprove>();
            InverseParent = new HashSet<ManagerApproval>();
            ManagerApprovalInbox = new HashSet<ManagerApprovalInbox>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long? SourceId { get; set; }
        public long ExpandId { get; set; }
        public string Status { get; set; }
        public int PersonId { get; set; }
        public int Level { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Memo { get; set; }

        public virtual Expand Expand { get; set; }
        public virtual ManagerApproval Parent { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<FaultImprove> FaultImprove { get; set; }
        public virtual ICollection<ManagerApproval> InverseParent { get; set; }
        public virtual ICollection<ManagerApprovalInbox> ManagerApprovalInbox { get; set; }
    }
}
