using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class HandyMasterAdapterModel
    {
        public int Id { get; set; }
        public int CreateId { get; set; }
        public string Memo { get; set; }
        public string Photo1 { get; set; }
        public string Location { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ServerUpdateTime { get; set; }
        public Guid? Guid { get; set; }

        public Person Create { get; set; }
        public virtual ICollection<HandyDetailAdapterModel> HandyDetail { get; set; }
    }
}
