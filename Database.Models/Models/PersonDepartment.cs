using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PersonDepartment
    {
        public long Id { get; set; }
        public int PersonId { get; set; }
        public long? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
