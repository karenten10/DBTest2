using System;

namespace InspectionBlazor.DataModels {
    public class 主管簽核全流程Model {
        public string 單據狀態 { get; set; }
        public string 簽核人員 { get; set; }
        public DateTime 簽核時間 { get; set; }

        public string 簽核備註 { get; set; }
    }
}
