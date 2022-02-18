using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class ShiftSchedulingRulesAdapterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "請輸入名稱")]
        public string Name { get; set; }
        public string RuleType { get; set; }
        public int ContractorShiftId1 { get; set; }
        public int? ContractorShiftId2 { get; set; }
        public string Remark { get; set; }
        public int?[] PeopleId { get; set; }

        public virtual ContractorShift ContractorShiftId1Navigation { get; set; }
        public virtual ContractorShift ContractorShiftId2Navigation { get; set; }
        public virtual ICollection<ShiftSchedulingRulesPeople> ShiftSchedulingRulesPeople { get; set; }
    }
}
