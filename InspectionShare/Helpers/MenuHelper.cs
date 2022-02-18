using Database.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InspectionShare.Helpers
{
    public class MenuHelper
    {
        #region 父選單
        public static MenuDetail 首頁 { get; set; } = new MenuDetail { Code = "Home", Name = "首頁" };
        public static MenuDetail 帳號權限管理 { get; set; } = new MenuDetail { Code = "OperationalEnvironment", Name = "帳號權限管理" };
        public static MenuDetail 作業環境設定 { get; set; } = new MenuDetail { Code = "SystemOperation", Name = "作業環境設定" };
        public static MenuDetail 巡檢作業規劃 { get; set; } = new MenuDetail { Code = "InspectionOperation", Name = "巡檢作業規劃" };
        public static MenuDetail 排程計劃 { get; set; } = new MenuDetail { Code = "SchedulePlan", Name = "排程計劃" };
        public static MenuDetail 紀錄稽核作業 { get; set; } = new MenuDetail { Code = "AuditOperation", Name = "紀錄稽核作業" };
        public static MenuDetail 查詢作業 { get; set; } = new MenuDetail { Code = "QueryOperation", Name = "查詢作業" };
        public static MenuDetail 統計作業 { get; set; } = new MenuDetail { Code = "StatisticalWork", Name = "統計作業" };
        public static MenuDetail 報表作業 { get; set; } = new MenuDetail { Code = "ReportOperation", Name = "報表作業" };
        public static MenuDetail 庫存管理 { get; set; } = new MenuDetail { Code = "InventorySystem", Name = "庫存管理" };
        public static MenuDetail 請修管理 { get; set; } = new MenuDetail { Code = "RepairSystem", Name = "請修管理" };
        public static MenuDetail 儀表板 { get; set; } = new MenuDetail { Code = "Dashboard", Name = "儀表板" };
        public static MenuDetail 後台管理 { get; set; } = new MenuDetail { Code = "SystemSettings", Name = "後台管理" };
        public static MenuDetail 登出 { get; set; } = new MenuDetail { Code = "Logout", Name = "登出" };
        #endregion

        #region 帳號權限管理 - 子選單
        public static MenuDetail 人員資料設定 { get; set; } = new MenuDetail { Code = "Person", Name = "人員資料設定" };
        public static MenuDetail 人員異動維護 { get; set; } = new MenuDetail { Code = "PersonnelChange", Name = "人員異動維護" };
        public static MenuDetail 部門單位設定 { get; set; } = new MenuDetail { Code = "Department", Name = "部門單位設定" };
        public static MenuDetail 職稱設定 { get; set; } = new MenuDetail { Code = "JobTitle", Name = "職稱設定" };
        public static MenuDetail 帳號權限設定 { get; set; } = new MenuDetail { Code = "Authority", Name = "帳號權限設定" };
        public static MenuDetail 修改密碼 { get; set; } = new MenuDetail { Code = "PersonInfoModify", Name = "修改密碼" };
        #endregion

        #region 作業環境設定 - 子選單
        public static MenuDetail 群組設定 { get; set; } = new MenuDetail { Code = "PatrolGroup", Name = "群組設定" };
        public static MenuDetail 路線設定 { get; set; } = new MenuDetail { Code = "PatrolPath", Name = "路線設定" };
        public static MenuDetail 範圍設定 { get; set; } = new MenuDetail { Code = "PatrolScope", Name = "範圍設定" };
        public static MenuDetail 巡檢點設定 { get; set; } = new MenuDetail { Code = "PatrolPlace", Name = "巡檢點設定" };
        public static MenuDetail 巡檢路線規劃 { get; set; } = new MenuDetail { Code = "PatrolPathNPlace", Name = "巡檢路線規劃" };
        public static MenuDetail 區段設定 { get; set; } = new MenuDetail { Code = "Section", Name = "區段設定" };
        public static MenuDetail 設備設定 { get; set; } = new MenuDetail { Code = "Equipment", Name = "設備設定" };
        public static MenuDetail 設備檢查項目範本 { get; set; } = new MenuDetail { Code = "EquipmentTemplate", Name = "設備檢查項目範本" };
        public static MenuDetail 行動裝置綁定 { get; set; } = new MenuDetail { Code = "MobileDevice", Name = "行動裝置綁定" };
        public static MenuDetail 行動裝置設定 { get; set; } = new MenuDetail { Code = "PadSettings", Name = "行動裝置設定" };
        public static MenuDetail 表單模組設定 { get; set; } = new MenuDetail { Code = "FormReport", Name = "表單模組設定" };
        public static MenuDetail 表單模組對應設定 { get; set; } = new MenuDetail { Code = "FormPath", Name = "表單模組對應設定" };
        public static MenuDetail 行動裝置備註常用語 { get; set; } = new MenuDetail { Code = "PadMessage", Name = "行動裝置備註常用語" };
        public static MenuDetail 郵件SMTP設定 { get; set; } = new MenuDetail { Code = "SMTP", Name = "郵件SMTP設定" };
        #endregion

        #region 巡檢作業規劃 - 子選單
        public static MenuDetail 時段設定 { get; set; } = new MenuDetail { Code = "PatrolPathPeriod", Name = "時段設定" };
        public static MenuDetail 不巡檢行事曆 { get; set; } = new MenuDetail { Code = "NoInspectCalendar", Name = "不巡檢行事曆" };
        #endregion

        #region 排程計劃 - 子選單
        public static MenuDetail 工作種類維護 { get; set; } = new MenuDetail { Code = "WorkType", Name = "工作種類維護" };
        public static MenuDetail 班別維護 { get; set; } = new MenuDetail { Code = "ContractorShift", Name = "班別維護" };
        public static MenuDetail 假別維護 { get; set; } = new MenuDetail { Code = "LeaveType", Name = "假別維護" };
        public static MenuDetail 排班規則維護 { get; set; } = new MenuDetail { Code = "ShiftSchedulingRules", Name = "排班規則維護" };
        public static MenuDetail 契約人數維護 { get; set; } = new MenuDetail { Code = "ContractEmployees", Name = "契約人數維護" };
        public static MenuDetail 出勤表 { get; set; } = new MenuDetail { Code = "AttendanceRegister", Name = "出勤表" };
        public static MenuDetail 工作計劃表 { get; set; } = new MenuDetail { Code = "WorkingPlan", Name = "工作計劃表" };
        public static MenuDetail 工作計劃書 { get; set; } = new MenuDetail { Code = "WorkingPlanCollection", Name = "工作計劃書" };
        public static MenuDetail 工作日誌 { get; set; } = new MenuDetail { Code = "WorkLog", Name = "工作日誌" };
        public static MenuDetail 工作排程 { get; set; } = new MenuDetail { Code = "WorkSchedule", Name = "工作排程" };
        #endregion

        #region 紀錄稽核作業 - 子選單
        public static MenuDetail 主管簽核作業 { get; set; } = new MenuDetail { Code = "ManagerApproval", Name = "主管簽核作業" };
        public static MenuDetail 補單作業 { get; set; } = new MenuDetail { Code = "OutComeEdit", Name = "補單作業" };
        public static MenuDetail 缺失改善作業 { get; set; } = new MenuDetail { Code = "FaultImprove", Name = "缺失改善作業" };
        public static MenuDetail 簽核常用語表單設定 { get; set; } = new MenuDetail { Code = "CanMessageForm", Name = "簽核常用語表單設定" };
        public static MenuDetail 簽核常用語明細設定 { get; set; } = new MenuDetail { Code = "CanMessageExamItem", Name = "簽核常用語明細設定" };
        #endregion

        #region 查詢作業 - 子選單
        public static MenuDetail 巡檢紀錄查詢 { get; set; } = new MenuDetail { Code = "InspectionRecordAll", Name = "巡檢紀錄查詢" };
        public static MenuDetail 異常紀錄查詢 { get; set; } = new MenuDetail { Code = "InspectionRecordAbnormal", Name = "異常紀錄查詢" };
        public static MenuDetail 隨手拍 { get; set; } = new MenuDetail { Code = "HandyPhoto", Name = "隨手拍" };
        public static MenuDetail 圖資瀏覽 { get; set; } = new MenuDetail { Code = "InspcetMap", Name = "圖資瀏覽" };
        public static MenuDetail 舊巡檢紀錄查詢 { get; set; } = new MenuDetail { Code = "OldInspectionRecordAll", Name = "舊巡檢紀錄查詢" };
        public static MenuDetail 舊異常紀錄查詢 { get; set; } = new MenuDetail { Code = "OldInspectionRecordAbnormal", Name = "舊異常紀錄查詢" };
        public static MenuDetail 設備更換查詢 { get; set; } = new MenuDetail { Code = "EquipmentMaintainCycle", Name = "設備更換查詢" };
        public static MenuDetail 請修作業逾期查詢 { get; set; } = new MenuDetail { Code = "MWMasterOverdue", Name = "請修作業逾期查詢" };
        public static MenuDetail 巡檢逾期查詢 { get; set; } = new MenuDetail { Code = "InspectionRecordDue", Name = "巡檢逾期查詢" };
        public static MenuDetail 耗材管理追蹤 { get; set; } = new MenuDetail { Code = "PFMasterMonthly", Name = "耗材管理追蹤" };
        #endregion

        #region 統計作業 - 子選單
        public static MenuDetail 巡檢紀錄統計 { get; set; } = new MenuDetail { Code = "InspectionRecordStatistics", Name = "巡檢紀錄統計" };
        public static MenuDetail 檢查項目趨勢圖 { get; set; } = new MenuDetail { Code = "EquipmentTrendChart", Name = "檢查項目趨勢圖" };
        public static MenuDetail 各課路線統計 { get; set; } = new MenuDetail { Code = "CourseStatistics", Name = "各課路線統計" };
        public static MenuDetail 統計報表 { get; set; } = new MenuDetail { Code = "InspectionRecordStatisticsIndex", Name = "統計報表" };
        public static MenuDetail 檢查動態 { get; set; } = new MenuDetail { Code = "InspectionRecordIndex", Name = "檢查動態" };
        public static MenuDetail 缺失改善 { get; set; } = new MenuDetail { Code = "FaultImproveIndex", Name = "缺失改善" };
        public static MenuDetail 巡檢統計 { get; set; } = new MenuDetail { Code = "CourseStatisticsIndex", Name = "巡檢統計" };

        #endregion

        #region 報表作業 - 子選單        
        public static MenuDetail 每月工作日誌 { get; set; } = new MenuDetail { Code = "MonthWorkLogReport", Name = "每月工作日誌" };
        public static MenuDetail 每月巡檢紀錄 { get; set; } = new MenuDetail { Code = "MonthInspectionReport", Name = "每月巡檢紀錄" };
        public static MenuDetail 每日紀錄表 { get; set; } = new MenuDetail { Code = "CheckPath", Name = "每日紀錄表" };
        public static MenuDetail 消防設備維護保養總表 { get; set; } = new MenuDetail { Code = "FireFight", Name = "消防設備維護保養總表" };
        public static MenuDetail 庫存紀錄表 { get; set; } = new MenuDetail { Code = "StockReport", Name = "庫存紀錄表" };
        public static MenuDetail 空調紀錄表 { get; set; } = new MenuDetail { Code = "AirConditionerReport", Name = "空調紀錄表" };
        #endregion

        #region 庫存管理 - 子選單
        public static MenuDetail 組件設定 { get; set; } = new MenuDetail { Code = "PartInfo", Name = "組件設定" };
        public static MenuDetail 設備對應組件 { get; set; } = new MenuDetail { Code = "CMMS", Name = "設備對應組件" };
        public static MenuDetail 入庫單 { get; set; } = new MenuDetail { Code = "PFMasterStockInPage", Name = "入庫單" };
        public static MenuDetail 入庫退回單 { get; set; } = new MenuDetail { Code = "PFMasterStockInReturnPage", Name = "入庫退回單" };
        public static MenuDetail 領料單 { get; set; } = new MenuDetail { Code = "PFMasterStockOutPage", Name = "領料單" };
        public static MenuDetail 領料退回單 { get; set; } = new MenuDetail { Code = "PFMasterStockOutReturnPage", Name = "領料退回單" };
        public static MenuDetail 庫存異動單 { get; set; } = new MenuDetail { Code = "PFMasterStockModifyPage", Name = "庫存異動單" };
        public static MenuDetail 組件倉儲 { get; set; } = new MenuDetail { Code = "PartLocationInfo", Name = "組件倉儲" };
        public static MenuDetail 請修作業 { get; set; } = new MenuDetail { Code = "MWMaster", Name = "請修作業" };
        public static MenuDetail 庫存異動紀錄 { get; set; } = new MenuDetail { Code = "StockChangeHistory", Name = "庫存異動紀錄" };
        #endregion

        #region 請修管理 - 子選單
        public static MenuDetail 請修群組 { get; set; } = new MenuDetail { Code = "RepairEquipmentGroup", Name = "請修設備群組" };
        public static MenuDetail 請修設備 { get; set; } = new MenuDetail { Code = "RepairEquipment", Name = "請修設備維護" };
        public static MenuDetail 請修設備對應人員 { get; set; } = new MenuDetail { Code = "RepairEquipmentNPerson", Name = "請修設備對應人員" };
        public static MenuDetail 請修備註 { get; set; } = new MenuDetail { Code = "RepairRemaker", Name = "請修備註" };
        public static MenuDetail 請修設備對應備註 { get; set; } = new MenuDetail { Code = "RepairEquipmentNRepairRemaker", Name = "請修設備對應備註" };
        public static MenuDetail 開立請修單 { get; set; } = new MenuDetail { Code = "RepairMaster", Name = "開立請修單" };
        #endregion

        #region 儀表板 - 子選單
        public static MenuDetail 戰情儀表板 { get; set; } = new MenuDetail { Code = "SituationDashboard", Name = "戰情儀表板" };
        public static MenuDetail 指標儀表板 { get; set; } = new MenuDetail { Code = "TargetDashboard", Name = "指標儀表板" };
        #endregion

        #region 資料導入 - 子選單
        public static MenuDetail 展開產生資料庫 { get; set; } = new MenuDetail { Code = "ExpandSQLite", Name = "展開/產生資料庫" };
        public static MenuDetail 資料導入 { get; set; } = new MenuDetail { Code = "ImportData", Name = "資料導入" };
        public static MenuDetail 平板更新檔維護 { get; set; } = new MenuDetail { Code = "UpdateFile", Name = "平板更新檔維護" };
        #endregion

        #region 選單設定
        /// <summary>帳號權限設定中以下功能的CheckBox將反灰並勾起</summary>
        public static string[] 必要選單 { get; set; } = 
            { 首頁.Code, 帳號權限管理.Code, 修改密碼.Code, 登出.Code };

        /// <summary>帳號權限設定中以下功能不會出現</summary>
        public static string[] 一般使用者權限排除 { get; set; } = 
            { 帳號權限設定.Code, 行動裝置設定.Code, 行動裝置綁定.Code, 後台管理.Code, 展開產生資料庫.Code, 
            資料導入.Code, 平板更新檔維護.Code, 郵件SMTP設定.Code };

        /// <summary>客戶的管理員帳號(admin/administraor)以下功能不會出現</summary>
        public static string[] 客戶系統管理員權限排除 { get; set; } = 
            { 帳號權限設定.Code, 後台管理.Code, 展開產生資料庫.Code, 資料導入.Code, 平板更新檔維護.Code };
        #endregion

        //https://fontawesome.com/v4.7/icons/ 由此尋找Icon
        public static string GetIconCss(string code)
        {
            if (code == 首頁.Code)
                return "fa fa-home";
            else if (code == 帳號權限管理.Code)
                return "fa fa-cog";
            else if (code == 作業環境設定.Code)
                return "fa fa-sitemap";
            else if (code == 巡檢作業規劃.Code)
                return "fa fa-calendar-check-o";
            else if (code == 排程計劃.Code)
                return "fa fa-list-alt";
            else if (code == 紀錄稽核作業.Code)
                return "fa fa-check-square-o";
            else if (code == 查詢作業.Code)
                return "fa fa-filter";
            else if (code == 統計作業.Code)
                return "fa fa-bar-chart";
            else if (code == 報表作業.Code)
                return "fa fa-file-text";
            else if (code == 後台管理.Code)
                return "fa fa-cogs";
            else if (code == 登出.Code)
                return "fa fa-sign-out";
            else if (code == 庫存管理.Code)
                return "fa fa-database";
            else if (code == 儀表板.Code)
                return "fa fa-tachometer";
            else if (code == 請修管理.Code)
                return "fa fa-briefcase";
            else
                return "fa fa-caret-right";
        }

        public static List<MenuList> MenuListInit()
        {
            List<MenuList> menuLists = new List<MenuList>();
            //▂▂▂▂首頁▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 首頁.Code,
                Name = 首頁.Name,
                Index = 1000
            });
            //▂▂▂▂帳號權限管理▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 帳號權限管理.Code,
                Name = 帳號權限管理.Name,
                Index = 2000
            });
            menuLists.Add(new MenuList
            {
                Code = 人員資料設定.Code,
                Name = 人員資料設定.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2010
            });
            menuLists.Add(new MenuList
            {
                Code = 人員異動維護.Code,
                Name = 人員異動維護.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2020
            });
            menuLists.Add(new MenuList
            {
                Code = 部門單位設定.Code,
                Name = 部門單位設定.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2030
            });
            menuLists.Add(new MenuList
            {
                Code = 職稱設定.Code,
                Name = 職稱設定.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2040
            });
            menuLists.Add(new MenuList
            {
                Code = 帳號權限設定.Code,
                Name = 帳號權限設定.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2050
            });
            menuLists.Add(new MenuList
            {
                Code = 修改密碼.Code,
                Name = 修改密碼.Name,
                ParentCode = 帳號權限管理.Code,
                Index = 2060
            });
            //▂▂▂▂作業環境設定▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 作業環境設定.Code,
                Name = 作業環境設定.Name,
                Index = 3000
            });
            menuLists.Add(new MenuList
            {
                Code = 群組設定.Code,
                Name = 群組設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3005
            });
            menuLists.Add(new MenuList
            {
                Code = 路線設定.Code,
                Name = 路線設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3010
            });
            menuLists.Add(new MenuList
            {
                Code = 範圍設定.Code,
                Name = 範圍設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3020
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢點設定.Code,
                Name = 巡檢點設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3030
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢路線規劃.Code,
                Name = 巡檢路線規劃.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3040
            });
            menuLists.Add(new MenuList
            {
                Code = 區段設定.Code,
                Name = 區段設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3050
            });
            menuLists.Add(new MenuList
            {
                Code = 設備設定.Code,
                Name = 設備設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3060
            });
            menuLists.Add(new MenuList
            {
                Code = 設備檢查項目範本.Code,
                Name = 設備檢查項目範本.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3070
            });
            menuLists.Add(new MenuList
            {
                Code = 行動裝置綁定.Code,
                Name = 行動裝置綁定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3100
            });
            menuLists.Add(new MenuList
            {
                Code = 行動裝置設定.Code,
                Name = 行動裝置設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3110
            });
            menuLists.Add(new MenuList
            {
                Code = 表單模組設定.Code,
                Name = 表單模組設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3120
            });
            menuLists.Add(new MenuList
            {
                Code = 表單模組對應設定.Code,
                Name = 表單模組對應設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3130
            });
            menuLists.Add(new MenuList
            {
                Code = 行動裝置備註常用語.Code,
                Name = 行動裝置備註常用語.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3140
            });
            menuLists.Add(new MenuList
            {
                Code = 郵件SMTP設定.Code,
                Name = 郵件SMTP設定.Name,
                ParentCode = 作業環境設定.Code,
                Index = 3150
            });
            //▂▂▂▂巡檢作業規劃▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 巡檢作業規劃.Code,
                Name = 巡檢作業規劃.Name,
                Index = 4000
            });
            menuLists.Add(new MenuList
            {
                Code = 時段設定.Code,
                Name = 時段設定.Name,
                ParentCode = 巡檢作業規劃.Code,
                Index = 4010
            });
            menuLists.Add(new MenuList
            {
                Code = 不巡檢行事曆.Code,
                Name = 不巡檢行事曆.Name,
                ParentCode = 巡檢作業規劃.Code,
                Index = 4020
            });
            //▂▂▂▂排程計劃▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 排程計劃.Code,
                Name = 排程計劃.Name,
                Index = 4200
            });
            menuLists.Add(new MenuList
            {
                Code = 工作種類維護.Code,
                Name = 工作種類維護.Name,
                ParentCode = 排程計劃.Code,
                Index = 4210
            });
            menuLists.Add(new MenuList
            {
                Code = 班別維護.Code,
                Name = 班別維護.Name,
                ParentCode = 排程計劃.Code,
                Index = 4220
            });
            menuLists.Add(new MenuList
            {
                Code = 假別維護.Code,
                Name = 假別維護.Name,
                ParentCode = 排程計劃.Code,
                Index = 4230
            });
            menuLists.Add(new MenuList
            {
                Code = 排班規則維護.Code,
                Name = 排班規則維護.Name,
                ParentCode = 排程計劃.Code,
                Index = 4240
            });
            menuLists.Add(new MenuList
            {
                Code = 契約人數維護.Code,
                Name = 契約人數維護.Name,
                ParentCode = 排程計劃.Code,
                Index = 4250
            });
            menuLists.Add(new MenuList
            {
                Code = 出勤表.Code,
                Name = 出勤表.Name,
                ParentCode = 排程計劃.Code,
                Index = 4260
            });
            menuLists.Add(new MenuList
            {
                Code = 工作計劃表.Code,
                Name = 工作計劃表.Name,
                ParentCode = 排程計劃.Code,
                Index = 4270
            });
            menuLists.Add(new MenuList
            {
                Code = 工作計劃書.Code,
                Name = 工作計劃書.Name,
                ParentCode = 排程計劃.Code,
                Index = 4280
            });
            menuLists.Add(new MenuList
            {
                Code = 工作日誌.Code,
                Name = 工作日誌.Name,
                ParentCode = 排程計劃.Code,
                Index = 4290
            });
            menuLists.Add(new MenuList
            {
                Code = 工作排程.Code,
                Name = 工作排程.Name,
                ParentCode = 排程計劃.Code,
                Index = 4300
            });
            //▂▂▂▂紀錄稽核作業▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 紀錄稽核作業.Code,
                Name = 紀錄稽核作業.Name,
                Index = 5000
            });
            menuLists.Add(new MenuList
            {
                Code = 補單作業.Code,
                Name = 補單作業.Name,
                ParentCode = 紀錄稽核作業.Code,
                Index = 5010
            });
            menuLists.Add(new MenuList
            {
                Code = 主管簽核作業.Code,
                Name = 主管簽核作業.Name,
                ParentCode = 紀錄稽核作業.Code,
                Index = 5020
            });
            menuLists.Add(new MenuList
            {
                Code = 缺失改善作業.Code,
                Name = 缺失改善作業.Name,
                ParentCode = 紀錄稽核作業.Code,
                Index = 5030
            });
            menuLists.Add(new MenuList
            {
                Code = 簽核常用語表單設定.Code,
                Name = 簽核常用語表單設定.Name,
                ParentCode = 紀錄稽核作業.Code,
                Index = 5040
            });
            menuLists.Add(new MenuList
            {
                Code = 簽核常用語明細設定.Code,
                Name = 簽核常用語明細設定.Name,
                ParentCode = 紀錄稽核作業.Code,
                Index = 5050
            });
            //▂▂▂▂查詢作業▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 查詢作業.Code,
                Name = 查詢作業.Name,
                Index = 6000
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢紀錄查詢.Code,
                Name = 巡檢紀錄查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6010
            });
            menuLists.Add(new MenuList
            {
                Code = 異常紀錄查詢.Code,
                Name = 異常紀錄查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6020
            });
            menuLists.Add(new MenuList
            {
                Code = 舊巡檢紀錄查詢.Code,
                Name = 舊巡檢紀錄查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6021
            });
            menuLists.Add(new MenuList
            {
                Code = 舊異常紀錄查詢.Code,
                Name = 舊異常紀錄查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6022
            });
            menuLists.Add(new MenuList
            {
                Code = 隨手拍.Code,
                Name = 隨手拍.Name,
                ParentCode = 查詢作業.Code,
                Index = 6030
            });
            menuLists.Add(new MenuList
            {
                Code = 圖資瀏覽.Code,
                Name = 圖資瀏覽.Name,
                ParentCode = 查詢作業.Code,
                Index = 6040
            });
            menuLists.Add(new MenuList
            {
                Code = 設備更換查詢.Code,
                Name = 設備更換查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6050
            });
            menuLists.Add(new MenuList
            {
                Code = 請修作業逾期查詢.Code,
                Name = 請修作業逾期查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6060
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢逾期查詢.Code,
                Name = 巡檢逾期查詢.Name,
                ParentCode = 查詢作業.Code,
                Index = 6070
            });
            //▂▂▂▂統計作業▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 統計作業.Code,
                Name = 統計作業.Name,
                Index = 7000
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢紀錄統計.Code,
                Name = 巡檢紀錄統計.Name,
                ParentCode = 統計作業.Code,
                Index = 7010
            });
            menuLists.Add(new MenuList
            {
                Code = 檢查項目趨勢圖.Code,
                Name = 檢查項目趨勢圖.Name,
                ParentCode = 統計作業.Code,
                Index = 7020
            });
            menuLists.Add(new MenuList
            {
                Code = 各課路線統計.Code,
                Name = 各課路線統計.Name,
                ParentCode = 統計作業.Code,
                Index = 7030
            });
            menuLists.Add(new MenuList
            {
                Code = 統計報表.Code,
                Name = 統計報表.Name,
                ParentCode = 統計作業.Code,
                Index = 7040
            });
            menuLists.Add(new MenuList
            {
                Code = 檢查動態.Code,
                Name = 檢查動態.Name,
                ParentCode = 統計作業.Code,
                Index = 7050
            });
            menuLists.Add(new MenuList
            {
                Code = 缺失改善.Code,
                Name = 缺失改善.Name,
                ParentCode = 統計作業.Code,
                Index = 7060
            });
            menuLists.Add(new MenuList
            {
                Code = 巡檢統計.Code,
                Name = 巡檢統計.Name,
                ParentCode = 統計作業.Code,
                Index = 7070
            });
            //▂▂▂▂報表作業▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 報表作業.Code,
                Name = 報表作業.Name,
                Index = 8000
            });
            menuLists.Add(new MenuList
            {
                Code = 每日紀錄表.Code,
                Name = 每日紀錄表.Name,
                ParentCode = 報表作業.Code,
                Index = 8010
            });
            menuLists.Add(new MenuList
            {
                Code = 消防設備維護保養總表.Code,
                Name = 消防設備維護保養總表.Name,
                ParentCode = 報表作業.Code,
                Index = 8020
            });
            menuLists.Add(new MenuList
            {
                Code = 每月工作日誌.Code,
                Name = 每月工作日誌.Name,
                ParentCode = 報表作業.Code,
                Index = 8030
            });
            menuLists.Add(new MenuList
            {
                Code = 每月巡檢紀錄.Code,
                Name = 每月巡檢紀錄.Name,
                ParentCode = 報表作業.Code,
                Index = 8040
            });
            menuLists.Add(new MenuList
            {
                Code = 庫存紀錄表.Code,
                Name = 庫存紀錄表.Name,
                ParentCode = 報表作業.Code,
                Index = 8050
            });
            menuLists.Add(new MenuList
            {
                Code = 空調紀錄表.Code,
                Name = 空調紀錄表.Name,
                ParentCode = 報表作業.Code,
                Index = 8060
            });
            //▂▂▂▂庫存管理▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 庫存管理.Code,
                Name = 庫存管理.Name,
                Index = 9000
            });
            menuLists.Add(new MenuList
            {
                Code = 組件設定.Code,
                Name = 組件設定.Name,
                ParentCode = 庫存管理.Code,
                Index = 9010
            });
            menuLists.Add(new MenuList
            {
                Code = 組件倉儲.Code,
                Name = 組件倉儲.Name,
                ParentCode = 庫存管理.Code,
                Index = 9005
            });

            menuLists.Add(new MenuList
            {
                Code = 設備對應組件.Code,
                Name = 設備對應組件.Name,
                ParentCode = 庫存管理.Code,
                Index = 9020
            });
            menuLists.Add(new MenuList
            {
                Code = 入庫單.Code,
                Name = 入庫單.Name,
                ParentCode = 庫存管理.Code,
                Index = 9030
            });
            menuLists.Add(new MenuList
            {
                Code = 入庫退回單.Code,
                Name = 入庫退回單.Name,
                ParentCode = 庫存管理.Code,
                Index = 9040
            });
            menuLists.Add(new MenuList
            {
                Code = 領料單.Code,
                Name = 領料單.Name,
                ParentCode = 庫存管理.Code,
                Index = 9050
            });
            menuLists.Add(new MenuList
            {
                Code = 領料退回單.Code,
                Name = 領料退回單.Name,
                ParentCode = 庫存管理.Code,
                Index = 9060
            });
            menuLists.Add(new MenuList
            {
                Code = 庫存異動單.Code,
                Name = 庫存異動單.Name,
                ParentCode = 庫存管理.Code,
                Index = 9070
            });
            menuLists.Add(new MenuList
            {
                Code = 請修作業.Code,
                Name = 請修作業.Name,
                ParentCode = 庫存管理.Code,
                Index = 9080
            });
            menuLists.Add(new MenuList
            {
                Code = 庫存異動紀錄.Code,
                Name = 庫存異動紀錄.Name,
                ParentCode = 庫存管理.Code,
                Index = 9090
            });
            //▂▂▂▂儀表板▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 儀表板.Code,
                Name = 儀表板.Name,
                Index = 10000
            });
            menuLists.Add(new MenuList
            {
                Code = 戰情儀表板.Code,
                Name = 戰情儀表板.Name,
                ParentCode = 儀表板.Code,
                Index = 10010
            });
            menuLists.Add(new MenuList
            {
                Code = 指標儀表板.Code,
                Name = 指標儀表板.Name,
                ParentCode = 儀表板.Code,
                Index = 10020
            });
            //▂▂▂▂請修管理▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 請修管理.Code,
                Name = 請修管理.Name,
                Index = 11000
            });
            menuLists.Add(new MenuList
            {
                Code = 請修群組.Code,
                Name = 請修群組.Name,
                ParentCode = 請修管理.Code,
                Index = 11010
            });
            menuLists.Add(new MenuList
            {
                Code = 請修設備對應人員.Code,
                Name = 請修設備對應人員.Name,
                ParentCode = 請修管理.Code,
                Index = 11020
            });
            menuLists.Add(new MenuList
            {
                Code = 請修備註.Code,
                Name = 請修備註.Name,
                ParentCode = 請修管理.Code,
                Index = 11030
            });
            menuLists.Add(new MenuList
            {
                Code = 請修設備對應備註.Code,
                Name = 請修設備對應備註.Name,
                ParentCode = 請修管理.Code,
                Index = 11040
            });
            menuLists.Add(new MenuList
            {
                Code = 開立請修單.Code,
                Name = 開立請修單.Name,
                ParentCode = 請修管理.Code,
                Index = 11050
            });
            //▂▂▂▂後台管理▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 後台管理.Code,
                Name = 後台管理.Name,
                Index = 1000000
            });
            menuLists.Add(new MenuList
            {
                Code = 展開產生資料庫.Code,
                Name = 展開產生資料庫.Name,
                ParentCode = 後台管理.Code,
                Index = 1000010
            });
            menuLists.Add(new MenuList
            {
                Code = 資料導入.Code,
                Name = 資料導入.Name,
                ParentCode = 後台管理.Code,
                Index = 1000020
            });
            menuLists.Add(new MenuList
            {
                Code = 平板更新檔維護.Code,
                Name = 平板更新檔維護.Name,
                ParentCode = 後台管理.Code,
                Index = 1000030
            });

            //▂▂▂▂登出▂▂▂▂
            menuLists.Add(new MenuList
            {
                Code = 登出.Code,
                Name = 登出.Name,
                Index = int.MaxValue
            });

            return menuLists;
        }
    }

    public class MenuDetail
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
