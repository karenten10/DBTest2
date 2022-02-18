using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class StockChangeHistoryAdapterModel
    {
        public int Id { get; set; }
        public string PartName { get; set; }
    }
}
