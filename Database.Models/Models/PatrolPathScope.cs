using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathScope
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolScopeId { get; set; }

        public virtual PatrolPath PatrolPath { get; set; }
        public virtual PatrolScope PatrolScope { get; set; }
    }
}
