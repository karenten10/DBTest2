using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ContractorShift
    {
        public ContractorShift()
        {
            AttendanceRegister = new HashSet<AttendanceRegister>();
            ShiftSchedulingRulesContractorShiftId1Navigation = new HashSet<ShiftSchedulingRules>();
            ShiftSchedulingRulesContractorShiftId2Navigation = new HashSet<ShiftSchedulingRules>();
            WorkLog = new HashSet<WorkLog>();
        }

        public int Id { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? BeginTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? Hours { get; set; }
        public string CrossDay { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
        public virtual ICollection<ShiftSchedulingRules> ShiftSchedulingRulesContractorShiftId1Navigation { get; set; }
        public virtual ICollection<ShiftSchedulingRules> ShiftSchedulingRulesContractorShiftId2Navigation { get; set; }
        public virtual ICollection<WorkLog> WorkLog { get; set; }
    }
}
