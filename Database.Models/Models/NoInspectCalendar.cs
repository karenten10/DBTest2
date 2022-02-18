using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class NoInspectCalendar
    {
        public NoInspectCalendar()
        {
            NoInspectCalendarNpatrolPlace = new HashSet<NoInspectCalendarNpatrolPlace>();
        }

        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Remaker { get; set; }

        public virtual ICollection<NoInspectCalendarNpatrolPlace> NoInspectCalendarNpatrolPlace { get; set; }
    }
}
