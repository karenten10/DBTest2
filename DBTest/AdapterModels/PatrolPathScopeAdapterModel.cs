using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathScopeAdapterModel
    {
        public int Id { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolScopeId { get; set; }
        [Required(ErrorMessage = "關聯鍵值必須要存在")]
        public string PatrolScopeName { get; set; }
        [Required(ErrorMessage = "請選取路線")]
        public string PatrolPathName { get; set; }

    }
}
