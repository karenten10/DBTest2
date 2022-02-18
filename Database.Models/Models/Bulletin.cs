using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Bulletin
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AnnounceTime { get; set; }
    }
}
