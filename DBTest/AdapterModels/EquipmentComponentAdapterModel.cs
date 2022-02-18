using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentComponentAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }

        //[Required(ErrorMessage = "欄位必須要輸入值")]
        public string PartName { get; set; }
        public string Component { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public string MaterialNo { get; set; }
        public string ChangeRecord { get; set; }
        public string ImageNo { get; set; }

        public string EquipmentName { get; set; }

        public EquipmentAdapterModel Equipment { get; set; }
    }
}
