using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentBasicAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "欄位必須要輸入值")]
        public string EnglishName { get; set; }
        public string EquipmentStatus { get; set; }
        public string Spec { get; set; }
        public string MaintenRemark { get; set; }
        public string EquipmentValue { get; set; }
        public long? OwnerDepartment { get; set; }
        public int? OwnerPerson { get; set; }
        public long? MaintenDepartment { get; set; }
        public int? MaintenPerson { get; set; }
        public string Manufacturer { get; set; }
        public string Capacity { get; set; }
        public string SerialNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
        public int? WorkingHour { get; set; }
        public string Remark { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }

        public string EquipmentName { get; set; }

        public string OwnerDepartmentName { get; set; }

        public string OwnerPersonName { get; set; }

        public string MaintenDepartmentName { get; set; }

        public string MaintenPersonName { get; set; }
        public DateTime? LastMaintainDate { get; set; }
        public int? MaintainCycleYear { get; set; }
        public int? MaintainCycleMonth { get; set; }
        public int? MaintainCycleDay { get; set; }

        public EquipmentAdapterModel Equipment { get; set; }
    }
}
