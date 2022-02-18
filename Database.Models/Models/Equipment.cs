using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            EquipmentBasic = new HashSet<EquipmentBasic>();
            EquipmentComponent = new HashSet<EquipmentComponent>();
            EquipmentExam = new HashSet<EquipmentExam>();
            EquipmentExamItem = new HashSet<EquipmentExamItem>();
            EquipmentFile = new HashSet<EquipmentFile>();
            EquipmentFix = new HashSet<EquipmentFix>();
            EquipmentMainten = new HashSet<EquipmentMainten>();
            FaultImproveDetail = new HashSet<FaultImproveDetail>();
            FormMapping = new HashSet<FormMapping>();
            VirtualEquipmentExamItem = new HashSet<VirtualEquipmentExamItem>();
            WorkingPlanCollection = new HashSet<WorkingPlanCollection>();
            WorkSchedule = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public string EquipmentIdExt { get; set; }
        public string EquipmentName { get; set; }
        public int SectionId { get; set; }
        public string Status { get; set; }
        public int? EquipmentTemplateId { get; set; }
        public int PatrolPlaceId { get; set; }
        public int? OrderId { get; set; }
        public string IsElectricEquipment { get; set; }

        public virtual EquipmentTemplate EquipmentTemplate { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<EquipmentBasic> EquipmentBasic { get; set; }
        public virtual ICollection<EquipmentComponent> EquipmentComponent { get; set; }
        public virtual ICollection<EquipmentExam> EquipmentExam { get; set; }
        public virtual ICollection<EquipmentExamItem> EquipmentExamItem { get; set; }
        public virtual ICollection<EquipmentFile> EquipmentFile { get; set; }
        public virtual ICollection<EquipmentFix> EquipmentFix { get; set; }
        public virtual ICollection<EquipmentMainten> EquipmentMainten { get; set; }
        public virtual ICollection<FaultImproveDetail> FaultImproveDetail { get; set; }
        public virtual ICollection<FormMapping> FormMapping { get; set; }
        public virtual ICollection<VirtualEquipmentExamItem> VirtualEquipmentExamItem { get; set; }
        public virtual ICollection<WorkingPlanCollection> WorkingPlanCollection { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedule { get; set; }
    }
}
