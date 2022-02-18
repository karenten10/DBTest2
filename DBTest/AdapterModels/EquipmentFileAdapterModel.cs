using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class EquipmentFileAdapterModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        //[Required(ErrorMessage = "欄位必須要輸入值")]
        public string FileKind { get; set; }
        public string FileName { get; set; }

        public string EquipmentName { get; set; }

        public EquipmentAdapterModel Equipment { get; set; }
        // public object ImageRepository { get; internal set; }

        public List<DocRepositoryAdapterModel> DocRepository { get; set; } = new List<DocRepositoryAdapterModel>();
    }
}
