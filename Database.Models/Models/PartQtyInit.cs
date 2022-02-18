using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.Models
{
    public partial class PartQtyInit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PartId { get; set; }
        public decimal QtyInit { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

        public virtual PartInfo Part { get; set; }
    }
}
