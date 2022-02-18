using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class DropDownListClass
    {
    }

    public class RemakerType
    {
        public string Type { get; set; }
        public int TypeId { get; set; }
    }
    public class Dispatch
    {
        public string DispatchStatus { get; set; }
    }
    public class FormStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
