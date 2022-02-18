using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionShare.Helpers
{
    public class MagicDocHelper
    {
        #region 圖片相關使用到的常數
        public static string DocTempFolderName { get; set; } = "Temp";
        public static string DocFolderName { get; set; } = "Docs";
        public static string DocEndPoint { get; set; } = "/Docs";
        public static string DocForWebPostfix { get; set; } = "-web";
        public static int TotalDocFolder { get; set; } = 10;
        public static List<string> AvailableDocExtension = new List<string>()
        {
            "doc", "pdf",
            "png", "jpg", "jpeg"
        };
        #endregion
    }
}
