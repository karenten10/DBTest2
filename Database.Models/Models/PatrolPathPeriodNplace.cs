using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathPeriodNplace
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        public int PatroPlaceId { get; set; }

        public virtual PatrolPlace PatroPlace { get; set; }
        public virtual PatrolPathPeriod PatrolPathPeriod { get; set; }
    }
}
