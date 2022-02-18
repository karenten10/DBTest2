using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPlace
    {
        public PatrolPlace()
        {
            Equipment = new HashSet<Equipment>();
            EquipmentExamItem = new HashSet<EquipmentExamItem>();
            ExpandPlace = new HashSet<ExpandPlace>();
            NoInspectCalendarNpatrolPlace = new HashSet<NoInspectCalendarNpatrolPlace>();
            PatrolPathNplace = new HashSet<PatrolPathNplace>();
            PatrolPathPeriodNplace = new HashSet<PatrolPathPeriodNplace>();
            PlaceStayTime = new HashSet<PlaceStayTime>();
            WorkSchedule = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public int PatrolScopeId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public virtual PatrolScope PatrolScope { get; set; }
        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<EquipmentExamItem> EquipmentExamItem { get; set; }
        public virtual ICollection<ExpandPlace> ExpandPlace { get; set; }
        public virtual ICollection<NoInspectCalendarNpatrolPlace> NoInspectCalendarNpatrolPlace { get; set; }
        public virtual ICollection<PatrolPathNplace> PatrolPathNplace { get; set; }
        public virtual ICollection<PatrolPathPeriodNplace> PatrolPathPeriodNplace { get; set; }
        public virtual ICollection<PlaceStayTime> PlaceStayTime { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedule { get; set; }
    }
}
