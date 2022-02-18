using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PatrolGroupService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<PatrolPathService> logger;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public DepartmentService DepartmentService { get; }

        public PatrolGroupService(InspectionDBContext context, ILogger<PatrolPathService> logger
            , AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public async Task<List<PatrolGroup>> GetAsync()
        {
            return await context.PatrolGroup.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(PatrolGroup paraObject)
        {
            await context.PatrolGroup.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolGroup> UpdateAsync(PatrolGroup paraObject)
        {
            PatrolGroup item = await context.PatrolGroup
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolGroup>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolGroup> DeleteAsync(PatrolGroup paraObject)
        {
            PatrolGroup item = await context.PatrolGroup.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolGroup.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(PatrolGroup paraObject)
        {
            await Task.Delay(100);
            PatrolGroup curritem = await context.PatrolGroup
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolGroup>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(PatrolGroup paraObject)
        {
            await Task.Delay(100);
            PatrolGroup curritem = await context.PatrolGroup
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolGroup>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<PatrolGroup>> GetPatrolGroupByPathIdAsync(int pathId)
        {
            var result = await context.PatrolGroupNpath
                .Where(x => x.PatrolPathId == pathId)
                .Select(x => new PatrolGroup { Id = x.GroupId, GroupName = x.Group.GroupName })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<PatrolGroup>> GetPatrolGroupByPahtNameAsync(string name)
        {
            List<PatrolGroup> result = new List<PatrolGroup>();
            if (!string.IsNullOrEmpty(name))
            {
                if (name.ToLower() != "all")
                {
                    result = await context.PatrolGroupNpath
                        .Include(x => x.PatrolPath)
                        .Where(x => x.PatrolPath.Name == name)
                        .Select(x => new PatrolGroup { Id = x.GroupId, GroupName = x.Group.GroupName })
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {

                    result = await context.PatrolGroupNpath
                        .Include(x => x.PatrolPath)
                        .Select(x => new PatrolGroup { Id = x.GroupId, GroupName = x.Group.GroupName })
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            
            return result;
        }

        public async Task<List<PatrolGroup>> GetEnabledAllAsync()
        {

            var groupList = await
                (
                    from a in context.PatrolGroup
                    join b in context.PatrolGroupNpath
                      on a.Id equals b.GroupId
                    join c in context.PatrolPath
                      on b.PatrolPathId equals c.Id
                    join d in context.Department
                      on c.DepartmentId equals d.Id
                    where a.Status == "N"
                      && c.Status == "N"
                      && d.DepartmentName == "中央監控"
                    select new PatrolGroup
                    {
                        Id = a.Id,
                        GroupName = a.GroupName
                    }).Distinct().ToListAsync();

            return groupList;
        }
    }
}
