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
using Dapper;
using Microsoft.AspNetCore.Components.Authorization;

namespace InspectionBlazor.Services
{
    public class InspectionRecordStatisticsService
    {

        private readonly InspectionDBContext context;
        private readonly ILogger<InspectionRecordStatisticsService> logger;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }

        public DepartmentService DepartmentService { get; }

        public InspectionRecordStatisticsService(InspectionDBContext context, ILogger<InspectionRecordStatisticsService> logger, AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public async Task<IQueryable<InspectionRecordStatisticsDataModel>> GetCheckinAbnormalAsync(InspectionQueryConditionDataModel conditionDataModel)
        {
            DateTime dtToday = DateTime.Now;
            //抓取異常資料
            var dbResult = context.Checkin
               .AsNoTracking()
               .Include(x => x.ExpandPlace)
               .ThenInclude(x => x.PatrolPlace)
               .ThenInclude(x => x.PatrolScope)
               .Include(x => x.PatrolPath)
               .ThenInclude(x => x.PatrolPathPeriod)
               .Include(x => x.Expand)
               .Where(x =>
               x.Expand.BeginTime >= conditionDataModel.Begin &&
               x.Expand.EndTime <= conditionDataModel.End )
               .AsQueryable();

            //排除時間未到日期
            var filter = dbResult.Where(x => x.Expand.EndTime >= DateTime.Now && x.IsCompleted == MagicHelper.StatusNoCode).ToList();

            long[] 排除的陣列 = new long[filter.Count];

            for (int i = 0; i < filter.Count; i++)
            {
                排除的陣列[i] = filter[i].ExpandPlaceId;
            }

             dbResult = dbResult.Where(x => !排除的陣列.Contains(x.ExpandPlaceId)).AsQueryable();

            IQueryable<InspectionRecordStatisticsDataModel> result = null;

            switch (conditionDataModel.AbnormalType)
            {
                case "日期排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).AsQueryable();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandPlace.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                        .GroupBy(x => x.Expand.BeginTime)
                                        .Select(x => new InspectionRecordStatisticsDataModel
                                        {
                                            AbnormalCount = x.Count(),
                                            AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                        }).AsQueryable();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).AsQueryable();
                    }
                    break;
                case "路線排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).AsQueryable();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandPlace.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                         .GroupBy(x => x.PatrolPath.Name)
                                         .Select(x => new InspectionRecordStatisticsDataModel
                                         {
                                             AbnormalCount = x.Count(),
                                             AbnormalName = x.Key
                                         }).AsQueryable();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).AsQueryable();
                    }
                    break;
                case "範圍排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.ExpandPlace.PatrolPlace.PatrolScope.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).AsQueryable();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandPlace.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                         .GroupBy(x => x.ExpandPlace.PatrolPlace.PatrolScope.Name)
                                         .Select(x => new InspectionRecordStatisticsDataModel
                                         {
                                             AbnormalCount = x.Count(),
                                             AbnormalName = x.Key
                                         }).AsQueryable();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.ExpandPlace.PatrolPlace.PatrolScope.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).AsQueryable();
                    }
                    break;
                default:
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).AsQueryable();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandPlace.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                        .GroupBy(x => x.Expand.BeginTime)
                                        .Select(x => new InspectionRecordStatisticsDataModel
                                        {
                                            AbnormalCount = x.Count(),
                                            AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                        }).AsQueryable();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).AsQueryable();
                    }

                    break;
            }

            return result;
        }

        public async Task<List<InspectionRecordStatisticsDataModel>> GetOutComeByConditionAsync(InspectionQueryConditionDataModel conditionDataModel)
        {
            // 撈出登入者部門(含子部門)
            UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();
            var loginDepts = await DepartmentService.GetLoginDeptsAsync(UserId, Account);

            var dbResult = await context.OutCome
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
                          .Where(x =>
                          x.UpdateTime >= conditionDataModel.Begin &&
                          x.UpdateTime <= conditionDataModel.End && 
                          x.IsAbnormal == MagicHelper.StatusYesCode &&
                          // 管理員可看到全部 => null
                          // 非管理員只能看到自己部門資料(含底下部門)
                          (loginDepts == null || loginDepts.Contains(x.PatrolPath.DepartmentId)))
                          .ToListAsync();

            //排除時間未到日期
            var filter = dbResult.Where(x => x.Expand.EndTime >= DateTime.Now && x.IsCompleted == MagicHelper.StatusNoCode).ToList();

            long[] 排除的陣列 = new long[filter.Count];

            for (int i = 0; i < filter.Count; i++)
            {
                排除的陣列[i] = filter[i].ExpandExamItemId;
            }

            dbResult = dbResult.Where(x => !排除的陣列.Contains(x.ExpandExamItemId)).ToList();
            List<InspectionRecordStatisticsDataModel> result = null;

            switch (conditionDataModel.AbnormalType)
            {
                case "日期排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).ToList();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                        .GroupBy(x => x.Expand.BeginTime)
                                        .Select(x => new InspectionRecordStatisticsDataModel
                                        {
                                            AbnormalCount = x.Count(),
                                            AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                        }).ToList();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).ToList();
                    }
                    break;
                case "路線排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       { 
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).ToList();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                         .GroupBy(x => x.PatrolPath.Name)
                                         .Select(x => new InspectionRecordStatisticsDataModel
                                         {
                                             AbnormalCount = x.Count(),
                                             AbnormalName = x.Key
                                         }).ToList();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).ToList();
                    }
                    break;
                case "範圍排序":
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScope.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).ToList();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                         .GroupBy(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScope.Name)
                                         .Select(x => new InspectionRecordStatisticsDataModel
                                         {
                                             AbnormalCount = x.Count(),
                                             AbnormalName = x.Key
                                         }).ToList();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScope.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).ToList();
                    }
                    break;
                default:
                    //路線搜尋
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        result = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath)
                                       .GroupBy(x => x.PatrolPath.Name)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key
                                       }).ToList();
                    }
                    //範圍搜尋
                    if (conditionDataModel.Scope != 0)
                    {
                        result = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope)
                                        .GroupBy(x => x.Expand.BeginTime)
                                        .Select(x => new InspectionRecordStatisticsDataModel
                                        {
                                            AbnormalCount = x.Count(),
                                            AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                        }).ToList();
                    }
                    //路線跟範圍都沒有預設
                    if (conditionDataModel.PatrolPath == 0 && conditionDataModel.Scope == 0)
                    {
                        result = dbResult
                                       .GroupBy(x => x.Expand.BeginTime)
                                       .Select(x => new InspectionRecordStatisticsDataModel
                                       {
                                           AbnormalCount = x.Count(),
                                           AbnormalName = x.Key.ToString("yyyy-MM-dd")
                                       }).ToList();
                    }

                    break;
            }
            return result;
        }
    }
}
