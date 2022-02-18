using InspectionBlazor.Helpers;
using InspectionShare.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ImageRepositoryAdapterModel
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
        public string ImageURL
        {
            get
            {
                return $"{MagicImageHelper.ImageFolderName}/{Folder}/{Filename}.{FileExtension}";
            }
        }
        public int Reference { get; set; }
    }
}
