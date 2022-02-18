using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolScope
    {
        public PatrolScope()
        {
            PatrolPathScope = new HashSet<PatrolPathScope>();
            PatrolPlace = new HashSet<PatrolPlace>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public virtual ICollection<PatrolPathScope> PatrolPathScope { get; set; }
        public virtual ICollection<PatrolPlace> PatrolPlace { get; set; }
    }
}
