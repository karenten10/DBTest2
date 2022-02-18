using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class HandyDetailAdapterModel
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int HandyMasterId { get; set; }
        public string Status { get; set; }

        public string StatusStr 
        {
            get {
                if (Status == "Y")
                {
                    return "已確認";
                } else 
                {
                    return "尚未確認";
                }
            }
            set { }
        }

        public string Memo { get; set; }
        public DateTime? UpdateDate { get; set; }

        // ==== 以下為master所需欄位 ====
        
        public int MasterCreateId { get; set; }

        public string MasterCreator { get; set; }

        public string MasterMemo { get; set; }
        public string MasterPhoto1 { get; set; }
        public string MasterLocation { get; set; }
        public DateTime? MasterCreateDate { get; set; }
    }
}
