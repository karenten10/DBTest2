using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class Expand
    {
        public Expand()
        {
            Checkin = new HashSet<Checkin>();
            CheckinConflictn = new HashSet<CheckinConflictn>();
            ExpandAllowDay = new HashSet<ExpandAllowDay>();
            ExpandExamItem = new HashSet<ExpandExamItem>();
            ExpandPlace = new HashSet<ExpandPlace>();
            ManagerApproval = new HashSet<ManagerApproval>();
            OutCome = new HashSet<OutCome>();
            OutComeHistory = new HashSet<OutComeHistory>();
            OutcomeConflict = new HashSet<OutcomeConflict>();
            WorkingPlanCollection = new HashSet<WorkingPlanCollection>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalExamItem { get; set; }
        public int CompleteExamItem { get; set; }
        public int TotalPlace { get; set; }
        public int CompletePlace { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolPathPeriodId { get; set; }

        public virtual PatrolPath PatrolPath { get; set; }
        public virtual PatrolPathPeriod PatrolPathPeriod { get; set; }
        public virtual ICollection<Checkin> Checkin { get; set; }
        public virtual ICollection<CheckinConflictn> CheckinConflictn { get; set; }
        public virtual ICollection<ExpandAllowDay> ExpandAllowDay { get; set; }
        public virtual ICollection<ExpandExamItem> ExpandExamItem { get; set; }
        public virtual ICollection<ExpandPlace> ExpandPlace { get; set; }
        public virtual ICollection<ManagerApproval> ManagerApproval { get; set; }
        public virtual ICollection<OutCome> OutCome { get; set; }
        public virtual ICollection<OutComeHistory> OutComeHistory { get; set; }
        public virtual ICollection<OutcomeConflict> OutcomeConflict { get; set; }
        public virtual ICollection<WorkingPlanCollection> WorkingPlanCollection { get; set; }
    }
}
