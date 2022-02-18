using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Section
    {
        public Section()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
