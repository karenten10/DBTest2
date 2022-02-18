using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{

    public class OutComeReport89AdapterModel
    {
        // 巡檢點相關
        public int PatrolPlaceId { get; set; }
        public string PatrolPlaceName { get; set; }

        public string JobTitleName { get; set; }

        public string PersonName { get; set; }

        public int SectionId { get; set; }

        public string SectionName { get; set; }

        public int equipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int EquipmentExamItemId { get; set; }

        public string EquipmentExamItemName { get; set; }

        public string ExamMethod { get; set; }

        public string ExpandName { get; set; }

        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }


        // 以下為原本OutCome欄位
        public long ExpandExamItemId { get; set; }
        public decimal? Value { get; set; }
        public string ValueString { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }
        public string MultiChoice { get; set; }
        public string IsCompleted { get; set; }
        public int PatrolPathId { get; set; }
        public long ExpandId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string IsAbnormal { get; set; }
        public bool IsInspection { get; set; }
        public string Remark { get; set; }
        public int PersonId { get; set; }
        public string EquipmentShutdown { get; set; }
        public DateTime? ServerUpdateTime { get; set; }
        public string IsEdit { get; set; }

        public string YetMemo { get; set; } // 應到未到備註
    }
}
