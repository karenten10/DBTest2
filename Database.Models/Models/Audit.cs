using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Audit
    {
        public long Id { get; set; }
        public string AuditName { get; set; }
        public string ForPaths { get; set; }
    }
}
