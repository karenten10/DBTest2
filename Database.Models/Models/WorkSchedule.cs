using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WorkSchedule
    {
        public long Id { get; set; }
        public DateTime WorkDate { get; set; }
        public int GroupId { get; set; }
        public int PathId { get; set; }
        public int PlaceId { get; set; }
        public int EquipmentId { get; set; }

        public virtual PatrolGroup PatrolGroup { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
        public virtual Equipment Equipment { get; set; }
        
    }
}
