using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ReportManagerListAdapterModel
    {
        public int Level { get; set; }
        public List<OutComeReportAdapterModel> managerList { get; set; }
    }

    public class OutComeReportAdapterModel
    {

        public string JobTitleName { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public DateTime 簽核時間 { get; set; }

    }
}
