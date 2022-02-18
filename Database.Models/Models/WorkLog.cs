using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WorkLog
    {
        public int Id { get; set; }
        public DateTime WorkLogDate { get; set; }
        public int WorkTypeId { get; set; }
        public int ContractorShiftId { get; set; }
        public string WorkingArea { get; set; }
        public string WorkingContent { get; set; }
        public string Remark { get; set; }

        public virtual ContractorShift ContractorShift { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
