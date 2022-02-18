using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Mwmaster
    {
        public Mwmaster()
        {
            Mwdetail = new HashSet<Mwdetail>();
        }

        public int FormId { get; set; }
        public string FormType { get; set; }
        public string FormNumber { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? QtyChangeTime { get; set; }
        public int? CreatePersonnelId { get; set; }
        public int? LocationId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int? ApplicantId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? EquipmentId { get; set; }
        public int? AssingPersonnelId { get; set; }
        public string PropertyNo { get; set; }
        public int? MaintainQty { get; set; }
        public int? PersonInChargeId { get; set; }
        public string CauseOfFailure { get; set; }
        public string ContactTelNo { get; set; }
        public string VendorVatno { get; set; }
        public string Note { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }

        public virtual ICollection<Mwdetail> Mwdetail { get; set; }
    }
}
