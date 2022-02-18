using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class DepartmentAdapterModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "必須要輸入值")]
        public string DepartmentName { get; set; }
        public int? ParentId { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string ParentDepartmentName { get; set; }
        public int? UnreportUsers { get; set; }
    }
}
