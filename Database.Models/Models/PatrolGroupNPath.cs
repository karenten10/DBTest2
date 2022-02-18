using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolGroupNpath
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PatrolPathId { get; set; }

        public virtual PatrolGroup Group { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
    }
}
