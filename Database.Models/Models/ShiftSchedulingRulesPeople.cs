using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ShiftSchedulingRulesPeople
    {
        public int Id { get; set; }
        public int ShiftSchedulingRulesId { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ShiftSchedulingRules ShiftSchedulingRules { get; set; }
    }
}
