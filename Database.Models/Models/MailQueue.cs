using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class MailQueue
    {
        public long Id { get; set; }
        public string MailAddress { get; set; }
        public string Mailcc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string SentStatus { get; set; }
    }
}
