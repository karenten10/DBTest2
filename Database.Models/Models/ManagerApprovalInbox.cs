using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ManagerApprovalInbox
    {
        public long Id { get; set; }
        public long ManagerApprovalId { get; set; }
        public int PersonId { get; set; }
        public string IsApproval { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ManagerApproval ManagerApproval { get; set; }
        public virtual Person Person { get; set; }
    }
}
