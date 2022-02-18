using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FormMapping
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int FormReportId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual FormReport FormReport { get; set; }
    }
}
