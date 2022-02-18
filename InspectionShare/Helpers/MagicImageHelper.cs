using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionShare.Helpers
{
    public class MagicImageHelper
    {
        #region 圖片相關使用到的常數
        public static string ImageTempFolderName { get; set; } = "Temp";
        public static string ImageFolderName { get; set; } = "Images";
        public static string ImageEndPoint { get; set; } = "/Images";
        public static string ImageForWebPostfix { get; set; } = "-web";
        public static int TotalImageFolder { get; set; } = 300;
        public static List<string> AvailableImageExtension = new List<string>()
        {
            "png", "jpg", "jpeg"
        };
        #endregion
    }
}
