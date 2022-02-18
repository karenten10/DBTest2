using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolGroup
    {
        public PatrolGroup()
        {
            PatrolGroupNpath = new HashSet<PatrolGroupNpath>();
            WorkSchedule = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Status { get; set; }

        public virtual ICollection<PatrolGroupNpath> PatrolGroupNpath { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedule { get; set; }
    }
}
