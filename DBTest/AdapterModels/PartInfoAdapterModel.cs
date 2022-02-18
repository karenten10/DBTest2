using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PartInfoAdapterModel
    {
        public int PartId { get; set; }
        public int OldPartId { get; set; }
        public string PartIdNumber { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string StockUnit { get; set; }
        public decimal StockQty { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public int? CycleTime { get; set; }

    }
}
