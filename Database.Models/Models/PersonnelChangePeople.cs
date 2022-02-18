using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PersonnelChangePeople
    {
        public int Id { get; set; }
        public int PersonnelChangeId { get; set; }
        public int PersonId { get; set; }
    }
}
