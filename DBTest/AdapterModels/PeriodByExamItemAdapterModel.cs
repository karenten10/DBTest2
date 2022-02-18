using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PeriodByExamItemAdapterModel
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public string EquipmentName { get; set; }
        public string ExamItemName { get; set; }
        public bool InPeriod { get; set; }
        public string SectionName { get; set; }
        public string TemplateName { get; set; }
    }
}
