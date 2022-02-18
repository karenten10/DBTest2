using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathPeriod
    {
        public PatrolPathPeriod()
        {
            Expand = new HashSet<Expand>();
            PatrolPathPeriodNexamItem = new HashSet<PatrolPathPeriodNexamItem>();
            PatrolPathPeriodNplace = new HashSet<PatrolPathPeriodNplace>();
            PlaceStayTime = new HashSet<PlaceStayTime>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Crontab { get; set; }
        public string Shift { get; set; }
        public string DaysOfMonth { get; set; }
        public string DaysOfWeek { get; set; }
        public DateTime? BeginDay { get; set; }
        public DateTime? LastDay { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PatrolPathId { get; set; }
        public string Cycle { get; set; }
        public int? DayOfMonth { get; set; }
        public int? RangeBeginMonth { get; set; }
        public int? RangeBeginDay { get; set; }
        public int? RangeEndMonth { get; set; }
        public int? RangeEndDay { get; set; }
        public int? WeekRangeBegin { get; set; }
        public int? WeekRangeEnd { get; set; }
        public string Status { get; set; }
        public int? SortId { get; set; }
        public string Rule1 { get; set; }
        public string Rule2 { get; set; }
        public string TimeLimit { get; set; }
        public string FillUnKeyin { get; set; }
        public int? TipNotify { get; set; }
        public int? UnTransNotify { get; set; }

        public virtual PatrolPath PatrolPath { get; set; }
        public virtual ICollection<Expand> Expand { get; set; }
        public virtual ICollection<PatrolPathPeriodNexamItem> PatrolPathPeriodNexamItem { get; set; }
        public virtual ICollection<PatrolPathPeriodNplace> PatrolPathPeriodNplace { get; set; }
        public virtual ICollection<PlaceStayTime> PlaceStayTime { get; set; }
    }
}
