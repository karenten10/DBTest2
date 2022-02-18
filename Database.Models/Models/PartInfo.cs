using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PartInfo
    {
        public PartInfo()
        {
            EquipmentPart = new HashSet<EquipmentPart>();
            Pfdetail = new HashSet<Pfdetail>();
        }

        public int PartId { get; set; }
        public string PartIdNumber { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string StockUnit { get; set; }
        public decimal StockQty { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? CycleTime { get; set; }

        public virtual PartQtyInit PartQtyInit { get; set; }
        public virtual ICollection<EquipmentPart> EquipmentPart { get; set; }
        public virtual ICollection<Pfdetail> Pfdetail { get; set; }
    }
}
