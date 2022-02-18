using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class LeaveTypeAdapterModel
    {
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
    }
}
