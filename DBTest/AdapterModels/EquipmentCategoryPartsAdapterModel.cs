using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentCategoryPartsAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "關聯鍵值必須要存在")]
        public int EquipmentCategoryId { get; set; }
        [Required(ErrorMessage = "組件名稱必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "規格必須要輸入值")]
        public string Specification { get; set; }
        [Required(ErrorMessage = "材料編號必須要輸入值")]
        public string MaterialNo { get; set; }

        public string EquipmentCategoryChineseName { get; set; }

    }
}
