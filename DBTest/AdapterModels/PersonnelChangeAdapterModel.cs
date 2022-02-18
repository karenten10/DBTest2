using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PersonnelChangeAdapterModel
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
        public string StatusName { get; set; }
        public string JobTitleName { get; set; }
        public string PersonName { get; set; }
        public string IsChangedName { get; set; }
        public int?[] PersonIds { get; set; }   
    }
}
