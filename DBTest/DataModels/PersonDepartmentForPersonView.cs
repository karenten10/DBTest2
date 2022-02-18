using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class PersonDepartmentForPersonView
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string DepartmentName { get; set; }
    }
}
