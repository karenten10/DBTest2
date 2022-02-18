using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FormPath
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int FormReportId { get; set; }

        public virtual FormReport FormReport { get; set; }
        public virtual PatrolPath PatrolPath { get; set; }
    }
}
