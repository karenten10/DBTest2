using Syncfusion.Blazor.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class CheckPathAdapterModel
    {
        public DateTime? StartTime { get; set; }//查詢報表時間
        public DateTime? EndTime { get; set; }
        public int? PatrolPathId { get; set; }//路線ID
        public string PatrolPathName { get; set; }
        public int? PatrolPathPeriodId { get; set; }//時段ID
        public string PatrolPathPeriodName { get; set; }
        public int DepartmentId { get; set; }
        public int? FormReportId { get; set; }
    }
}
