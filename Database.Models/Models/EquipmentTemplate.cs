using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentTemplate
    {
        public EquipmentTemplate()
        {
            Equipment = new HashSet<Equipment>();
            EquipmentExamItemTemplate = new HashSet<EquipmentExamItemTemplate>();
            PatrolPathNequipmentTemp = new HashSet<PatrolPathNequipmentTemp>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string EquipmentIdExt { get; set; }
        public string EquipmentName { get; set; }
        public string Status { get; set; }
        public string PatrolPathName { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<EquipmentExamItemTemplate> EquipmentExamItemTemplate { get; set; }
        public virtual ICollection<PatrolPathNequipmentTemp> PatrolPathNequipmentTemp { get; set; }
    }
}
