using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PadMessageAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "「常用語」為必填")]
        public string Message { get; set; }
        [Required(ErrorMessage = "「部門」為必填")]
        public long? DepartmentId { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string DepartmentName { get; set; }

    }
}
