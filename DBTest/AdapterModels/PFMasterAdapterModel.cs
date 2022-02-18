using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PFMasterAdapterModel
    {
        public int FormId { get; set; }
        public int DeatailId { get; set; }
        public string FormType { get; set; }
        public string FormNumber { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? QtyChangeTime { get; set; }
        public int CreatePersonnelId { get; set; }
        public string CreatePersonnelName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public decimal ModifyQty { get; set; }
        public string Remark { get; set; }

        public List<PfDetailAdapterModel> listPfDetail = new List<PfDetailAdapterModel>();
    }
}
