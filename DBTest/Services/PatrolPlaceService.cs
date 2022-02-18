using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;

namespace InspectionBlazor.Services
{
    public class PatrolPlaceService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<PatrolPlaceService> logger;

        public PatrolPlaceService(InspectionDBContext context, ILogger<PatrolPlaceService> logger
            )
        {
            this.context = context;
            this.logger = logger;
        }

        public Task<IQueryable<PatrolPlace>> GetAsync()
        {
            return Task.FromResult(context.PatrolPlace.Include(x => x.PatrolScope).AsNoTracking().AsQueryable());
        }

        public Task<IQueryable<PatrolPlace>> GetPatrolPlaceALL()
        {
            return Task.FromResult(context.PatrolPlace
             .AsNoTracking()
             .Where(x => x.Status == "N")
             .AsQueryable());
        }

        public async Task<IQueryable<PatrolPlace>> GetPlaceByPeriod(int periodId)
        {
            var places = await context.PatrolPathPeriodNplace
                .Where(x => x.PatrolPathPeriodId == periodId)
                .Select(x => new
                {
                    Id = x.PatroPlaceId
                })
                .ToListAsync();

            List<int> placeIds = new List<int>();
            foreach (var placeId in places)
            {
                placeIds.Add(placeId.Id);
            }

            var placeObjs = context.PatrolPlace.Where(x => placeIds.Contains(x.Id))
                .AsNoTracking().AsQueryable();

            return placeObjs;

        }

        public Task<IQueryable<PatrolPlace>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.PatrolPlace
                .Include(x => x.PatrolScope)
                .Where(x => x.PatrolScopeId == paraObj).AsNoTracking().AsQueryable());
        }

        public string GetNameById(int id)
        {
            var item = context.PatrolPlace.FirstOrDefault(x => x.Id == id);
            string Name = string.Empty;
            if (item != null)
            {
                Name = item.Name;
            }
            return Name;
        }

        /// <summary>由巡檢路線Id列出相關巡檢範圍內的所有巡檢點</summary>
        public async Task<IQueryable<PatrolPlace>> GetAllPlaceByPathAsync(int pathId)
        {
            //由Pathid 找 scopeid
            var patrolPaths = await context.PatrolPathScope
                .Where(x => x.PatrolPathId == pathId)
                .Select(x => new
                {
                    Id = x.PatrolScopeId,
                })
                .ToListAsync();
            // 將該 Path 找到 PatrolPathScope 紀錄之 PatrolScopeId，全部放到 陣列
            List<int> pathscope = new List<int>();
            foreach (var scope in patrolPaths)
            {
                pathscope.Add(scope.Id);
            }

            var placesLists = context.PatrolPlace
                .Where(x => pathscope.Contains(x.PatrolScopeId))
                .OrderBy(x => x.PatrolScopeId)
                .Include(x => x.PatrolScope)
                .ThenInclude(x => x.PatrolPathScope)
                .ThenInclude(x => x.PatrolPath)
                .AsNoTracking().AsQueryable();
            return placesLists;
        }

        /// <summary> 由巡檢路線Id列出 巡檢路線規劃 所對應的巡檢點(包含巡檢範圍)</summary>
        public async Task<IQueryable<PatrolPlace>> GetJoinedPlaceByPathAsync(int pathId)
        {
            //由Pathid 找 scopeid
            var patrolPaths = await context.PatrolPathScope
                .Where(x => x.PatrolPathId == pathId)
                .Select(x => new
                {
                    Id = x.PatrolScopeId,
                })
                .ToListAsync();
            // 將該 Path 找到 PatrolPathScope 紀錄之 PatrolScopeId，全部放到 陣列
            List<int> pathscope = new List<int>();
            foreach (var scope in patrolPaths)
            {
                pathscope.Add(scope.Id);
            }

            var placesLists = context.PatrolPlace
                .AsNoTracking()
                .Where(x => pathscope.Contains(x.PatrolScopeId))
                .Include(x => x.PatrolPathNplace)
                .Include(x => x.PatrolScope)
                .ThenInclude(x => x.PatrolPathScope)
                .OrderBy(x => x.PatrolScopeId)
                .ThenBy(x => x.Name)
                .AsQueryable();

            return placesLists;
        }

        /// <summary>巡檢路線規劃作業中,檢查巡檢點是否加入巡檢路線</summary>
        public async Task<bool> PlaceWetherExistInPathAsync(int pathId, int placeId)
        {
            PatrolPathNplace item = await context.PatrolPathNplace.AsNoTracking()
               .FirstOrDefaultAsync(x => x.PatrolPlaceId == placeId &&
               x.PatrolPathId == pathId);
            if (item == null)
                return false;
            else
                return true;
        }

        /// <summary>巡檢時段作業中檢查巡檢點是否加入時段中(路線-時段-巡檢點)</summary>
        public async Task<bool> PlaceWetherExistInPeriodAsync(int peroidId, int PlaceId)
        {
            PatrolPathPeriodNplace item = await context.PatrolPathPeriodNplace.AsNoTracking()
                .FirstOrDefaultAsync(x => x.PatroPlaceId == PlaceId &&
                x.PatrolPathPeriodId == peroidId);

            return item == null ? false : true;
        }

        /// <summary>巡檢路線規劃作業 將巡檢點加入PatrolPathNPlace</summary>
        public async Task AddPlaceToPathAsync(int PathId, int PlaceId)
        {
            PatrolPathNplace item = new PatrolPathNplace()
            {
                PatrolPlaceId = PlaceId,
                PatrolPathId = PathId
            };
            context.PatrolPathNplace.Add(item);
            await context.SaveChangesAsync();
        }

        /// <summary>巡檢路線規劃作業 將巡檢點從 PatrolPathNPlace 移除</summary>
        public async Task RemovePlaceToPathAsync(int PathId, int PlaceId)
        {
            var checkItem = await context.PatrolPathNplace.FirstOrDefaultAsync(x =>
               x.PatrolPathId == PathId && x.PatrolPlaceId == PlaceId);
            if (checkItem != null)
            {
                context.PatrolPathNplace.Remove(checkItem);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>將巡檢點加入PatrolPathPeriodNPlace(巡檢時段)中</summary>
        public async Task AddPlaceToPeriodAsync(int periodId, int placeId)
        {
            PatrolPathPeriodNplace item = new PatrolPathPeriodNplace()
            {
                PatroPlaceId = placeId,
                PatrolPathPeriodId = periodId
            };
            context.PatrolPathPeriodNplace.Add(item);
            await context.SaveChangesAsync();
        }

        /// <summary>將巡檢點從PatrolPathPeriodNPlace(巡檢時段)中移除 </summary>
        public async Task RemovePlaceFromPeriodAsync(int periodId, int placeId)
        {
            var checkItem = await context.PatrolPathPeriodNplace.FirstOrDefaultAsync(x =>
               x.PatrolPathPeriodId == periodId && x.PatroPlaceId == placeId);
            if (checkItem != null)
            {
                context.PatrolPathPeriodNplace.Remove(checkItem);
                await context.SaveChangesAsync();
            }

            var findExamItems = await context.EquipmentExamItem
                .AsNoTracking()
                .Where(x => x.PatrolPlaceId == placeId)
                .Select(x => x.Id)
                .ToArrayAsync();

            if (findExamItems.Any())
            {
                var itemsInPeriod = await context.PatrolPathPeriodNexamItem
                    .AsNoTracking()
                    .Where(x => x.PatrolPathPeriodId == periodId && findExamItems.Contains(x.EquipmentExamItemId))
                    .ToListAsync();

                if (itemsInPeriod.Any())
                {
                    context.PatrolPathPeriodNexamItem.RemoveRange(itemsInPeriod);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task AddAsync(PatrolPlace paraObject)
        {
            try
            {
                await context.PatrolPlace.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string aa = ex.ToString();
            }

            return;
        }

        public async Task<PatrolPlace> UpdateAsync(PatrolPlace paraObject)
        {
            PatrolPlace item = await context.PatrolPlace
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolPlace>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolPlace> DeleteAsync(PatrolPlace paraObject)
        {
            PatrolPlace item = await context.PatrolPlace.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolPlace.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }

        public async Task DisableIt(PatrolPlace paraObject)
        {
            await Task.Yield();
            PatrolPlace curritem = await context.PatrolPlace
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolPlace>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(PatrolPlace paraObject)
        {
            await Task.Yield();
            PatrolPlace curritem = await context.PatrolPlace
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PatrolPlace>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<PatrolPlace>> GetPatrolPlaceByPathOrPeriodAsync(int pathId, int? periodId, string dateStr)
        {

            List<PatrolPlace> listPatrolPlace = new List<PatrolPlace>();
            try
            {
                var tempList = await (
                    from o in context.OutCome
                    join e in context.Expand
                      on o.ExpandId equals e.Id
                    join epi in context.ExpandExamItem
                      on o.ExpandExamItemId equals epi.Id
                    join eqi in context.EquipmentExamItem
                      on epi.EquipmentExamItemId equals eqi.Id
                    join pp in context.PatrolPlace
                      on eqi.PatrolPlaceId equals pp.Id
                    where o.PatrolPathId == pathId // 7
                       && (periodId == null || e.PatrolPathPeriodId == periodId.Value) // 7
                       && e.Name.StartsWith(dateStr/*"2021-04"*/)
                    // orderby pp.Name // order by 下在這裡無效
                    select new
                    {
                        Id = pp.Id,
                        Name = pp.Name,
                    })
                    .Distinct()
                    .OrderBy(x => x.Name) // order by 要下在這裡才有效
                    .ToListAsync();

                // 整理成結果
                if (tempList != null && tempList.Count() > 0)
                {
                    foreach (var item in tempList)
                    {
                        listPatrolPlace.Add(new PatrolPlace
                        {
                            Name = item.Name,
                            Id = item.Id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return listPatrolPlace;
        }

        public async Task<string> GetPlaceNameByScopeIdAsync(int scopeId)
        {
            var result = await context.PatrolPlace
                .AsNoTracking()
                .Where(x => x.PatrolScopeId == scopeId)
                .ToListAsync();

            string PlaceName = "";
            foreach (var item in result)
            {
                PlaceName += $"{item.Name}、";
            }
            if (PlaceName.Length > 0) PlaceName = PlaceName.Substring(0, PlaceName.Length - 1);

            return PlaceName;
        }
    }
}
