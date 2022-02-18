using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class VirtualEquipmentExamItem
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int EquipmentExamId { get; set; }
        public string Name { get; set; }
        public int EquipmentExamItemId { get; set; }
        public int OrderId { get; set; }
        public int ExamConditionNameId { get; set; }
        public string Status { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentExam EquipmentExam { get; set; }
        public virtual EquipmentExamItem EquipmentExamItem { get; set; }
    }
}
