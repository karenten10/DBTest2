using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WorkType
    {
        public WorkType()
        {
            AttendanceRegister = new HashSet<AttendanceRegister>();
            EquipmentExamItem = new HashSet<EquipmentExamItem>();
            WorkLog = new HashSet<WorkLog>();
            WorkTypePeople = new HashSet<WorkTypePeople>();
        }

        public int Id { get; set; }
        public string JobName { get; set; }
        public decimal? WorkingHours { get; set; }
        public string Remark { get; set; }
        public int? JobTypeId { get; set; }
        public string WorkingDay { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
        public virtual ICollection<EquipmentExamItem> EquipmentExamItem { get; set; }
        public virtual ICollection<WorkLog> WorkLog { get; set; }
        public virtual ICollection<WorkTypePeople> WorkTypePeople { get; set; }
    }
}
