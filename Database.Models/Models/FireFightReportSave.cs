using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class FireFightReportSave
    {
        public FireFightReportSave()
        {
            
        }

        public int Id { get; set; }

        public string QueryDate { get; set; }
        public string Building { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        
    }
}
