using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class MyPFMasterMonthly
    {
        public int PartId { get; set; }
        public string PartName { get; set; }

        public decimal?[] DayValues { get; set; } = new decimal?[31];

        
        public string D1Value { get; set; }
        
        public string D2Value { get; set; }
        
        public string D3Value { get; set; }
        
        public string D4Value { get; set; }
        
        public string D5Value { get; set; }
        
        public string D6Value { get; set; }
        
        public string D7Value { get; set; }
        
        public string D8Value { get; set; }
        
        public string D9Value { get; set; }
        
        public string D10Value { get; set; }
        
        public string D11Value { get; set; }
        
        public string D12Value { get; set; }
        
        public string D13Value { get; set; }
        
        public string D14Value { get; set; }
        
        public string D15Value { get; set; }
        
        public string D16Value { get; set; }
        
        public string D17Value { get; set; }
        
        public string D18Value { get; set; }
        
        public string D19Value { get; set; }
        
        public string D20Value { get; set; }
        
        public string D21Value { get; set; }
        
        public string D22Value { get; set; }
        
        public string D23Value { get; set; }
        
        public string D24Value { get; set; }
        
        public string D25Value { get; set; }
        
        public string D26Value { get; set; }
        
        public string D27Value { get; set; }
        
        public string D28Value { get; set; }
        
        public string D29Value { get; set; }
        
        public string D30Value { get; set; }
        
        public string D31Value { get; set; }

        public string SubtotalValue { get; set; }
    }

    public class MyPFDetail {

        public int PartId { get; set; }
        public decimal QtyChange { get; set; }

        public int ParentId { get; set; }
        public string ParentFormType { get; set; }
        public DateTime ParentCreateTime { get; set; }


    }

    public class MyMWDetail
    {

        public int PartId { get; set; }
        public decimal QtyChange { get; set; }

        public int ParentId { get; set; }
        public DateTime ParentCreateTime { get; set; }


    }
}
