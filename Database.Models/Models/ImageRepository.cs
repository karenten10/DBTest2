using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ImageRepository
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PhotoDatetime { get; set; }
        public DateTime UploadDatetime { get; set; }
        public string Folder { get; set; }
        public string Filename { get; set; }
        public string FileExtension { get; set; }
        public int ImageType { get; set; }
        public int Reference { get; set; }
        public int ReferenceBig { get; set; }
    }
}
