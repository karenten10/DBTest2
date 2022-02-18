using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
