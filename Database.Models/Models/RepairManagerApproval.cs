using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairManagerApproval
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? SourceId { get; set; }
        public int RepairMasterId { get; set;}
        public string Status { get; set; }
        public int PersonId { get; set; }
        public int Level { get; set; }
        public string Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Memo { get; set; }
    }
}
