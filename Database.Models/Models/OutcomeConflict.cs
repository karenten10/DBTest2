using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class OutcomeConflict
    {
        public long Id { get; set; }
        public long ExpandExamItemId { get; set; }
        public decimal? Value { get; set; }
        public string ValueString { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }
        public string MultiChoice { get; set; }
        public string IsCompleted { get; set; }
        public int PatrolPathId { get; set; }
        public long ExpandId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string IsAbnormal { get; set; }
        public string Remark { get; set; }
        public int PersonId { get; set; }
        public string EquipmentShutdown { get; set; }
        public DateTime? ServerUpdateTime { get; set; }

        public virtual Expand Expand { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
    }
}
