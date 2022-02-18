using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class OutComeEditAdapterModel
    {
        public int PathId { get; set; }
        public string PathName { get; set; }
        public int ExamItemId { get; set; }

        public int EquipmentExamItemId { get; set; }
        public string ExanItemName { get; set; }
        public bool IsAbnormal { get; set; }
        [Required(ErrorMessage = "欄位必須要輸入值")]
        public string IsAbnormalName { get; set; }
        public double Value { get; set; }
        public string ValueString { get; set; }
        public string MultiChoice { get; set; }
        public string OldResult { get; set; }
        // [Required(ErrorMessage = "欄位必須要輸入值")]
        public string EditResult { get; set; }
        public int PersionId { get; set; }
        public string PersionName { get; set; }
        [Required(ErrorMessage = "請填寫時間")]
        public DateTime UpdateTime { get; set; }
        public bool IsEdit { get; set; }
        public string IsEditName { get; set; }
        public int EditId { get; set; }
        public string EditName { get; set; }
        public int OutComeHistoryId { get; set; }
        public string ExamConditionName { get; set; }
        public string EquipmentShutdown { get; set; }

        public string Remark { get; set; } // 備註(巡檢備註)
        public string YetMemo { get; set; } // 處理情形(補單備註)

        public string PatrolPlaceName { get; set; }// [巡檢點]名稱

        public string EquipmentName { get; set; } // [設備]名稱

        public DateTime BeginTime { get; set; } // 時段-開始時間
        public DateTime EndTime { get; set; } // 時段-結束時間

        public string IsCompleted { get; set; }

        public PatrolPath PatrolPath { get; set; }
    }
}
