using Dapper;
using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using InspectionBlazor.AdapterModels;

namespace InspectionBlazor.Services
{
    public class InspectionRecordService
    {

        private readonly InspectionDBContext context;
        private readonly ILogger<InspectionRecordService> logger;
        private readonly DepartmentService departmentService;

        public InspectionRecordService(InspectionDBContext context, ILogger<InspectionRecordService> logger, DepartmentService DepartmentService)
        {
            this.context = context;
            this.logger = logger;
            departmentService = DepartmentService;
        }

        public async Task<IQueryable<Checkin>> GetCheckinByConditionAsync(InspectionQueryConditionDataModel conditionDataModel)
        {
            var result = context.Checkin
                .AsNoTracking()
                .Include(x => x.ExpandPlace)
                .ThenInclude(x => x.PatrolPlace)
                .ThenInclude(x => x.PatrolScope)
                .Include(x => x.PatrolPath)
                .ThenInclude(x => x.PatrolPathPeriod)
                .Include(x => x.Expand)
                .Where(x =>
                x.Expand.BeginTime >= conditionDataModel.Begin &&
                x.Expand.EndTime <= conditionDataModel.End)
                .AsQueryable();

            if (conditionDataModel.PatrolPath != 0)
            {
                result = result.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath);
            }

            //排除時間未到日期
            var filter = result.Where(x => x.Expand.EndTime >= DateTime.Now && x.IsCompleted == MagicHelper.StatusNoCode).ToList();

            long[] 排除的陣列 = new long[filter.Count];

            for (int i = 0; i < filter.Count; i++)
            {
                排除的陣列[i] = filter[i].ExpandPlaceId;
            }

            var filterResult = result.Where(x => !排除的陣列.Contains(x.ExpandPlaceId)).AsQueryable();

            return filterResult;
        }

        public async Task<IQueryable<OutCome>> GetOutComeByConditionAsync(InspectionQueryConditionDataModel conditionDataModel, List<long?> loginDepts)
        {
            try
            {
                if (conditionDataModel.Person == null || conditionDataModel.Person.Value < 0)
                {
                    conditionDataModel.Person = 0;
                }

                var result = context.OutCome
                              .AsNoTracking()
                              .Include(x => x.ExpandExamItem)
                              .ThenInclude(x => x.EquipmentExamItem)
                              .ThenInclude(x => x.PatrolPlace)
                              .ThenInclude(x => x.PatrolScope)
                              .Include(x => x.ExpandExamItem)
                              .ThenInclude(x => x.EquipmentExamItem)
                              .ThenInclude(x => x.Equipment)
                              .Include(x => x.PatrolPath)
                              .ThenInclude(x => x.PatrolPathPeriod)
                              .Include(x => x.Expand)
                              .ThenInclude(x => x.PatrolPathPeriod)
                              .Include(x => x.PatrolPath)
                              .ThenInclude(x => x.Department)
                              .Where(x => (loginDepts == null || loginDepts.Contains(x.PatrolPath.DepartmentId)))
                              .AsQueryable();

                if (conditionDataModel.SearchType == InspectionRecordHelper.SearchType.All || conditionDataModel.SearchType == InspectionRecordHelper.SearchType.Abnormal)
                {
                    result = result.Where(x =>
                        x.UpdateTime >= conditionDataModel.Begin &&
                        x.UpdateTime <= conditionDataModel.End &&
                        (x.Expand.PatrolPathPeriod.Status == "N"
                            || (x.Expand.PatrolPathPeriod.Status == "Y" && x.IsCompleted == "Y")) &&
                        ((conditionDataModel.SearchType == InspectionRecordHelper.SearchType.Abnormal && x.IsAbnormal == MagicHelper.StatusYesCode)
                            || conditionDataModel.SearchType == InspectionRecordHelper.SearchType.All));
                } 
                else if (conditionDataModel.SearchType == InspectionRecordHelper.SearchType.Due)
                {
                    // 逾期
                    result = result.Where(x =>
                        x.ExpandExamItem.Expand.EndTime < DateTime.Now // 已經逾期
                        && x.IsCompleted == "N" // 且未完成
                        && x.ExpandExamItem.Expand.BeginTime >= conditionDataModel.Begin 
                        && x.ExpandExamItem.Expand.EndTime <= conditionDataModel.End 
                        && x.Expand.PatrolPathPeriod.Status == "N"
                        );
                }

                //課別查詢
                if (conditionDataModel.DepartmentId != null && conditionDataModel.DepartmentId != 0)
                {
                    // 改成可以查出含底下部門的資料
                    var subDepts = await departmentService.GetSubDepts(conditionDataModel.DepartmentId.Value);
                    subDepts.Add(conditionDataModel.DepartmentId.Value); // 把自己加進去
                    result = result.Where(x => subDepts.Contains(x.PatrolPath.DepartmentId));
                }

                //人員查詢
                if (conditionDataModel.Person != null && conditionDataModel.Person != 0)
                {
                    result = result.Where(x => x.PersonId == conditionDataModel.Person);
                }

                //路線查詢
                if (conditionDataModel.PatrolPath != null && conditionDataModel.PatrolPath != 0)
                {
                    result = result.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath);
                }
                //查詢範圍
                if (conditionDataModel.Scope != null && conditionDataModel.Scope != 0)
                {
                    result = result.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope);
                }
                //設備查詢
                if (conditionDataModel.Equipment != null && conditionDataModel.Equipment != 0)
                {
                    result = result.Where(x => x.ExpandExamItem.EquipmentExamItem.Equipment.Id == conditionDataModel.Equipment);
                }

                IQueryable<OutCome> returnResult = new List<OutCome>().AsQueryable();
                if (conditionDataModel.SearchType == InspectionRecordHelper.SearchType.Due)
                {
                    returnResult = result;
                }
                else // if (conditionDataModel.SearchType == InspectionRecordHelper.SearchType.All || conditionDataModel.SearchType == InspectionRecordHelper.SearchType.Abnormal)
                {
                    //排除時間未到日期
                    var filter = result.Where(x => x.Expand.EndTime >= DateTime.Now && x.IsCompleted == MagicHelper.StatusNoCode).ToList();

                    long[] 排除的陣列 = new long[filter.Count];

                    for (int i = 0; i < filter.Count; i++)
                    {
                        排除的陣列[i] = filter[i].ExpandExamItemId;
                    }

                    var filterResult = result.Where(x => !排除的陣列.Contains(x.ExpandExamItemId)).AsQueryable();

                    returnResult = filterResult;
                }

                return returnResult;
            }
            catch (Exception ex)
            {
                logger.LogError($"GetOutComeByConditionAsync error = {ex.Message}");
                return null;
            }
        }

        public Task<IQueryable<InspectionRecordForDashboardAdapterModel>> GetInspectionRecordForDashboardAsync(List<long?> loginDepts)
        {
            List<InspectionRecordForDashboardAdapterModel> returnList = new List<InspectionRecordForDashboardAdapterModel>();

            try
            {
                List<InspectionRecordForDashboardAdapterModel> inspectionRecordForDashboardAdapterModels = new List<InspectionRecordForDashboardAdapterModel>();

                using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
                {

                    conn.Open();
                    string strSql = "";
                    if (loginDepts != null)
                    {
                        strSql = $@"
                                SELECT distinct a.Name AS PathName,
                                   c.Name AS PeriodName,
                                   e.Name AS UserName,
                                   substring(convert(varchar, d.UpdateTime, 120), 0, 17) AS 紀錄時間
                            FROM dbo.PatrolPath a
                                INNER JOIN dbo.Expand b
                                    ON a.Id = b.PatrolPathId
                                INNER JOIN dbo.PatrolPathPeriod c
                                    ON b.PatrolPathPeriodId = c.Id
                                INNER JOIN dbo.OutCome d
                                    ON d.ExpandId = b.Id
                                LEFT JOIN dbo.Person e
                                    ON d.PersonId = e.Id
                            WHERE d.IsCompleted = 'Y'
                                  AND d.UpdateTime
                                
                                  BETWEEN CONVERT(DATETIME, CONVERT(VARCHAR(10), GETDATE(), 120)) AND DATEADD(
                                                                                                                 SS,
                                                                                                                 -1,
                                                                                                                 DATEADD(
                                                                                                                            DD,
                                                                                                                            1,
                                                                                                                            CONVERT(
                                                                                                                                       DATETIME,
                                                                                                                                       CONVERT(
                                                                                                                                                  VARCHAR(10),
                                                                                                                                                  GETDATE(),
                                                                                                                                                  120
                                                                                                                                              )
                                                                                                                                   )
                                                                                                                        )
                                                                                                             )
                            AND a.DepartmentId IN(@depIds)
                            ORDER BY 紀錄時間 DESC, UserName asc, PeriodName asc;";

                        inspectionRecordForDashboardAdapterModels = conn.Query<InspectionRecordForDashboardAdapterModel>(strSql, new
                        {
                            depIds = string.Join(",", loginDepts)
                        }).ToList();
                    }
                    else
                    {
                        strSql = $@"
                                SELECT distinct a.Name AS PathName,
                                   c.Name AS PeriodName,
                                   e.Name AS UserName,
                                   substring(convert(varchar, d.UpdateTime, 120), 0, 17) AS 紀錄時間
                            FROM dbo.PatrolPath a
                                INNER JOIN dbo.Expand b
                                    ON a.Id = b.PatrolPathId
                                INNER JOIN dbo.PatrolPathPeriod c
                                    ON b.PatrolPathPeriodId = c.Id
                                INNER JOIN dbo.OutCome d
                                    ON d.ExpandId = b.Id
                                LEFT JOIN dbo.Person e
                                    ON d.PersonId = e.Id
                            WHERE d.IsCompleted = 'Y'
                                  AND d.UpdateTime
                                  BETWEEN CONVERT(DATETIME, CONVERT(VARCHAR(10), GETDATE(), 120)) AND DATEADD(
                                                                                                                 SS,
                                                                                                                 -1,
                                                                                                                 DATEADD(
                                                                                                                            DD,
                                                                                                                            1,
                                                                                                                            CONVERT(
                                                                                                                                       DATETIME,
                                                                                                                                       CONVERT(
                                                                                                                                                  VARCHAR(10),
                                                                                                                                                  GETDATE(),
                                                                                                                                                  120
                                                                                                                                              )
                                                                                                                                   )
                                                                                                                        )
                                                                                                             )
                            ORDER BY 紀錄時間 DESC, UserName asc, PeriodName asc;

                                ";
                        inspectionRecordForDashboardAdapterModels = conn.Query<InspectionRecordForDashboardAdapterModel>(strSql).ToList();
                    }


                    if (inspectionRecordForDashboardAdapterModels.Any())
                    {
                        returnList = inspectionRecordForDashboardAdapterModels;
                    }
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

            return Task.FromResult(returnList.AsQueryable());
        }

        public Task<IQueryable<InspectionRecordIndexAdapterModel>> GetInspectionRecordIndexAsync(InspectionRecordIndexConditionDataModel conditionDataModel, List<long?> loginDepts)
        {
            List<InspectionRecordIndexAdapterModel> returnList = new List<InspectionRecordIndexAdapterModel>();

            try
            {
                List<InspectionRecordIndexAdapterModel> inspectionRecordIndexAdapterModels = new List<InspectionRecordIndexAdapterModel>();

                using (SqlConnection conn = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
                {

                    conn.Open();

                    string depSql = "";

                    if (loginDepts != null)
                    {
                        depSql = " AND a.DepartmentId IN(@depIds) ";
                    }

                    string timeSql = "";
                    string begin = "";
                    string end = "";
                    if (conditionDataModel.Begin != null)
                    {
                        timeSql = " and d.UpdateTime >= @begin ";
                        begin = conditionDataModel.Begin.ToString("yyyy-MM-dd") + "";
                    }

                    if (conditionDataModel.End != null)
                    {
                        timeSql += " and d.UpdateTime < @end ";
                        end = conditionDataModel.End.AddDays(1).ToString("yyyy-MM-dd");
                    }

                    string strSql = $@"
                                SELECT distinct a.Name AS PathName,
                                   c.Name AS PeriodName,
                                   e.Name AS UserName,
                                   substring(convert(varchar, d.UpdateTime, 120), 0, 17) AS 紀錄時間
                            FROM dbo.PatrolPath a
                                INNER JOIN dbo.Expand b
                                    ON a.Id = b.PatrolPathId
                                INNER JOIN dbo.PatrolPathPeriod c
                                    ON b.PatrolPathPeriodId = c.Id
                                INNER JOIN dbo.OutCome d
                                    ON d.ExpandId = b.Id
                                LEFT JOIN dbo.Person e
                                    ON d.PersonId = e.Id
                            WHERE d.IsCompleted = 'Y'
                              {depSql}
                              {timeSql}
                            ORDER BY 紀錄時間 DESC, UserName asc, PeriodName asc;";

                    inspectionRecordIndexAdapterModels = conn.Query<InspectionRecordIndexAdapterModel>(strSql, new
                    {
                        depIds = (loginDepts != null ? string.Join(",", loginDepts) : null),
                        begin = begin,
                        end = end
                    }).ToList();

                    if (inspectionRecordIndexAdapterModels.Any())
                    {
                        returnList = inspectionRecordIndexAdapterModels;
                    }
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

            return Task.FromResult(returnList.AsQueryable());
        }
    }
}
