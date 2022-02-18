using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WalkthroughShiftSchedule
    {
        public long Id { get; set; }
        public string RuleType { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string ShiftName { get; set; }
        public string DayOfWeek { get; set; }
        public string WeekOfYear { get; set; }
    }
}
