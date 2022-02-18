using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class HandyMaster
    {
        public HandyMaster()
        {
            HandyDetail = new HashSet<HandyDetail>();
        }

        public int Id { get; set; }
        public int CreateId { get; set; }
        public string Memo { get; set; }
        public string Photo1 { get; set; }
        public string Location { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ServerUpdateTime { get; set; }
        public string Guid { get; set; }

        public virtual Person Create { get; set; }
        public virtual ICollection<HandyDetail> HandyDetail { get; set; }
    }
}
