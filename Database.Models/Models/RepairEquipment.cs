using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int RepairEquipmentGroupId { get; set; }
    }
}
