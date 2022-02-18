using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    using System.ComponentModel.DataAnnotations;
    public class ExamConditionNameAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "區段名稱 欄位不可為空白")]
        [StringLength(50, ErrorMessage = "區段名稱 不可超過 50 個字")]
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用 欄位不可為空白")]
        [StringLength(1, ErrorMessage = "是否停用 長度不可超過 1 個字")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public int? LastModifyPersonId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? FactoryId { get; set; }
    }
}
