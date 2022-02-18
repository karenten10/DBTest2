using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PartLocationInfo
    {
        public PartLocationInfo()
        {
            Pfmaster = new HashSet<Pfmaster>();
        }

        public int LocationId { get; set; }
        public string LocationIdNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Pfmaster> Pfmaster { get; set; }
    }
}
