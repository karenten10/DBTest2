using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentPart
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string PositionName { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }


        public virtual PartInfo Part { get; set; }
    }
}
