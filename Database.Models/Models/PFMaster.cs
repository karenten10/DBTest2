using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Pfmaster
    {
        public Pfmaster()
        {
            Pfdetail = new HashSet<Pfdetail>();
        }

        public int FormId { get; set; }
        public string FormType { get; set; }
        public string FormNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? QtyChangeTime { get; set; }
        public int CreatePersonnelId { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }

        public virtual PartLocationInfo Location { get; set; }
        public virtual ICollection<Pfdetail> Pfdetail { get; set; }
    }
}
