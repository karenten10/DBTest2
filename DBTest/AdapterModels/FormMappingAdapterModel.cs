using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class FormMappingAdapterModel
    {
        public long Id { get; set; }
        public int EquipmentId { get; set; }
        public int FormReportId { get; set; }
        public string EquipmentName { get; set; }
        public string FormReportName { get; set; }
        public string FormReportCode { get; set; }
        public EquipmentAdapterModel Equipment { get; set; }
        public FormReportAdapterModel FormReport { get; set; }
    }
}
