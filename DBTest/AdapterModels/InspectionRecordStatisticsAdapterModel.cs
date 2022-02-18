using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class InspectionRecordStatisticsAdapterModel
    {
        public DateTime ExpandTime { get; set; }//展開時段
        public string strUpdateTime { get; set; }//展開時段
        public string Path { get; set; } //路線
        public string Period { get; set; } //時段
        public string Scope { get; set; } //範圍
        public string Place { get; set; }//巡檢點
        public string Recorder { get; set; } //紀錄者
        public string Exam { get; set; } //設備
        public string ExamItem { get; set; } //設備項目
        public string Audit { get; set; }//審核依據
        public string Value { get; set; }//紀錄數值
        public DateTime UpdateTime { get; set; }
        public string Type { get; set; }//紀錄屬性
    }
}
