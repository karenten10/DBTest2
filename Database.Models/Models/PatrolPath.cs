using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class PatrolPath
    {
        public PatrolPath()
        {
            Checkin = new HashSet<Checkin>();
            CheckinConflictn = new HashSet<CheckinConflictn>();
            EquipmentExam = new HashSet<EquipmentExam>();
            Expand = new HashSet<Expand>();
            FormPath = new HashSet<FormPath>();
            OutCome = new HashSet<OutCome>();
            OutComeHistory = new HashSet<OutComeHistory>();
            OutcomeConflict = new HashSet<OutcomeConflict>();
            PatrolGroupNpath = new HashSet<PatrolGroupNpath>();
            PatrolPathNplace = new HashSet<PatrolPathNplace>();
            PatrolPathPeriod = new HashSet<PatrolPathPeriod>();
            PatrolPathScope = new HashSet<PatrolPathScope>();
            PatrolPathNequipmentTemp = new HashSet<PatrolPathNequipmentTemp>();
            WorkSchedule = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string InspectByOrder { get; set; }
        public string InspectByPda { get; set; }
        public string TurnOnGps { get; set; }
        public string ShowRemark { get; set; }
        public string RealTimeTrans { get; set; }
        public long? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Checkin> Checkin { get; set; }
        public virtual ICollection<CheckinConflictn> CheckinConflictn { get; set; }
        public virtual ICollection<EquipmentExam> EquipmentExam { get; set; }
        public virtual ICollection<Expand> Expand { get; set; }
        public virtual ICollection<FormPath> FormPath { get; set; }
        public virtual ICollection<OutCome> OutCome { get; set; }
        public virtual ICollection<OutComeHistory> OutComeHistory { get; set; }
        public virtual ICollection<OutcomeConflict> OutcomeConflict { get; set; }
        public virtual ICollection<PatrolGroupNpath> PatrolGroupNpath { get; set; }
        public virtual ICollection<PatrolPathNplace> PatrolPathNplace { get; set; }
        public virtual ICollection<PatrolPathPeriod> PatrolPathPeriod { get; set; }
        public virtual ICollection<PatrolPathScope> PatrolPathScope { get; set; }
        public virtual ICollection<PatrolPathNequipmentTemp> PatrolPathNequipmentTemp { get; set; }
        public virtual ICollection<WorkSchedule> WorkSchedule { get; set; }
    }
}
