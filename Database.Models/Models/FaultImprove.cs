using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FaultImprove
    {
        public FaultImprove()
        {
            FaultImproveInbox = new HashSet<FaultImproveInbox>();
            InverseParent = new HashSet<FaultImprove>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long? SourceId { get; set; }
        public long? ManagerApprovalId { get; set; }
        public string Status { get; set; }
        public int Level { get; set; }
        public int PersonId { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ManagerApproval ManagerApproval { get; set; }
        public virtual FaultImprove Parent { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<FaultImproveInbox> FaultImproveInbox { get; set; }
        public virtual ICollection<FaultImprove> InverseParent { get; set; }
    }
}
