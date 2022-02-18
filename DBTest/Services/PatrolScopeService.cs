using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PatrolScopeService
    {
        private readonly InspectionDBContext context;

        public PatrolScopeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<List<PatrolScope>> GetAsync()
        {
            return await context.PatrolScope.AsNoTracking().ToListAsync();
        }

        public string GetNameByPlaceId(int id)
        {
            var item = context.PatrolPlace
                .Include(x => x.PatrolScope)
                .FirstOrDefault(x => x.Id == id);
            string Name = string.Empty;
            if (item != null)
            {
                Name = item.PatrolScope.Name;
            }
            return Name;
        }

        public async Task AddAsync(PatrolScope paraObject)
        {
            await context.PatrolScope.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolScope> UpdateAsync(PatrolScope paraObject)
        {
            PatrolScope item = await context.PatrolScope
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolScope>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolScope> DeleteAsync(PatrolScope paraObject)
        {
            PatrolScope item = await context.PatrolScope.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolScope.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(PatrolScope paraObject)
        {
            await Task.Delay(100);
            PatrolScope curritem = await context.PatrolScope
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolScope>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(PatrolScope paraObject)
        {
            await Task.Delay(100);
            PatrolScope curritem = await context.PatrolScope
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolScope>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<PatrolScope>> GetPatrolScopeByPathIdAsync(int pathId)
        {
            var result = await context.PatrolPathScope
                .Where(x => x.PatrolPathId == pathId)
                .Select(x => new PatrolScope { Id = x.PatrolScopeId, Name = x.PatrolScope.Name })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<PatrolScope>> GetPatrolScopeByPathIdAsync(int? pathId)
        {
            var result = await context.PatrolPathScope
                .Where(x => pathId == null || x.PatrolPathId == pathId)
                .Select(x => new PatrolScope { Id = x.PatrolScopeId, Name = x.PatrolScope.Name })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<PatrolScope>> GetPatrolScopeByPahtNameAsync(string name)
        {
            List<PatrolScope> result = new List<PatrolScope>();
            if (!string.IsNullOrEmpty(name))
            {
                if (name.ToLower() != "all")
                {
                    result = await context.PatrolPathScope
                        .Include(x => x.PatrolPath)
                        .Where(x => x.PatrolPath.Name == name)
                        .Select(x => new PatrolScope { Id = x.PatrolScopeId, Name = x.PatrolScope.Name })
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {
                    result = await context.PatrolPathScope
                        .Include(x => x.PatrolPath)
                        .Select(x => new PatrolScope { Id = x.PatrolScopeId, Name = x.PatrolScope.Name })
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            
            return result;
        }

        public async Task<List<PathScope>> GetScopeListByPathIds(int[] pathIds)
        {
            var placeList = await
            (
                from a in context.Equipment
                join b in context.EquipmentExamItem
                  on a.Id equals b.EquipmentId
                join h in context.PatrolPathPeriodNexamItem
                  on b.Id equals h.EquipmentExamItemId
                join g in context.PatrolPathPeriod
                  on h.PatrolPathPeriodId equals g.Id
                join e in context.PatrolPath
                  on g.PatrolPathId equals e.Id
                join f in context.Department
                  on e.DepartmentId equals f.Id
                join c in context.PatrolPlace
                  on b.PatrolPlaceId equals c.Id
                join i in context.PatrolScope
                  on c.PatrolScopeId equals i.Id
                where a.Status == "N"
                   // && b.Status == "N" // 不考慮檢查項目停用
                   && g.Status == "N"
                   && e.Status == "N"
                   && c.Status == "N"
                   && f.DepartmentName == "中央監控"
                   && pathIds.Contains(e.Id)
                select new PathScope
                {
                    PathIdScopeId = e.Id + "-" + i.Id,
                    PathId = e.Id,
                    PathName = e.Name,
                    ScopeId = i.Id,
                    ScopeName = i.Name,
                    PathScopeName = e.Name + "-" + i.Name
                }).Distinct().ToListAsync();

            placeList = placeList.OrderBy(x => x.PathScopeName).ToList();

            return placeList;
        }
    }
}
