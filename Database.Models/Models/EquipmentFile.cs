using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class EquipmentFile
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string FileKind { get; set; }
        public string FileName { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
