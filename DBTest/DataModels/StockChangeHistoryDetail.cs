using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class StockChangeHistoryDetail
    {
        public string FormType { get; set; }
        public string FormNumber { get; set; }
        public DateTime? QtyChangeTime { get; set; }
        public string CreatePerson { get; set; }
        public decimal Qty { get; set; }
        public string Ramark { get; set; }
    }
}
