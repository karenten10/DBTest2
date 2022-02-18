using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ContractEmployees
    {
        public int Id { get; set; }
        public long DepartmentId { get; set; }
        public DateTime ContractDate { get; set; }
        public int? NumberOfPeople { get; set; }

        public virtual Department Department { get; set; }
    }
}
