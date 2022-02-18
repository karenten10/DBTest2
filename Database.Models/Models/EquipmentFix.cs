using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentFix
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentItem { get; set; }
        public int? CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? ToDepartment { get; set; }
        public int? ToPerson { get; set; }
        public DateTime? EstimateDate { get; set; }
        public int? ExpireDay { get; set; }
        public string FixRemark { get; set; }
        public string FixStatus { get; set; }
        public string CreatePersonName { get; set; }
        public string ToPersonName { get; set; }

        public virtual Person CreatePersonNavigation { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual Department ToDepartmentNavigation { get; set; }
        public virtual Person ToPersonNavigation { get; set; }
    }
}
