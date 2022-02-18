using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPlaceAdapterModel
    {
        public int Id { get; set; }
        public int PatrolScopeId { get; set; }
        [Required(ErrorMessage = "名稱欄位必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "是否停用欄位必須要輸入值")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "編碼欄位必須要輸入值")]
        public string Code { get; set; }
        public dynamic Longitude { get; set; }
        public dynamic Latitude { get; set; }
        public string PatrolScopeName { get; set; }
        public string strLongitude { get; set; }
        public string strLatitude { get; set; }
    }
}
