using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentCategory
    {
        public EquipmentCategory()
        {
            EquipmentCategoryParts = new HashSet<EquipmentCategoryParts>();
        }

        public int Id { get; set; }
        public string ChineseName { get; set; }
        public string EnglishName { get; set; }
        public string EquipmentCategoryExtId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<EquipmentCategoryParts> EquipmentCategoryParts { get; set; }
    }
}
