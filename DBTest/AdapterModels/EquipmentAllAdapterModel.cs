using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentAllAdapterModel
    {
        public int Id { get; set; }
        public string EquipmentIdExt { get; set; }
        public string EquipmentName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string Status { get; set; }
    }
}
