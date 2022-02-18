using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class InspectionRecordForDashboardAdapterModel
    {
        public string PathName { get; set; }
        public string PeriodName { get; set; }
        public string UserName { get; set; }
        public DateTime 紀錄時間 { get; set; }
    }
}
