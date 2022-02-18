using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class MyLastPatrolRecord
    {
        public string EquipmentName { get; set; }
        public string EquipmentExamItemName { get; set; }
        public string Value { get; set; }

        public DateTime UpdateTime { get; set; }
        public int xid { get; set; }
    }
}
