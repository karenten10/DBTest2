using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class MyAttendanceRegister
    {
        public long Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int PersonId { get; set; }
        public int? ContractorShiftId { get; set; }
        public string ContractorShiftSegment { get; set; }
        public int? LeaveTypeId { get; set; }
        public string LeaveTypeSegment { get; set; }
        public string Remark { get; set; }
        public int? WorkTypeId { get; set; }
        public string PersonName { get; set; }
        public string ContractorShiftShiftName { get; set; }    
        public string LeaveTypeLeaveName { get; set; }
        public string WorkTypeJobName { get; set; }
    }
}
