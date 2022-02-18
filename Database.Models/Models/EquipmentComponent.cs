using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentComponent
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string PartName { get; set; }
        public string Component { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public string MaterialNo { get; set; }
        public string ChangeRecord { get; set; }
        public string ImageNo { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
