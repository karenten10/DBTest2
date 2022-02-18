using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Checkin
    {
        public long ExpandPlaceId { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }
        public string IsCompleted { get; set; }
        public int PatrolPathId { get; set; }
        public long ExpandId { get; set; }
        public bool IsInspection { get; set; }
        public DateTime? UpdateTime { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public virtual Expand Expand { get; set; }
        public virtual ExpandPlace ExpandPlace { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
    }
}
