using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Smtp
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
