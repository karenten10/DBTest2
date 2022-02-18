using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class WorkTypeAdapterModel
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public decimal? WorkingHours { get; set; }
        public string Remark { get; set; }
        public int? JobTypeId { get; set; }
        public string WorkingDay { get; set; }
        [Required(ErrorMessage = "必須至少要一項")]
        public int?[] WorkingDayArray { get; set; }
        public int?[] WorkTypePeopleArray { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
    }
}
