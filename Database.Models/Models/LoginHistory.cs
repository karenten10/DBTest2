using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class LoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
