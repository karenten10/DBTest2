using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class InspectMapModel
    {
       public int PlaceId { get; set; }
       public string Location { get; set; }
       public string Status { get; set; }
       public int EquipmentId { get; set; }
       public string EquipmentName { get; set; }
       public string EquipmentPhoto { get; set; }
       public string IsPhoto { get; set; }
       
    }

    public class InspectMapDailyInspctModel
    {
        public int PlaceId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string IsPhoto { get; set; }
        public string EquipmentExamItem { get; set; }
        public long ExpandEquipmentExamItemId { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Value { get; set; }
        public string UserName { get; set; }

    }
}
