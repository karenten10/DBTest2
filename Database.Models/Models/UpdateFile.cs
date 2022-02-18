using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class UpdateFile
    {
        public int Id { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Memo { get; set; }
    }
}
