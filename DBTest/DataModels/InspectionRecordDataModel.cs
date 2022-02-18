using InspectionBlazor.Helpers;
using System;
using System.Collections.Generic;

namespace InspectionBlazor.DataModels
{
    public partial class InspectionQueryConditionDataModel
    {
        public long? DepartmentId { get; set; } // 部門id
        public int? Person { get; set; }//人員id
        public int? PatrolPath { get; set; }//路線id
        public DateTime? Begin { get; set; }//時間
        public DateTime? End { get; set; }//時間
        public int? Equipment { get; set; }//設備
        public int? Scope { get; set; }//設備
        public string AbnormalType { get; set; }
        public InspectionRecordHelper.SearchType SearchType { get; set; }

        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }

    public partial class InspectionForOutComeHistoryQueryConditionDataModel
    {
        public int? PatrolPath { get; set; }//路線id
        public DateTime? Begin { get; set; }//時間
        public DateTime? End { get; set; }//時間

        public string Should { get; set; } // Y: 已巡檢, N: 未到位

        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }

    public partial class CourseStatisticsQueryConditionDataModel
    {
        public DateTime? Begin { get; set; }//時間
        public DateTime? End { get; set; }//時間
        public int? DepartmentID { get; set; }
        public string 到位Type { get; set; }
        public string 巡檢Type { get; set; }
    }

    public partial class FaultImproveIndexConditionDataModel
    {
        public long? DepartmentId { get; set; } // 部門id
        public DateTime Begin { get; set; }//時間
        public DateTime End { get; set; }//時間


        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }

    public partial class CourseStatisticIndexConditionDataModel
    {
        public long? DepartmentId { get; set; } // 部門id
        public DateTime Begin { get; set; }//時間
        public DateTime End { get; set; }//時間
        

        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }

    public partial class InspectionRecordIndexConditionDataModel
    {
        public long? DepartmentId { get; set; } // 部門id
        public DateTime Begin { get; set; }//時間
        public DateTime End { get; set; }//時間


        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }

    public partial class InspectionRecordStatisticsIndexConditionDataModel
    {
        public long? DepartmentId { get; set; } // 部門id
        public int? Person { get; set; }//人員id
        public int? PatrolPath { get; set; }//路線id
        public DateTime Begin { get; set; }//時間
        public DateTime End { get; set; }//時間
        public int? Equipment { get; set; }//設備
        public int? Scope { get; set; }//設備
        public string AbnormalType { get; set; }
        public InspectionRecordHelper.SearchType SearchType { get; set; }

        public bool IsFirst { get; set; } = true; // 第一次打開畫面先不查
    }
}
