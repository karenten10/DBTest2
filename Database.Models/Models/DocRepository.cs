using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class DocRepository
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDatetime { get; set; }
        public DateTime UpdateDatetime { get; set; }
        public string Folder { get; set; }
        public string Filename { get; set; }
        public string FilenameOriginal { get; set; }
        public string FileExtension { get; set; }
        public int FormType { get; set; }
        public int Reference { get; set; }
        public int ReferenceBig { get; set; }
    }
}
