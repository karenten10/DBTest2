using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathPeriodNexamItemAdapterModel
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        public int EquipmentExamItemId { get; set; }

        //public virtual EquipmentExamItemAdapterModel EquipmentExamItem { get; set; }
    }
}
