using Database.Models.Models;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class MWMasterAdapterModel
    {
        public int FormId { get; set; }
        public string FormType { get; set; }
        public string FormNumber { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? QtyChangeTime { get; set; }
        public int? CreatePersonnelId { get; set; }
        public string CreatePersonnelName { get; set; }
        public int? LocationId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int? ApplicantId { get; set; }
        public string ApplicantIdName { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string ApplicationDateDisplay { get; set; }
        public DateTime? DueDate { get; set; }
        public string DueDateDisplay { get; set; }
        public int? EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int? AssingPersonnelId { get; set; }
        public string AssingPersonnelName { get; set; }
        public string PropertyNo { get; set; }
        public int? MaintainQty { get; set; }
        public int? PersonInChargeId { get; set; }
        public string PersonInChargeName { get; set; }
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

        public List<MwdetailAdapterModel> listMwdetailAdapterModel = new List<MwdetailAdapterModel>();
    }

    public  class MwdetailAdapterModel
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
