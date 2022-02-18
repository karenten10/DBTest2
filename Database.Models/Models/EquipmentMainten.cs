using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentMainten
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public DateTime? MaintenDate { get; set; }
        public string MaintenPeriod { get; set; }
        public string MaintenPath { get; set; }
        public string MaintenScope { get; set; }
        public string MaintenSpot { get; set; }
        public string MaintenEquipment { get; set; }
        public string MaintenItem { get; set; }
        public string MaintenStandard { get; set; }
        public string MaintenValue { get; set; }
        public int? MaintenPerson { get; set; }
        public DateTime? RecordTime { get; set; }
        public string RecordAudit { get; set; }
        public string MaintenPicture { get; set; }
        public string MaintenPersonName { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Person MaintenPersonNavigation { get; set; }
    }
}
