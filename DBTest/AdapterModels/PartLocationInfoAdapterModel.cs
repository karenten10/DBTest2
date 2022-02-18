using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PartLocationInfoAdapterModel
    {
        public int LocationId { get; set; }
        public string LocationIdNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
