using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentExamItem
    {
        public EquipmentExamItem()
        {
            ExpandExamItem = new HashSet<ExpandExamItem>();
            FaultImproveDetail = new HashSet<FaultImproveDetail>();
            PatrolPathPeriodNexamItem = new HashSet<PatrolPathPeriodNexamItem>();
            VirtualEquipmentExamItem = new HashSet<VirtualEquipmentExamItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int EquipmentId { get; set; }
        public string ExamConditionName { get; set; }
        public int? OrderId { get; set; }
        public decimal? UpperLimit { get; set; }
        public decimal? LowerLimit { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string DiffJudgeStardand { get; set; }
        public decimal? Value1 { get; set; }
        public decimal? Value2 { get; set; }
        public string Unit { get; set; }
        public int PatrolPlaceId { get; set; }
        public string Sinspection { get; set; }
        public string ExamMethod { get; set; }
        public int? Score { get; set; }
        public string Status { get; set; }
        public string WarningMessage { get; set; }
        public Guid? Guid { get; set; }
        public bool? IsSync { get; set; }
        public int? Reference { get; set; }
        public string MustPhoto { get; set; }
        public int? WorkTypeId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual PatrolPlace PatrolPlace { get; set; }
        public virtual WorkType WorkType { get; set; }
        public virtual ICollection<ExpandExamItem> ExpandExamItem { get; set; }
        public virtual ICollection<FaultImproveDetail> FaultImproveDetail { get; set; }
        public virtual ICollection<PatrolPathPeriodNexamItem> PatrolPathPeriodNexamItem { get; set; }
        public virtual ICollection<VirtualEquipmentExamItem> VirtualEquipmentExamItem { get; set; }
    }
}
