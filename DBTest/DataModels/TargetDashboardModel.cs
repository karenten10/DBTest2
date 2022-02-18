using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels {
    public class SectionAbnormalModel {
        public string SectionName { get; set; }
        public int AbnormalCount { get; set; }
    }

    public class EquipmentAbnormalModel
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int AbnormalCount { get; set; }
    }

}
