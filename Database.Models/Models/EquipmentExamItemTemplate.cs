using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentExamItemTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EquipmentTemplateId { get; set; }
        public string ExamConditionName { get; set; }
        public int? OrderId { get; set; }
        public decimal? UpperLimit { get; set; }
        public decimal? LowerLimit { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public Guid Guid { get; set; }
        public string DiffJudgeStardand { get; set; }
        public decimal? Value1 { get; set; }
        public decimal? Value2 { get; set; }
        public string Unit { get; set; }
        public string Sinspection { get; set; }
        public string ExamMethod { get; set; }
        public int? Score { get; set; }
        public string Status { get; set; }
        public string WarningMessage { get; set; }

        public virtual EquipmentTemplate EquipmentTemplate { get; set; }

       
    }
}
