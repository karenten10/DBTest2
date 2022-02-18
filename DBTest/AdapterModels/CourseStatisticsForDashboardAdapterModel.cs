using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class CourseStatisticsForDashboardAdapterModel
    {
        public string DepartmentName { get; set; }
        public int 總檢查數 { get; set; }
        public int 正常巡檢次數 { get; set; }
        public double 正常巡檢百分比 { get; set; }
        public int 檢查異常數 { get; set; }
        public double 異常巡檢百分比 { get; set; }
        public int 未巡檢次數 { get; set; }
        public double 未巡檢百分比 { get; set; }

        //長柱圖使用
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
