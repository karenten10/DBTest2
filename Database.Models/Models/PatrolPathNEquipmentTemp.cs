using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPathNequipmentTemp
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int EquipmentTemplateId { get; set; }

        public virtual PatrolPath PatrolPath { get; set; }
        public virtual EquipmentTemplate EquipmentTemplate { get; set; }
    }
}
