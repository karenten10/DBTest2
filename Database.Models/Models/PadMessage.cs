using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PadMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public long? DepartmentId { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual Department Department { get; set; }
    }
}
