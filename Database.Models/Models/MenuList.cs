using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class MenuList
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public int Index { get; set; }
    }
}
