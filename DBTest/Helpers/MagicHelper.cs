//// using InspectionBlazor.DataModels;
using InspectionBlazor.DataModels;
using System;
using System.Collections.Generic;

namespace InspectionBlazor.Helpers
{
    public static class MagicHelper
    {
        #region 系統相關資訊
        public static string SysName { get; set; } = "電子巡檢管理系統";
        public static string SysCompany { get; set; } = "臺北榮民總醫院工務室";
        public static string SysVer { get; set; } = "3.6.1.0";
        public static string SysInfo { get; set; } = $"© {DateTime.Now.Year} {SysCompany} {SysName} {SysVer} 版";
        #endregion
        public static string StatusTitle { get; set; } = "是否停用";
        public static string StatusYes { get; set; } = "是";
        public static string StatusYesCode { get; set; } = "Y";
        public static string StatusNo { get; set; } = "否";
        public static string StatusNoCode { get; set; } = "N";
        public static string 主管簽核功能表代號 { get; set; } = "ManagerApproval";
        public static string EquipmentShutdown { get; set; } = "停車";
        public static int 改密碼天數 { get; set; } = 90;
        public static int DefalutPageSize { get; set; } = 10;
        public static int DefalutMasterPageSize { get; set; } = 3;
        public static string 到位 { get; set; } = "到位";
        public static string 巡檢 { get; set; } = "巡檢";
        public static string InspectionMapURL { get; set; }
        public static List<StatusSelectModel> StatusList { get; set; } = new List<StatusSelectModel>()
        {
            new StatusSelectModel(){ Code = StatusYesCode, Title = StatusYes },
            new StatusSelectModel(){ Code = StatusNoCode, Title = StatusNo },
        };

        public static string GetStatusName(string code)
        {
            return code == "Y" ? StatusYes : StatusNo;
        }

        public static string GetManagerApprovalStatusName(string code)
        {
            string result;
            switch (code.ToLower())
            {
                case "init":
                    result = "初始";
                    break;
                case "submit":
                    result = "上呈";
                    break;
                case "return":
                    result = "退件";
                    break;
                case "finish":
                    result = "完成";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        public static string GetFaultImproveStatusName(string code)
        {
            string result;
            switch (code.ToLower())
            {
                case "init":
                    result = "初始";
                    break;
                case "submit1":
                    result = "上呈";
                    break;
                case "cancel":
                    result = "註銷";
                    break;
                case "agreemaintain":
                    result = "同意維修";
                    break;
                case "improved":
                    result = "改善完成";
                    break;
                case "submit2":
                    result = "改善後上呈";
                    break;
                case "return1":
                    result = "改善後退件";
                    break;
                case "return2":
                    result = "改善同意退件";
                    break;
                case "finish":
                    result = "結單";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }

        public enum ContractorType
        {
            正興
        }

        public enum PeriodType
        {
            全天,
            上午,
            下午
        }

        public enum SegmentTypes
        {
            全日班 = 1,
            全日假,
            上午班下午假,
            上午假下午班
        }

        public enum ShiftRuleType
        {
            固定,
            輪班1,
            輪班2,
            輪班3,
            輪班4
        }

        public enum 正興部門
        {
            空調,
            鍋爐
        }

        public enum FormType
        {
            入庫單,
            入庫退回單,
            領料單,
            領料退回單,
            庫存異動單
        }
        public enum FormStatus
        {
            立單,
            確認
        }
    }
}
