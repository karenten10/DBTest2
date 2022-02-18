using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolScopeAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "名稱欄位必須要輸入值")] 
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用欄位必須要輸入值")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string PatrolPlaceName { get; set; }
    }
}
