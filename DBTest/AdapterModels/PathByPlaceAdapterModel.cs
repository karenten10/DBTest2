using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PathByPlaceAdapterModel
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public string ScopeName { get; set; }
        public string PlaceName { get; set; }
        public bool InPath { get; set; }
    }
}
