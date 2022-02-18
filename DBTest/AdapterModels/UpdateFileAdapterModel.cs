using System;
using System.ComponentModel.DataAnnotations;

namespace InspectionBlazor.AdapterModels {
    public class UpdateFileAdapterModel {
        public int Id { get; set; }
        public string FileType { get; set; }
        [Required(ErrorMessage = "請先上傳檔案")]
        public string FileName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Memo { get; set; }
    }
}
