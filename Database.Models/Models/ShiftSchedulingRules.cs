using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ShiftSchedulingRules
    {
        public ShiftSchedulingRules()
        {
            ShiftSchedulingRulesPeople = new HashSet<ShiftSchedulingRulesPeople>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string RuleType { get; set; }
        public int ContractorShiftId1 { get; set; }
        public int? ContractorShiftId2 { get; set; }
        public string Remark { get; set; }

        public virtual ContractorShift ContractorShiftId1Navigation { get; set; }
        public virtual ContractorShift ContractorShiftId2Navigation { get; set; }
        public virtual ICollection<ShiftSchedulingRulesPeople> ShiftSchedulingRulesPeople { get; set; }
    }
}
