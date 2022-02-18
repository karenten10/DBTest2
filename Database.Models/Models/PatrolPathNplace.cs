using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathNplace
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolPlaceId { get; set; }

        public virtual PatrolPath PatrolPath { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
    }
}
