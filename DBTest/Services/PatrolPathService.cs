using Dapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PatrolPathService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<PatrolPathService> logger;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public DepartmentService DepartmentService { get; }

        public PatrolPathService(InspectionDBContext context, ILogger<PatrolPathService> logger, AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public Task<IQueryable<PatrolPath>> GetAsync()
        {
            return Task.FromResult(context.PatrolPath.AsNoTracking().AsQueryable());
        }

        public async Task<PatrolPath> GetAsync(int id)
        {
            PatrolPath item = await context.PatrolPath.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(PatrolPath paraObject)
        {
            await context.PatrolPath.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolPath> UpdateAsync(PatrolPath paraObject)
        {
            PatrolPath item = await context.PatrolPath
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolPath>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }
        public async Task<PatrolPath> DeleteAsync(PatrolPath paraObject)
        {
            PatrolPath item = await context.PatrolPath.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolPath.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(PatrolPath paraObject)
        {
            await Task.Delay(100);
            PatrolPath curritem = await context.PatrolPath
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolPath>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(PatrolPath paraObject)
        {
            await Task.Delay(100);
            PatrolPath curritem = await context.PatrolPath
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolPath>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<PatrolPath>> GetPatrolPathAllAsync()
        {
            UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();

            var loginDepts = await DepartmentService.GetLoginDeptsAsync(UserId, Account);

            List<PatrolPath> listPath = new List<PatrolPath>();
            try
            {
                listPath = await (
                    from a in context.PatrolPath
                    // and a.[DepartmentId] in (1,2)
                    where ((loginDepts == null || loginDepts.Count()<=0)
                        || loginDepts.Contains(a.DepartmentId))
                      // and EXISTS (SELECT 1 FROM PatrolPathPeriod b WHERE a.Id = b.PatrolPathId)
                      && a.PatrolPathPeriod.Any()
                    select new PatrolPath
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Status = a.Status,
                        InspectByOrder = a.InspectByOrder,
                        InspectByPda = a.InspectByPda,
                        TurnOnGps = a.TurnOnGps,
                        ShowRemark = a.ShowRemark,
                        RealTimeTrans = a.RealTimeTrans,
                        DepartmentId = a.DepartmentId

                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return listPath;
        }
        /// <summary>
        /// 取得路線by課別
        /// </summary>
        /// <returns></returns>
        public async Task<List<PatrolPath>> GetPatrolPathIdByDepartmentId(long departmentId)
        {
            List<PatrolPath> listPath = new List<PatrolPath>();
            if (departmentId != 0)
            {
                // 改成可以查出含底下部門的資料
                var subDepts = await DepartmentService.GetSubDepts(departmentId);
                subDepts.Add(departmentId); // 把自己加進去

                listPath = context.PatrolPath
                  .AsNoTracking()
                  .Where(x => subDepts.Contains(x.DepartmentId))
                  .ToList();
            }
            else
            {
                // 撈出登入者部門(含子部門)
                UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
                (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();
                var loginDepts = await DepartmentService.GetLoginDeptsAsync(UserId, Account);

                listPath = context.PatrolPath
                    .AsNoTracking()
                    .Where(x => loginDepts == null || loginDepts.Contains(x.DepartmentId))
                    .ToList();
            }
            return listPath;
        }

        public async Task<List<PatrolPathAdapterModel>> GetByReportIdAsync(int FormReportId)
        {
            List<PatrolPathAdapterModel> returnPath = new List<PatrolPathAdapterModel>();

            try
            {
                returnPath = await (
                    from a in context.FormPath
                    join b in context.PatrolPath
                      on a.PatrolPathId equals b.Id
                    where a.FormReportId == FormReportId
                    select new PatrolPathAdapterModel
                    {
                        Id = b.Id,
                        Name = b.Name
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

            }

            return returnPath;
        }

        public async Task<List<PatrolPath>> GetPathListByGroupAsync(int groupId)
        {

            var pathList = await
                (
                    from a in context.PatrolGroup
                    join b in context.PatrolGroupNpath
                      on a.Id equals b.GroupId
                    join c in context.PatrolPath
                      on b.PatrolPathId equals c.Id
                    join d in context.Department
                      on c.DepartmentId equals d.Id
                    where a.Id == groupId 
                      && a.Status == MagicHelper.StatusNoCode
                      && c.Status == MagicHelper.StatusNoCode
                      && d.DepartmentName == "中央監控"
                    select new PatrolPath
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Distinct().ToListAsync();

            pathList = pathList.OrderBy(x => x.Name).ToList();

            return pathList;
        }

        public async Task<PatrolPath> GetPathName(int PathId)
        {
            var result = await context.PatrolPath
                .FirstOrDefaultAsync(x => x.Id == PathId);

            return result;


        }
    }
}
