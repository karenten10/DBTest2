using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class RepairEquipmentNRepairRemarkAdapterModel
    {
        public int Id { get; set; }
        public int RepairRemakerId { get; set; }
        public string RepairRemakerName { get; set; }
        public int RepairEquipmentId { get; set; }
        public string RepairEquipmentName { get; set; }
    }
}
