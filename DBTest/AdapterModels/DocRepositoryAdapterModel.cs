using InspectionBlazor.Helpers;
using InspectionShare.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class DocRepositoryAdapterModel
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
        public string DocURL
        {
            get
            {
                return $"{MagicDocHelper.DocFolderName}/{Folder}/{Filename}.{FileExtension}";
            }
        }
        public int Reference { get; set; }
    }
}
