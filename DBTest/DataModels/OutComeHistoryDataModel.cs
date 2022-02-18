using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class OutComeHistoryDataModel
    {
        public decimal? Value { get; set; }
        public string ValueString { get; set; }
        public string MultiChoice { get; set; }
        public string IsAbnormal { get; set; }
        public string EquipmentShutdown { get; set; }
        public string Result { get; set; }
        public int EditUserId { get; set; }
        public string EditUserName { get; set; }
        public DateTime EditTime { get; set; }
    }
}
