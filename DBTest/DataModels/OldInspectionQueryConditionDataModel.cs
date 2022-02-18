using InspectionBlazor.Helpers;
using System;
using System.Collections.Generic;

namespace InspectionBlazor.DataModels
{
    public partial class OldInspectionQueryConditionDataModel
    {
        public int DepartmentId { get; set; } // 部門id
        public int PatrolPath { get; set; }//路線id
        public DateTime Begin { get; set; }//時間
        public DateTime End { get; set; }//時間
        public int Equipment { get; set; }//設備
        public int Scope { get; set; }//範圍
        public string AbnormalType { get; set; }
        public InspectionRecordHelper.SearchType SearchType { get; set; }
        public bool IsFirst { get; set; } = true;
    }
}
