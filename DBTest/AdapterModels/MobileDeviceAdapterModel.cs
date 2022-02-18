using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class MobileDeviceAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "欄位必須要輸入值")]
        public string Code { get; set; }
        [Required(ErrorMessage = "欄位必須要輸入值")]
        public string Mac { get; set; }
        public string Remark { get; set; }
    }
}
