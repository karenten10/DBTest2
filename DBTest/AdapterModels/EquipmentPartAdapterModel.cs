using Syncfusion.Blazor.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentPartAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string PositionName { get; set; }
        public int PartId { get; set; }
        public int OldPartId { get; set; }
        public int Qty { get; set; }
    }
}
