using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class StrengthenInspection
    {
        public int Id { get; set; }
        public int EquipmentExamItemId { get; set; }
        public DateTime CreaetTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
