using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels {
    public class AuthorityAdapterModel {
        public long Id { get; set; }
        [Required(ErrorMessage = "欄位必須要輸入值")]
        public string Name { get; set; }
        public string Guid { get; set; }
    }
}
