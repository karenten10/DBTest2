using Database.Models.Models;
using InspectionBlazor.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class FaultImproveAdapterModel {    
        public long Id { get; set; }
        public long FaultImproveId { get; set; }
        public int PersonId { get; set; }
        public string IsApproval { get; set; }
        public DateTime CreatedDate { get; set; }

        public long? FaultImproveParentId { get; set; }
        public int FaultImproveLevel { get; set; }
        public long? FaultImproveSourceId { get; set; }
        public long? FaultImproveManagerApprovalId { get; set; }
        public int ApprovalPeopleId { get; set; }
        public string ApprovalPeople { get; set; }
        public string Manager { get; set; }
        public string Status { get; set; }
        public string Guid { get; set; }
        public int MyselfId { get; set; }
        public string 設備名稱 { get; set; }
        public string 巡檢點名稱 { get; set; }
        public string 範圍名稱 { get; set; }
        public string 設備項目 { get; set; }
        public string 交辦單位 { get; set; }
        public DateTime 預計完成日 { get; set; }
        public string 備註 { get; set; }
        public string 是否簽核 { get; set; }

        // 改善前照片1-6
        public string BeforePhoto1 { get; set; }
        public string BeforePhoto2 { get; set; }
        public string BeforePhoto3 { get; set; }
        public string BeforePhoto4 { get; set; }
        public string BeforePhoto5 { get; set; }
        public string BeforePhoto6 { get; set; }

        public string AfterMemo { get; set; }

        public List<ImageRepositoryAdapterModel> ImageRepository { get; set; } = new List<ImageRepositoryAdapterModel>();
        public List<缺失改善全流程Model> 缺失改善全流程 { get; set; }
    }
}
