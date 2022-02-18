using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            AttendanceRegister = new HashSet<AttendanceRegister>();
        }

        public int Id { get; set; }
        public string LeaveName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AttendanceRegister> AttendanceRegister { get; set; }
    }
}
