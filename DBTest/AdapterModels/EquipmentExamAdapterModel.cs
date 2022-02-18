using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentExamAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        [Required(ErrorMessage = "Equipment 關聯鍵值必須要存在")]
        public string EquipmentName { get; set; }
        public int PatrolPathId { get; set; }
        [Required(ErrorMessage = "請先「選取巡檢路線」")]
        public string PatrolPathName { get; set; }
        [Required(ErrorMessage = "是否停用 欄位不可為空白")]
        [StringLength(1, ErrorMessage = "是否停用 長度不可超過 1 個字元")]
        public string Status { get; set; }
        public string StatusName { get; set; }

    }
}
