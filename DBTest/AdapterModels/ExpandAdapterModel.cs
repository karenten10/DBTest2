using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ExpandAdapterModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalExamItem { get; set; }
        public int CompleteExamItem { get; set; }
        public int TotalPlace { get; set; }
        public int CompletePlace { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolPathPeriodId { get; set; }

        public virtual PatrolPathAdapterModel PatrolPath { get; set; }
        public virtual PatrolPathPeriodAdapterModel PatrolPathPeriod { get; set; }
        public virtual ICollection<ExpandAllowDayAdapterModel> ExpandAllowDay { get; set; }
    }
}
