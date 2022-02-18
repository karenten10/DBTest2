using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace InspectionBlazor.Services
{
    public class DaliyAbnormalService
    {

        private readonly InspectionDBContext context;
        private readonly ILogger<DaliyAbnormalService> logger;

        public AuthenticationStateProvider AuthenticationStateProvider { get; }

        public DepartmentService DepartmentService { get; }

        public DaliyAbnormalService(InspectionDBContext context, ILogger<DaliyAbnormalService> logger, AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public async Task<IQueryable<AbnormalDataModel>> GetOutComeCountByConditionAsync(InspectionQueryConditionDataModel conditionDataModel)
        {
            // 撈出登入者部門(含子部門)
            UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();
            var loginDepts = await DepartmentService.GetLoginDeptsAsync(UserId, Account);

            DateTime dtToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            var dbResult = context.OutCome
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
                            .AsQueryable();

            //排除時間未到日期
            var filter = dbResult.Where(x => x.Expand.EndTime >= DateTime.Now && x.IsCompleted == MagicHelper.StatusNoCode).ToList();

            long[] 排除的陣列 = new long[filter.Count];

            for (int i = 0; i < filter.Count; i++)
            {
                排除的陣列[i] = filter[i].ExpandExamItemId;
            }

            dbResult = dbResult.Where(x => !排除的陣列.Contains(x.ExpandExamItemId)).AsQueryable();


            IQueryable<AbnormalDataModel> result = null;
            
            // 部門
            if (conditionDataModel.DepartmentId != null && conditionDataModel.DepartmentId != 0)
            {
                // 改成可以查出含底下部門的資料
                var subDepts = await DepartmentService.GetSubDepts(conditionDataModel.DepartmentId.Value);
                subDepts.Add(conditionDataModel.DepartmentId.Value); // 把自己加進去
                dbResult = dbResult.Where(x => subDepts.Contains(x.PatrolPath.DepartmentId)).AsQueryable();
            }

            if (conditionDataModel.PatrolPath != null && conditionDataModel.PatrolPath != 0)
            {
                dbResult = dbResult.Where(x => x.PatrolPathId == conditionDataModel.PatrolPath).AsQueryable();
            }
            //範圍搜尋
            if (conditionDataModel.Scope != null && conditionDataModel.Scope != 0)
            {
                dbResult = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.PatrolPlace.PatrolScopeId == conditionDataModel.Scope).AsQueryable();
            }
            //設備搜尋
            if (conditionDataModel.Equipment != null && conditionDataModel.Equipment != 0)
            {
                dbResult = dbResult.Where(x => x.ExpandExamItem.EquipmentExamItem.EquipmentId == conditionDataModel.Equipment).AsQueryable();
            }
            
            result = dbResult
                .GroupBy(x => x.Expand.EndTime)
                .Select(x => new AbnormalDataModel
                {
                    AbnormalCount = x.Count(),
                    AbnormalName = x.Key.ToString("yyyy/MM/dd")
                }).AsQueryable();
            return result;
        }

        public async Task<IQueryable<OutCome>> GetOutComeByConditionForDashBoardAsync(InspectionQueryConditionDataModel conditionDataModel, List<long?> loginDepts)
        {
            //只顯示異常
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
                .Where(x =>
                x.UpdateTime >= conditionDataModel.Begin &&
                x.UpdateTime <= conditionDataModel.End &&
                x.IsAbnormal == MagicHelper.StatusYesCode &&
                (loginDepts ==null || (loginDepts!=null && loginDepts.Contains(x.PatrolPath.DepartmentId))))
                .AsQueryable();

            return result;
        }
    }
}
