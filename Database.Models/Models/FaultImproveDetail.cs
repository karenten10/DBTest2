using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FaultImproveDetail
    {
        public long Id { get; set; }
        public string FaultImproveGuid { get; set; }
        public int EquipmentId { get; set; }
        public int ExamItemId { get; set; }
        public long DepartmentId { get; set; }
        public string Memo { get; set; }
        public DateTime ExpectedFinishDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ExpandExamItemId { get; set; }
        public string AfterMemo { get; set; }

        public virtual Department Department { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual EquipmentExamItem ExamItem { get; set; }
    }
}
