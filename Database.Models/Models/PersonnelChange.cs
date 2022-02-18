using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PersonnelChange
    {
        public int Id { get; set; }
        public int? VacancyCount { get; set; }
        public string MustQualify { get; set; }
        public string OptionalQualify { get; set; }
        public int? OriginalCount { get; set; }
        public string IsChanged { get; set; }
        public int? ChangedCount { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
    }
}
