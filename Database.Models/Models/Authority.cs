using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Authority
    {
        public Authority()
        {
            AuthorityDetail = new HashSet<AuthorityDetail>();
            Person = new HashSet<Person>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }

        public virtual ICollection<AuthorityDetail> AuthorityDetail { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
