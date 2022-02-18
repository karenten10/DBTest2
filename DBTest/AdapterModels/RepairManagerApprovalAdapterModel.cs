using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class RepairManagerApprovalAdapterModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int SourceId { get; set; }
        public int RepairMasterId { get; set; }
        public string Status { get; set; }
        public int PersonId { get; set; }
        public int Level { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Memo { get; set; }
    }
}
