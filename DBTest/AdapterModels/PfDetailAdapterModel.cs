using Database.Models.Models;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PfDetailAdapterModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ItemId { get; set; }
        public int PartId { get; set; }
        public decimal QtyChange { get; set; }
        public string Remark { get; set; }

        public string PartName { get; set; }
    }
}
