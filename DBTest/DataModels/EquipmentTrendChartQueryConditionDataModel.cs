using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class EquipmentTrendChartQueryConditionDataModel
    {
        public DateTime? Begin { get; set; }//時間
        public DateTime? End { get; set; }//時間
        public int? EquipmentExamItemId { get; set; }//設備檢驗項目
       
    }
}
