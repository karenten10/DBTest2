using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class VirtualEquipmentExamItemAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int? EquipmentExamItemId { get; set; }
        public int EquipmentExamId { get; set; }
        [Required(ErrorMessage = "名稱欄位必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用 欄位不可為空白")]
        [StringLength(1, ErrorMessage = "是否停用 長度不可超過 1 個字元")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string EquipmentName { get; set; }
        public int OrderId { get; set; }
    }
}
