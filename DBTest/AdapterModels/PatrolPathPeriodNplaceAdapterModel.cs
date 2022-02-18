using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathPeriodNplaceAdapterModel
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        public int PatroPlaceId { get; set; }
    }
}
