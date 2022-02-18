using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class AuditAdapterModel
    {
        public long Id { get; set; }
        public string AuditName { get; set; }
        public string ForPaths { get; set; }
        public int?[] Paths { get; set; }
    }
}
