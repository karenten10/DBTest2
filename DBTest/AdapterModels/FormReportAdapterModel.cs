using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class FormReportAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "名稱欄位必須要輸入值")]
        public string Title { get; set; }
        [Required(ErrorMessage = "表單代碼欄位必須要輸入值")]
        [StringLength(10, ErrorMessage = "「表單代碼」請在10碼以內。")]
        public string Code { get; set; }
        public string No { get; set; }
        // 這個欄位當初適用於奇美專案，在大林廠專案改成作為[報表備註]
        public string Tool { get; set; }
        // Todo : 這個欄位當初適用於奇美專案，在大林廠專案是否還要繼續使用，若不使用，記得清除相關資料庫與程式碼，最後要做測試，確保沒有後遺症
        public string Iso { get; set; }
        [Required(ErrorMessage = "課別欄位必須要輸入值")]
        public int? DepartmentId { get; set; }

        public string Period { get; set; } // D: 日報, M: 月報
    }
}
