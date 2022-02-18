using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PeriodByPlaceAdapterModel
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public string ScopeName { get; set; }
        public string PlaceName { get; set; }
        public bool InPeriod { get; set; }
    }
}
