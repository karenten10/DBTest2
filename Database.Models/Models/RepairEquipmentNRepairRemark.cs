using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairEquipmentNRepairRemark
    {
        public int Id { get; set; }
        public int RepairRemakerId { get; set; }
        public int RepairEquipmentId { get; set; }
    }
}
