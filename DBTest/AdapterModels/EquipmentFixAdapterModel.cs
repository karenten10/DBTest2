using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentFixAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        //[Required(ErrorMessage = "欄位必須要輸入值")]
        public string EquipmentName { get; set; } // 這個table有自己的EquipmentName,不是用Equipment.EquipmentName
        public string EquipmentItem { get; set; }
        public int? CreatePerson { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? ToDepartment { get; set; }
        public int? ToPerson { get; set; }
        public DateTime? EstimateDate { get; set; }
        public int? ExpireDay { get; set; }
        public string FixRemark { get; set; }
        public string FixStatus { get; set; }

        public string CreatePersonName { get; set; }

        public string ToDepartmentName { get; set; }

        public string ToPersonName { get; set; }

        public EquipmentAdapterModel Equipment { get; set; }
    }
}
