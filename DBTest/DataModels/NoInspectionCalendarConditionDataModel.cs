using System;
using System.Collections.Generic;

namespace InspectionBlazor.DataModels
{
    public partial class NoInspectionCalendarConditionDataModel
    {
        public int Id { get; set; }
        public int? PatrolPathId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Remaker { get; set; }
        public int? PathPeriodId { get; set; }
        public int PlaceCount { get; set; }
    }
}
