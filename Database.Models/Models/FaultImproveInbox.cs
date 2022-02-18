using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FaultImproveInbox
    {
        public long Id { get; set; }
        public long FaultImproveId { get; set; }
        public int PersonId { get; set; }
        public string IsApproval { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual FaultImprove FaultImprove { get; set; }
        public virtual Person Person { get; set; }
    }
}
