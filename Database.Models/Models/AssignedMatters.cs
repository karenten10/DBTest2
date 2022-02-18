using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class AssignedMatters
    {
        public long Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Message { get; set; }
        public DateTime AnnounceTime { get; set; }
    }
}
