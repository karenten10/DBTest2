using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Department
    {
        public Department()
        {
            ContractEmployees = new HashSet<ContractEmployees>();
            EquipmentBasicMaintenDepartmentNavigation = new HashSet<EquipmentBasic>();
            EquipmentBasicOwnerDepartmentNavigation = new HashSet<EquipmentBasic>();
            EquipmentFix = new HashSet<EquipmentFix>();
            FaultImproveDetail = new HashSet<FaultImproveDetail>();
            PadMessage = new HashSet<PadMessage>();
            PatrolPath = new HashSet<PatrolPath>();
            PersonDepartment = new HashSet<PersonDepartment>();
        }

        public long Id { get; set; }
        public string DepartmentName { get; set; }
        public int ParentId { get; set; }
        public string Status { get; set; }
        public int? UnreportUsers { get; set; }
        public string UserDeptId { get; set; }

        public virtual ICollection<ContractEmployees> ContractEmployees { get; set; }
        public virtual ICollection<EquipmentBasic> EquipmentBasicMaintenDepartmentNavigation { get; set; }
        public virtual ICollection<EquipmentBasic> EquipmentBasicOwnerDepartmentNavigation { get; set; }
        public virtual ICollection<EquipmentFix> EquipmentFix { get; set; }
        public virtual ICollection<FaultImproveDetail> FaultImproveDetail { get; set; }
        public virtual ICollection<PadMessage> PadMessage { get; set; }
        public virtual ICollection<PatrolPath> PatrolPath { get; set; }
        public virtual ICollection<PersonDepartment> PersonDepartment { get; set; }
    }
}
