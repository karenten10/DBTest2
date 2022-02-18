using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class RepairMaster
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DepartmentId { get; set; }
        public int RepairEquipmentId { get; set; }
        public string Phone { get; set; }
        public string FailureDescription { get; set; }
        public string FormNumber { get; set; }
        public string Location { get; set; }
        public int PersonInCharge { get; set; }
        public int PersonOfContact { get; set; }
        public string Remark { get; set; }
        public string PropertyNo { get; set; }
        public string ContactTelNo { get; set; }
        public string VendorName { get; set; }
        public string VendorVATNo { get; set; }
        public string VendorMail { get; set; }
        public int QTY { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }

    }
}
