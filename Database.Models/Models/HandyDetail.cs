using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class HandyDetail
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int HandyMasterId { get; set; }
        public string Status { get; set; }
        public string Memo { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual HandyMaster HandyMaster { get; set; }
        public virtual Person Manager { get; set; }
    }
}
