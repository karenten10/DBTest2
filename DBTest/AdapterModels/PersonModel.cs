using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InspectionBlazor.AdapterModels {
    public class PersonAdapterModel {
        public int Id { get; set; }
        [Required(ErrorMessage = "必須要輸入值")]
        //[RegularExpression(@"^(?i)(?!administrator|admin|httc)(.+)|(.+)(?<!administrator|admin|httc)$", ErrorMessage = "禁用名稱")]
        public string Account { get; set; }
        public string PasswordPlainText { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        [Required(ErrorMessage = "必須要輸入值")]
        public string Name { get; set; }
        [Required(ErrorMessage = "必須要輸入值")]
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? JobTitleId { get; set; }
        public string JobTitleName { get; set; }

        public long? AuthorityId { get; set; }

        public int?[] PersonManagerId { get; set; }
        public long?[] PersonDepartmentId { get; set; }
        public string PersonDepartmentName { get; set; }    
        public List<ImageRepositoryAdapterModel> ImageRepository { get; set; } = new List<ImageRepositoryAdapterModel>();
        public int? LastUpdateUser { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string ShowLastUpdateUser { get; set; }
        public string ShowLastUpdateTime { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Address { get; set; }
        public string WorkingType { get; set; }
        public string Qualifications { get; set; }
        public string Education { get; set; }
        public string SchoolDepartment { get; set; }
        public string Experience { get; set; }
        public string License { get; set; }
        public string Memo { get; set; }
        public string ContractorName { get; set; }
    }
}
