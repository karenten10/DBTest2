using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class InspectionRecordStatisticsDataModel
    {
       public DateTime BeginTime { get; set; }
       public int PathId { get; set; }
       public string Path { get; set; }
       public int ScopeId { get; set; }
       public string Scope { get; set; }
       public string AbnormalName { get; set; }
       public int AbnormalCount { get; set; }
    }
}
