using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "名稱欄位必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用欄位必須要輸入值")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string InspectByOrder { get; set; }
        public string InspectByPda { get; set; }
        public string TurnOnGps { get; set; }
        public string ShowRemark { get; set; }
        public string RealTimeTrans { get; set; }
        [Required(ErrorMessage = "部門欄位必須要輸入值")]
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }  
    }
}
