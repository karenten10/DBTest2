using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathPeriodNexamItem
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        public int EquipmentExamItemId { get; set; }

        public virtual EquipmentExamItem EquipmentExamItem { get; set; }
        public virtual PatrolPathPeriod PatrolPathPeriod { get; set; }
    }
}
