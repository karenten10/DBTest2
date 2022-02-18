using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PersonManager
    {
        public long Id { get; set; }
        public int PersonId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Person Manager { get; set; }
    }
}
