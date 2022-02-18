using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class AttendanceRegister
    {
        public long Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int PersonId { get; set; }
        public int? ContractorShiftId { get; set; }
        public string ContractorShiftSegment { get; set; }
        public int? LeaveTypeId { get; set; }
        public string LeaveTypeSegment { get; set; }
        public string Remark { get; set; }
        public int? WorkTypeId { get; set; }

        public virtual ContractorShift ContractorShift { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual Person Person { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
