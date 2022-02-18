using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WorkingPlanCollection
    {
        public long Id { get; set; }
        public int EquipmentId { get; set; }
        public long ExpandId { get; set; }
        public DateTime WorkDate { get; set; }
        public string Remark { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Expand Expand { get; set; }
    }
}
