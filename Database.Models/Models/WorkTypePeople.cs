using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class WorkTypePeople
    {
        public int Id { get; set; }
        public int WorkTypeId { get; set; }
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
