using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ExpandAllowDay
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public long ExpandId { get; set; }

        public virtual Expand Expand { get; set; }
    }
}
