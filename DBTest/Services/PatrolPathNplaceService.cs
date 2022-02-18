using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InspectionBlazor.Extensions;
using InspectionBlazor.DataModels;

namespace InspectionBlazor.Services
{
    public class PatrolPathNplaceService
    {
        private readonly InspectionDBContext context;

        public PatrolPathNplaceService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PatrolPathNplace>> GetAsync()
        {
            return Task.FromResult(context.PatrolPathNplace
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<PatrolPathNplace>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.PatrolPathNplace
                .Include(x => x.PatrolPath)
                .Include(x=>x.PatrolPlace)
                .Where(x => x.PatrolPathId == paraObj)
                .AsNoTracking().AsQueryable());
        }

        public async Task<List<PathPlace>> GetPlaceListByPathIds(List<PathScope> pathScopeList)
        {
            var resultList = new List<PathPlace>();

            foreach (var item in pathScopeList)
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
                       && e.Id == item.PathId
                       && i.Id == item.ScopeId
                    select new PathPlace
                    {
                        PathIdPlaceId = e.Id + "-" + i.Id + "-" + c.Id,
                        PathId = e.Id,
                        PathName = e.Name,
                        ScopeId = i.Id,
                        ScopeName = i.Name,
                        PlaceId = c.Id,
                        PlaceName = c.Name,
                        PathScopeName = e.Name + "-" + i.Name,
                        PathPlaceName = e.Name + "-" + i.Name + "-" + c.Name
                    }).Distinct().ToListAsync();

                resultList.AddRange(placeList);

            }

            resultList = resultList.OrderBy(x => x.PathPlaceName).ToList();

            return resultList;
        }

        public async Task<PatrolPathNplace> GetAsync(int id)
        {
            PatrolPathNplace item = await context.PatrolPathNplace.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(PatrolPathNplace paraObject)
        {
            await context.PatrolPathNplace.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolPathNplace> UpdateAsync(PatrolPathNplace paraObject)
        {
            PatrolPathNplace item = await context.PatrolPathNplace
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolPathNplace>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolPathNplace> DeleteAsync(PatrolPathNplace paraObject)
        {
            PatrolPathNplace item = await context.PatrolPathNplace.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolPathNplace.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
    }
}
