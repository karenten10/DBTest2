using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class CheckReportModel
    {
        public FormReport formReport { get; set; }
        public string dateString { get; set; }
    }
}
