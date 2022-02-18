using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class RepairEquipmentAdapterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public int RepairEquipmentGroupId { get; set; }
        public string RepairEquipmentGroupName { get; set; }
    }
}
