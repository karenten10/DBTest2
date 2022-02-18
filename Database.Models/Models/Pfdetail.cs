using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Pfdetail
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ItemId { get; set; }
        public int PartId { get; set; }
        public decimal QtyChange { get; set; }
        public string Remark { get; set; }

        public virtual Pfmaster Parent { get; set; }
        public virtual PartInfo Part { get; set; }
    }
}
