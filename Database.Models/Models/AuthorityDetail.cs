using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class AuthorityDetail
    {
        public long Id { get; set; }
        public long AuthorityId { get; set; }
        public string MenuCode { get; set; }

        public virtual Authority Authority { get; set; }
    }
}
