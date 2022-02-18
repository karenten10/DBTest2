using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class RepairMasterAdapterModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string 申請人Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int RepairEquipmentId { get; set; }
        public string RepairEquipmentName { get; set; }
        public string Phone { get; set; }
        public string FailureDescription { get; set; }
        public string FormNumber { get; set; }
        public string Location { get; set; }
        public int PersonInCharge { get; set; }
        public string 負責人Name { get; set; }
        public int PersonOfContact { get; set; }
        public string 承辦人Name { get; set; }
        public string Remark { get; set; }
        public string PropertyNo { get; set; }
        public string ContactTelNo { get; set; }
        public string VendorVATNo { get; set; }
        public string VendorMail { get; set; }
        public string VendorName { get; set; }
        public int QTY { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Status { get; set; }
        
        public List<ImageRepositoryAdapterModel> ImageRepository { get; set; } = new List<ImageRepositoryAdapterModel>();
    }
}
