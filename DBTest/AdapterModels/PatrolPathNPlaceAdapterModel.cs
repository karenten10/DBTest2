using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathNPlaceAdapterModel
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolPlaceId { get; set; }
        public string PatrolPathName { get; set; }
        public string PatrolPlaceName { get; set; }
    }
}
