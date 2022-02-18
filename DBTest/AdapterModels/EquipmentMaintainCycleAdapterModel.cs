using System;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentMaintainCycleAdapterModel
    {
        public int EquipmentBasicId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime? LastMaintainDate { get; set; }
        public int? MaintainCycleYear { get; set; }
        public int? MaintainCycleMonth { get; set; }
        public int? MaintainCycleDay { get; set; }
        public string EquipmentName { get; set; }
        public string SectionName { get; set; }
        public int OverdueDay { get; set; }
        public string Cycle { get; set; }
        public string Spec { get; set; }
        public bool ButtonDisabled { get; set; }
        public string ButtonContent { get; set; }
    }
}
