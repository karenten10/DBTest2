using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairManagerApprovalInbox
    {
        public int Id { get; set; }
        public int RepairManagerApprovalId { get; set; }
        public int PersonId { get; set; }
        public string IsApproval { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
