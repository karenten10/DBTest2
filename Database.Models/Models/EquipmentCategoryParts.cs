using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentCategoryParts
    {
        public int Id { get; set; }
        public int EquipmentCategoryId { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string MaterialNo { get; set; }

        public virtual EquipmentCategory EquipmentCategory { get; set; }
    }
}
