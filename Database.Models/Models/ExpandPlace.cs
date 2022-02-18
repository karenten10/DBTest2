using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ExpandPlace
    {
        public long Id { get; set; }
        public string IsCompleted { get; set; }
        public int PatrolPathId { get; set; }
        public long ExpandId { get; set; }
        public int PatrolPlaceId { get; set; }
        public DateTime? ServerUpdateTime { get; set; }

        public virtual Expand Expand { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
        public virtual Checkin Checkin { get; set; }
    }
}
