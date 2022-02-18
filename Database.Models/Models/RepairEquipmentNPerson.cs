using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairEquipmentNPerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RepairEquipmentId { get; set; }
    }
}
