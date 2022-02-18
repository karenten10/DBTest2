using Dapper;
using Database.Models.Models;
using InspectionBlazor.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class SituationDashboardService
    {
        private readonly InspectionDBContext context;

        public SituationDashboardService(InspectionDBContext context)
        {
            this.context = context;
        }

        #region Data1

        public async Task<List<SituationDashboardData1DataModel>> GetData1Async()
        {
            List<SituationDashboardData1DataModel> result = new List<SituationDashboardData1DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                    SELECT x.承攬商, x.總巡檢數, x.異常通報數, x.未到位數
                    FROM 
                    (SELECT c.DepartmentName AS 承攬商, 
                    COUNT(a.ExpandExamItemId) AS 總巡檢數,
                    SUM(CASE WHEN a.IsAbnormal = 'Y' THEN 1 ELSE 0 END) AS 異常通報數,
                    SUM(CASE WHEN a.UpdateTime IS NULL AND d.EndTime < GETDATE() THEN 1 ELSE 0 END) AS 未到位數
                    FROM dbo.OutCome a
                    INNER JOIN dbo.PatrolPath b ON b.Id = a.PatrolPathId
                    INNER JOIN dbo.Department c ON c.Id = b.DepartmentId
                    INNER JOIN dbo.[Expand] d ON d.Id = a.ExpandId
                    INNER JOIN dbo.PatrolPathPeriod e ON e.Id = d.PatrolPathPeriodId
                    WHERE e.Cycle IN ('每日', '每日多班別', '每週', '每月')
                    AND b.[Status] = 'N' AND e.[Status] = 'N'
                    AND LEFT(CONVERT(VARCHAR(8), d.BeginTime, 112), 6) = '{monthRange}'
                    GROUP BY c.DepartmentName) x
                    GROUP BY x.承攬商, x.總巡檢數, x.異常通報數, x.未到位數
                    ";

                var data = conn.Query<SituationDashboardData1DataModel>(strSql);
                if (data != null)
                {
                    result = data.ToList();
                    int 總異常通報數 = result.Select(x => x.異常通報數).Sum();
                    if (總異常通報數 > 0)
                    {
                        foreach (var item in result)
                        {
                            item.異常通報比例 = Math.Round(((double)item.異常通報數 / 總異常通報數) * 100, 2);
                        }
                    }
                }
            }

            await Task.Yield();
            return result;
        }

        public async Task<SituationDashboardData1TotalDataModel> GetData1TotalAsync()
        {
            SituationDashboardData1TotalDataModel result = new SituationDashboardData1TotalDataModel();

            var datas = await GetData1Async();

            if (datas != null)
            {
                result.未到位數 = result.未到位數 + datas.Select(x => x.未到位數).Sum();
                result.異常通報數 = result.異常通報數 + datas.Select(x => x.異常通報數).Sum();
                result.總巡檢數 = result.總巡檢數 + datas.Select(x => x.總巡檢數).Sum();
            }

            return result;
        }
        #endregion

        #region Data2
        public async Task<int> GetData2Async()
        {
            var datas = await GetData7Async();
            int result = datas.Select(x => x.維修通報數).Sum();
            return result;
        }
        #endregion

        #region Data3
        public async Task<int> GetData3Async()
        {
            var datas = await GetData7Async();
            int result = datas.Select(x => x.等待改善數).Sum();
            return result;
        }
        #endregion

        #region Data4
        public async Task<List<SituationDashboardData4DataModel>> GetData4Async()
        {
            List<SituationDashboardData4DataModel> result = new List<SituationDashboardData4DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {

                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT x.承攬商,
                                CAST(ROUND(SUM(x.到位數) * 100.0 / SUM(SUM(x.到位數)) OVER (), 2) AS NUMERIC(5, 2)) AS 到位率
                                FROM
                                (SELECT c.DepartmentName AS 承攬商, COUNT(1) AS 到位數
                                FROM dbo.OutCome a
                                INNER JOIN dbo.PatrolPath b ON b.Id = a.PatrolPathId
                                INNER JOIN dbo.Department c ON c.Id = b.DepartmentId
                                WHERE a.IsCompleted='Y' AND b.[Status]='N' AND c.[Status]='N'
                                AND LEFT(CONVERT(VARCHAR(8), a.UpdateTime, 112), 6) = '{monthRange}'
                                GROUP BY c.DepartmentName) x
                                GROUP BY x.承攬商
                                ";

                result = conn.Query<SituationDashboardData4DataModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        #endregion

        #region Data5
        public async Task<List<SituationDashboardData5DataModel>> GetData5Async()
        {
            List<SituationDashboardData5DataModel> result = new List<SituationDashboardData5DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {

                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT x.路線, 
                                CAST(ROUND(SUM(x.異常數) * 100.0 / SUM(SUM(x.異常數)) OVER (), 2) AS NUMERIC(5, 2)) AS 異常數比例
                                FROM
                                (SELECT b.[Name] AS 路線,SUM(CASE WHEN a.IsAbnormal = 'Y' THEN 1 ELSE 0 END) AS 異常數
                                FROM dbo.OutCome a
                                INNER JOIN dbo.PatrolPath b ON b.Id = a.PatrolPathId
                                INNER JOIN dbo.Department c ON c.Id = b.DepartmentId
                                INNER JOIN dbo.[Expand] d ON d.Id = a.ExpandId
                                INNER JOIN dbo.PatrolPathPeriod e ON e.Id = d.PatrolPathPeriodId
                                WHERE e.Cycle IN ('每日', '每日多班別', '每週', '每月')
                                AND b.[Status] = 'N' AND e.[Status] = 'N'
                                AND LEFT(CONVERT(VARCHAR(8), d.BeginTime, 112), 6) = '{monthRange}'
                                GROUP BY b.[Name]) x
                                WHERE x.異常數 > 0
                                GROUP BY x.路線,x.異常數
                                ORDER BY x.異常數 DESC
                                ";

                result = conn.Query<SituationDashboardData5DataModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        #endregion

        #region Data6
        public async Task<List<SituationDashboardData6DataModel>> GetData6Async()
        {
            List<SituationDashboardData6DataModel> result = new List<SituationDashboardData6DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {

                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT x.範圍, 
                                CAST(ROUND(SUM(x.異常數) * 100.0 / SUM(SUM(x.異常數)) OVER (), 2) AS NUMERIC(5, 2)) AS 異常數比例
                                FROM
                                (SELECT i.[Name] AS 範圍,SUM(CASE WHEN a.IsAbnormal = 'Y' THEN 1 ELSE 0 END) AS 異常數
                                FROM dbo.OutCome a
                                INNER JOIN dbo.PatrolPath b ON b.Id = a.PatrolPathId
                                INNER JOIN dbo.Department c ON c.Id = b.DepartmentId
                                INNER JOIN dbo.[Expand] d ON d.Id = a.ExpandId
                                INNER JOIN dbo.PatrolPathPeriod e ON e.Id = d.PatrolPathPeriodId
                                INNER JOIN dbo.ExpandExamItem f ON f.Id = a.ExpandExamItemId
                                INNER JOIN dbo.EquipmentExamItem g ON g.Id = f.EquipmentExamItemId
                                INNER JOIN dbo.PatrolPlace h ON h.Id = g.PatrolPlaceId
                                INNER JOIN dbo.PatrolScope i ON i.Id = h.PatrolScopeId
                                WHERE e.Cycle IN ('每日', '每日多班別', '每週', '每月')
                                AND b.[Status] = 'N' AND e.[Status] = 'N' AND g.[Status] = 'N' AND h.[Status] = 'N' AND i.[Status] = 'N'
                                AND LEFT(CONVERT(VARCHAR(8), d.BeginTime, 112), 6) = '{monthRange}'
                                GROUP BY i.[Name]) x
                                WHERE x.異常數 > 0
                                GROUP BY x.範圍,x.異常數
                                ORDER BY x.異常數 DESC
                                ";

                result = conn.Query<SituationDashboardData6DataModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        #endregion

        #region Data7
        public async Task<List<SituationDashboardData7DataModel>> GetData7Async()
        {
            List<SituationDashboardData7DataModel> result = new List<SituationDashboardData7DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT e.DepartmentName AS 單位,COUNT(1) AS 維修通報數,
                                SUM(CASE WHEN a.Status='立單' THEN 1 ELSE 0 END) AS 等待改善數
                                FROM dbo.MWMaster a
                                INNER JOIN dbo.MWDetail b ON b.ParentId=a.FormId
                                INNER JOIN dbo.Person c ON c.Id=a.PersonInChargeId
                                INNER JOIN dbo.PersonDepartment d ON d.PersonId=c.Id
                                INNER JOIN dbo.Department e ON e.Id = d.DepartmentId
                                WHERE LEFT(CONVERT(VARCHAR(8), a.CreateTime, 112), 6) = '{monthRange}'
                                GROUP BY e.DepartmentName
                                ";

                result = conn.Query<SituationDashboardData7DataModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        #endregion

        #region Data8
        public async Task<List<SituationDashboardData8DataModel>> GetData8Async()
        {
            List<SituationDashboardData8DataModel> result = new List<SituationDashboardData8DataModel>();
            string monthRange = DateTime.Today.ToString("yyyyMM");

            using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {

                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT CONVERT(VARCHAR(8), d.BeginTime, 112)+'~'+CONVERT(VARCHAR(8), d.EndTime, 112) AS 期間,
                                b.[Name] AS 巡檢路線, e.[Name] AS 巡檢時段, a.UpdateTime AS 到位時間, c.DepartmentName AS 單位,
                                j.[Name] AS 記錄人員, 
                                CASE WHEN g.ExamConditionName = '範圍' THEN CAST(CAST(g.Value1 AS float) AS VARCHAR(100))+'~'+CAST(CAST(g.Value2 AS float) AS VARCHAR(100)) ELSE g.ExamConditionName+':'+CAST(CAST(g.Value1 AS float) AS VARCHAR(100)) END AS 標準值,
                                CASE WHEN g.ExamConditionName = '標準' THEN a.MultiChoice ELSE CAST(a.Value AS VARCHAR(100)) END 異常紀錄值,
                                k.EquipmentName AS 設備名稱, n.[Name] AS 簽核人, h.[Name] AS Tag編號
                                FROM dbo.OutCome a
                                INNER JOIN dbo.PatrolPath b ON b.Id = a.PatrolPathId
                                INNER JOIN dbo.Department c ON c.Id = b.DepartmentId
                                INNER JOIN dbo.[Expand] d ON d.Id = a.ExpandId
                                INNER JOIN dbo.PatrolPathPeriod e ON e.Id = d.PatrolPathPeriodId
                                INNER JOIN dbo.ExpandExamItem f ON f.Id = a.ExpandExamItemId
                                INNER JOIN dbo.EquipmentExamItem g ON g.Id = f.EquipmentExamItemId
                                INNER JOIN dbo.PatrolPlace h ON h.Id = g.PatrolPlaceId
                                INNER JOIN dbo.PatrolScope i ON i.Id = h.PatrolScopeId
                                INNER JOIN dbo.Person j ON j.Id = a.PersonId
                                INNER JOIN dbo.Equipment k ON k.Id = g.EquipmentId
                                LEFT JOIN dbo.ManagerApprovalDetail l ON l.ExpandExamItemId = a.ExpandExamItemId
                                LEFT JOIN dbo.ManagerApproval m ON m.[Guid] = l.ManagerApprovalGuid AND m.[Status] = 'Submit'
                                LEFT JOIN dbo.Person n ON n.Id = m.PersonId
                                WHERE e.Cycle IN ('每日', '每日多班別', '每週', '每月')
                                AND b.[Status] = 'N' AND e.[Status] = 'N' AND g.[Status] = 'N' AND h.[Status] = 'N' AND i.[Status] = 'N'
                                AND LEFT(CONVERT(VARCHAR(8), a.UpdateTime, 112), 6) = '{monthRange}'
                                AND a.IsAbnormal='Y'
                                ORDER BY a.UpdateTime DESC
                                ";

                result = conn.Query<SituationDashboardData8DataModel>(strSql).ToList();

            }

            await Task.Yield();
            return result;
        }
        #endregion
    }
}
