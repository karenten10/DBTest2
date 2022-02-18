using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class CanMessage
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public int CreateId { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual Person Create { get; set; }
    }
}
