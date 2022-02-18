using System;
using System.Collections.Generic;

namespace Database.Models.Models
{
    public partial class SystemEnvironment
    {
        public int Id { get; set; }
        public int CurrentSqliteFileIndex { get; set; }
        public string SqliteFilename1 { get; set; }
        public string SqliteFilename2 { get; set; }
        public string SqliteFilename3 { get; set; }
        public string File1Md5 { get; set; }
        public string File2Md5 { get; set; }
        public string File3Md5 { get; set; }
        public DateTime File1CreateTime { get; set; }
        public DateTime File2CreateTime { get; set; }
        public DateTime File3CreateTime { get; set; }
    }
}
