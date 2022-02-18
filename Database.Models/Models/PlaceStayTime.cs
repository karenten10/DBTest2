using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PlaceStayTime
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        public int PatroPlaceId { get; set; }
        public int StaySecs { get; set; }

        public virtual PatrolPlace PatroPlace { get; set; }
        public virtual PatrolPathPeriod PatrolPathPeriod { get; set; }
    }
}
