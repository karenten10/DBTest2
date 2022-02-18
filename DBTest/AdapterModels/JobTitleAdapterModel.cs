using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class JobTitleAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "必須要輸入值")]
        public string Name { get; set; }
    }
}
