using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class NoInspectCalendarNpatrolPlace
    {
        public int Id { get; set; }
        public int NoInspectCalendarId { get; set; }
        public int PatrolPlaceId { get; set; }

        public virtual NoInspectCalendar NoInspectCalendar { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
    }
}
