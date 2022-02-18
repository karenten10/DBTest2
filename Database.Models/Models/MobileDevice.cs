using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class MobileDevice
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Mac { get; set; }
        public string Remark { get; set; }
    }
}
