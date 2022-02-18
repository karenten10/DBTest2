using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class RepairEquipmentNPersonAdapterModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int RepairEquipmentId { get; set; }
        public string RepairEquipmentName { get; set; }
    }
}
