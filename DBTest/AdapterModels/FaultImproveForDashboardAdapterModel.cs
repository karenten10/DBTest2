using Database.Models.Models;
using InspectionBlazor.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class FaultImproveForDashboardAdapterModel
    {
        public string EquipmentName { get; set; }
        public string EquipmentItemName { get; set; }
        public string 開立時間 { get; set; }
        public string 預計完成時間 { get; set; }
        public int 逾期天數 { get; set; }
        public string Status { get; set; }
        public string Guid { get; set; }
    }
}
