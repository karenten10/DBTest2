using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentBasic
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EnglishName { get; set; }
        public string EquipmentStatus { get; set; }
        public string Spec { get; set; }
        public string MaintenRemark { get; set; }
        public string EquipmentValue { get; set; }
        public long? OwnerDepartment { get; set; }
        public int? OwnerPerson { get; set; }
        public long? MaintenDepartment { get; set; }
        public int? MaintenPerson { get; set; }
        public string Manufacturer { get; set; }
        public string Capacity { get; set; }
        public string SerialNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
        public int? WorkingHour { get; set; }
        public string Remark { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public DateTime? LastMaintainDate { get; set; }
        public int? MaintainCycleYear { get; set; }
        public int? MaintainCycleMonth { get; set; }
        public int? MaintainCycleDay { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Department MaintenDepartmentNavigation { get; set; }
        public virtual Person MaintenPersonNavigation { get; set; }
        public virtual Department OwnerDepartmentNavigation { get; set; }
        public virtual Person OwnerPersonNavigation { get; set; }
    }
}
