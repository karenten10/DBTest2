using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PlaceStayTimeAdapterModel
    {
        public long Id { get; set; }
        public int PatrolPathPeriodId { get; set; }
        [Required(ErrorMessage = "請選擇巡檢點")]
        public int PatroPlaceId { get; set; }
        [Required(ErrorMessage = "請輸入停留秒數")]
        public int StaySecs { get; set; }

        public string PatrolPlaceName { get; set; }

    }
}
