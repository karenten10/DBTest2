using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ExpandAllowDayAdapterModel
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public long ExpandId { get; set; }

        public virtual ExpandAdapterModel Expand { get; set; }
    }
}
