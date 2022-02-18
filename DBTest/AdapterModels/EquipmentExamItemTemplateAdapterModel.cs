using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentExamItemTemplateAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "名稱 欄位不可為空白")]
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用 欄位不可為空白")]
        [StringLength(1, ErrorMessage = "是否停用 長度不可超過 1 個字元")]
        public string Status { get; set; } = "N";
        public string StatusName { get; set; }
        public int EquipmentTemplateId { get; set; }
        [Required(ErrorMessage = "設備範本名稱 必須要存在，請點選問號來選取")]
        public string EquipmentTemplateTitle { get; set; }
        public int OrderId { get; set; }
        public string ExamConditionName { get; set; }
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
        public int PatrolPlaceId { get; set; }
        public string Sinspection { get; set; }
        public string ExamMethod { get; set; }
        public int? Score { get; set; }
        public string WarningMessage { get; set; }
        public int? ExamConditionNameId { get; set; }
        public int? ExamDataRangeId { get; set; }

        public List<ImageRepositoryAdapterModel> ImageRepository { get; set; } = new List<ImageRepositoryAdapterModel>();
    }
}
