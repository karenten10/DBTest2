using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class NoInspectCalendarAdapterModel
    {
        public int Id { get; set; }
        public int? PatrolPathId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? UpdateUser { get; set; }
        public string UserName { get; set; }

        public DateTime? UpdateTime { get; set; }
        public string Remaker { get; set; }
        public int? PathPeriodId { get; set; }
        public int PlaceCount { get; set; }
    }
}
