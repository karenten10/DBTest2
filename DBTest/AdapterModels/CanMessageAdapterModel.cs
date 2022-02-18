using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class CanMessageAdapterModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public int CreateId { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}
