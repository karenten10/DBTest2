using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class ExceptionMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string DeviceVersion { get; set; }
        public string Class { get; set; }
        public string Function { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionDetail { get; set; }
        public DateTime ExceptionTime { get; set; }
    }
}
