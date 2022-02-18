using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class PatrolPathPeriodAdapterModel
    {
        public PatrolPathPeriodAdapterModel()
        {
            PatrolPathPeriodNexamItem = new HashSet<PatrolPathPeriodNexamItemAdapterModel>();
            PatrolPathPeriodNplace = new HashSet<PatrolPathPeriodNplaceAdapterModel>();
        }
        public virtual ICollection<PatrolPathPeriodNexamItemAdapterModel> PatrolPathPeriodNexamItem { get; set; }
        public virtual ICollection<PatrolPathPeriodNplaceAdapterModel> PatrolPathPeriodNplace { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public string DaysOfMonth { get; set; }
        public string DaysOfWeek { get; set; }
        public DateTime? BeginDay { get; set; }
        public DateTime? LastDay { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SBeginTime { get; set; }
        public string SEndTime { get; set; }
        public int PatrolPathId { get; set; }

        [Required(ErrorMessage = "巡檢路線名稱必須要存在，請點選問號來選取")]
        public string PatrolPathName { get; set; }
        [Required(ErrorMessage = "週期必須存在")]
        public string Cycle { get; set; }
        public int? Day_Month { get; set; }
        public int? RangeBeginMonth { get; set; }
        public int? RangeBeginDay { get; set; }
        public int? RangeEndMonth { get; set; }
        public int? RangeEndDay { get; set; }
        public int? WeekRangeBegin { get; set; }
        public int? WeekRangeEnd { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public int? SortId { get; set; }
        public string Rule1 { get; set; }
        public string Rule2 { get; set; }
        public string TimeLimit { get; set; }
        public string FillUnKeyin { get; set; }
        public int? TipNotify { get; set; }
        public int? UnTransNotify { get; set; }
        public string Crontab { get; set; }
        public int TotalPlace { get; set; }
        public int TotalEquipment { get; set; }
        public int TotalExam { get; set; }
    }
}
