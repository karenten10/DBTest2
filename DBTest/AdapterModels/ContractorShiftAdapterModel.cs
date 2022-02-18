using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ContractorShiftAdapterModel
    {
        public int Id { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? BeginTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? Hours { get; set; }
        public string CrossDay { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
    }
}
