using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class SituationDashboardData8DataModel
    {
        public string 期間 { get; set; }
        public string 巡檢路線 { get; set; }
        public string 巡檢時段 { get; set; }
        public DateTime 到位時間 { get; set; }
        public string 單位 { get; set; }
        public string 記錄人員 { get; set; }
        public string 標準值 { get; set; }
        public string 異常紀錄值 { get; set; }
        public string 設備名稱 { get; set; }
        public string 簽核人 { get; set; }
        public string Tag編號 { get; set; }
    }
}
