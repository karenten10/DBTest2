using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentExam
    {
        public EquipmentExam()
        {
            VirtualEquipmentExamItem = new HashSet<VirtualEquipmentExamItem>();
        }

        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int PatrolPathId { get; set; }
        public string Status { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
        public virtual ICollection<VirtualEquipmentExamItem> VirtualEquipmentExamItem { get; set; }
    }
}
