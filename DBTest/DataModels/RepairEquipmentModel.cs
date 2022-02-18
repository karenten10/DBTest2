using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class RepairEquipmentModel
    {
       public int RepairEqipmentGroupId { get; set; }
       public int RepairEquipmentId { get; set; }
       public string RepairEquipmentName { get; set; }
       public string 負責人姓名 { get; set; }
       public string 維修備註 { get; set; }
    }
}
