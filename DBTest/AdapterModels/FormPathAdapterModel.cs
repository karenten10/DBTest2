using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class FormPathAdapterModel
    {
        public long Id { get; set; }
        public int PatrolPathId { get; set; }
        public int FormReportId { get; set; }
        [Required(ErrorMessage = "請選擇路線")]
        public string PatrolPathName { get; set; }
        [Required(ErrorMessage = "請選擇表單模組")]
        public string FormReportName { get; set; }
        public string FormReportCode { get; set; }
        public PatrolPathAdapterModel PatrolPath { get; set; }
        public FormReportAdapterModel FormReport { get; set; }
    }
}
