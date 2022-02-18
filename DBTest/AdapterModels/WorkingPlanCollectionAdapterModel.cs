using Database.Models.Models;
using System;

namespace InspectionBlazor.AdapterModels
{
    public class WorkingPlanCollectionAdapterModel : ICloneable
    {
        public long Id { get; set; }
        public int EquipmentId { get; set; }
        public int PeriodId { get; set; }
        public DateTime WorkDate { get; set; }
        public string Remark { get; set; }

        public string EquipmentType { get; set; }
        public string SectionName { get; set; }
        public string EquipmentName { get; set; }
        public string InspectionType { get; set; }
        public DateTime? ActualWorkDate { get; set; }
        public string PeriodType { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Expand Expand { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
