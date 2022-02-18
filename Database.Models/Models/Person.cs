using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Person
    {
        public Person()
        {
            AttendanceRegister = new HashSet<AttendanceRegister>();
            CanMessage = new HashSet<CanMessage>();
            EquipmentBasicMaintenPersonNavigation = new HashSet<EquipmentBasic>();
            EquipmentBasicOwnerPersonNavigation = new HashSet<EquipmentBasic>();
            EquipmentFixCreatePersonNavigation = new HashSet<EquipmentFix>();
            EquipmentFixToPersonNavigation = new HashSet<EquipmentFix>();
            EquipmentMainten = new HashSet<EquipmentMainten>();
            FaultImprove = new HashSet<FaultImprove>();
            FaultImproveInbox = new HashSet<FaultImproveInbox>();
            HandyDetail = new HashSet<HandyDetail>();
            HandyMaster = new HashSet<HandyMaster>();
            ManagerApproval = new HashSet<ManagerApproval>();
            ManagerApprovalInbox = new HashSet<ManagerApprovalInbox>();
            PersonManager = new HashSet<PersonManager>();
            ShiftSchedulingRulesPeople = new HashSet<ShiftSchedulingRulesPeople>();
            WorkTypePeople = new HashSet<WorkTypePeople>();
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? JobTitleId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public long? AuthorityId { get; set; }
        public DateTime? PasswordUpdateTime { get; set; }
        public int? LastUpdateUser { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Address { get; set; }
        public string WorkingType { get; set; }
        public string Qualifications { get; set; }
        public string Education { get; set; }
        public string SchoolDepartment { get; set; }
        public string Experience { get; set; }
        public string License { get; set; }
        public string Memo { get; set; }
        public string ContractorName { get; set; }

        public virtual Authority Authority { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
        public virtual ICollection<CanMessage> CanMessage { get; set; }
        public virtual ICollection<EquipmentBasic> EquipmentBasicMaintenPersonNavigation { get; set; }
        public virtual ICollection<EquipmentBasic> EquipmentBasicOwnerPersonNavigation { get; set; }
        public virtual ICollection<EquipmentFix> EquipmentFixCreatePersonNavigation { get; set; }
        public virtual ICollection<EquipmentFix> EquipmentFixToPersonNavigation { get; set; }
        public virtual ICollection<EquipmentMainten> EquipmentMainten { get; set; }
        public virtual ICollection<FaultImprove> FaultImprove { get; set; }
        public virtual ICollection<FaultImproveInbox> FaultImproveInbox { get; set; }
        public virtual ICollection<HandyDetail> HandyDetail { get; set; }
        public virtual ICollection<HandyMaster> HandyMaster { get; set; }
        public virtual ICollection<ManagerApproval> ManagerApproval { get; set; }
        public virtual ICollection<ManagerApprovalInbox> ManagerApprovalInbox { get; set; }
        public virtual ICollection<PersonManager> PersonManager { get; set; }
        public virtual ICollection<ShiftSchedulingRulesPeople> ShiftSchedulingRulesPeople { get; set; }
        public virtual ICollection<WorkTypePeople> WorkTypePeople { get; set; }
    }
}
