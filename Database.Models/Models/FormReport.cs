using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FormReport
    {
        public FormReport()
        {
            FormMapping = new HashSet<FormMapping>();
            FormPath = new HashSet<FormPath>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string No { get; set; }
        public string Tool { get; set; }
        public string Iso { get; set; }
        public int? DepartmentId { get; set; }
        public string Period { get; set; }

        public virtual ICollection<FormMapping> FormMapping { get; set; }
        public virtual ICollection<FormPath> FormPath { get; set; }
    }
}
