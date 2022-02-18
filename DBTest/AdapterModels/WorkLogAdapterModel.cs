using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class WorkLogAdapterModel
    {
        public int Id { get; set; }
        public DateTime WorkLogDate { get; set; }
        public int WorkTypeId { get; set; }
        public int ContractorShiftId { get; set; }
        public string WorkingArea { get; set; }
        public string WorkingContent { get; set; }
        public string Remark { get; set; }

        public string 工種 { get; set; }
        public string 班別 { get; set; }
        public string 工作人員 { get; set; }
        public decimal 工時 { get; set; }
        public int 人數 { get; set; } 

        public virtual ContractorShift ContractorShift { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
