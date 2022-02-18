using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class WorkScheduleService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<WorkScheduleService> logger;

        public WorkScheduleService(InspectionDBContext context, ILogger<WorkScheduleService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<WorkScheduleAdapterModel>> GetAsyncByDate(DateTime SearchDate, bool isFirst)
        {
            var resultList = new List<WorkScheduleAdapterModel>();

            if(isFirst)
            {
                return resultList;
            } else
            {
                var workGroup = await
                (
                    from a in context.WorkSchedule
                    where a.WorkDate.Year == SearchDate.Year
                       && a.WorkDate.Month == SearchDate.Month
                       && a.WorkDate.Day == SearchDate.Day
                    select new
                    {
                        GroupId = a.GroupId,
                    }
                ).Distinct().ToListAsync();

                foreach (var item in workGroup)
                {
                    var fullModel = await GetByDateGroupAsync(SearchDate, item.GroupId);

                    resultList.Add(new WorkScheduleAdapterModel
                    {
                        WorkDate = SearchDate,
                        GroupId = item.GroupId,
                        GroupName = fullModel.GroupName,
                        FullName = fullModel.FullName,
                        PathIds = fullModel.PathIds,
                        WorkList = fullModel.WorkList,
                        IdList = fullModel.IdList
                    });
                }

                return resultList;
            }
        }

        public async Task<WorkScheduleAdapterModel> GetByDateGroupAsync(DateTime SearchDate, int groupId)
        {
            var result = new WorkScheduleAdapterModel();
            var queryList = await
            (
                from a in context.WorkSchedule
                join b in context.PatrolPath
                  on a.PathId equals b.Id
                join c in context.PatrolPlace
                  on a.PlaceId equals c.Id
                join d in context.Equipment
                  on a.EquipmentId equals d.Id
                join e in context.PatrolGroup
                  on  a.GroupId equals e.Id
                join f in context.PatrolScope
                  on c.PatrolScopeId equals f.Id
                where b.Status == "N"
                   && c.Status == "N"
                   && d.Status == "N"
                   && e.Status == "N"
                   && a.WorkDate.Year == SearchDate.Year
                    && a.WorkDate.Month == SearchDate.Month
                    && a.WorkDate.Day == SearchDate.Day
                    && a.GroupId == groupId
                select new WorkScheduleOne
                {
                    Id = a.Id,
                    GroupId = a.GroupId,
                    GroupName = e.GroupName,

                    PatrolPathId = b.Id,
                    PathName = b.Name,

                    PatrolScopeId = f.Id,
                    ScopeName = f.Name,

                    PatrolPlaceId = c.Id,
                    PlaceName = c.Name,

                    EquipmentId = d.Id,
                    EquipmentName = d.EquipmentName,

                }).AsNoTracking().ToListAsync();

            string allFullNames = "";
            int[] pathIds = queryList.Select(x => x.PatrolPathId)?.Distinct()?.ToArray();
            List<long> IdList = queryList.Select(x => x.Id).ToList();
            foreach(var item in queryList)
            {
                string oneFullName = item.PathName + "-" + item.ScopeName + "-" + item.PlaceName + "-" + item.EquipmentName;
                if (string.IsNullOrEmpty(allFullNames))
                {
                    allFullNames = oneFullName;
                } else
                {
                    allFullNames += "、" + oneFullName;
                }
            }

            result.GroupName = queryList.FirstOrDefault()?.GroupName;
            result.FullName = allFullNames;
            result.PathIds = pathIds;
            result.WorkList = queryList;
            result.IdList = IdList;
            return result;
        }

        public Task<IQueryable<WorkSchedule>> GetAsync(DateTime? SearchDate)
        {
            return Task.FromResult(context.WorkSchedule
                .AsNoTracking()
                .Include(x => x.PatrolGroup)
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .Include(x => x.Equipment)
                .Where(x => x.WorkDate.Year == SearchDate.Value.Year 
                    && x.WorkDate.Month == SearchDate.Value.Month
                    && x.WorkDate.Day == SearchDate.Value.Day)
                .OrderBy(x => x.PatrolGroup.GroupName)
                .ThenBy(x => x.PatrolPath.Name)
                .ThenBy(x => x.PatrolPlace.Name)
                .ThenBy(x => x.Equipment.EquipmentName)
                .AsQueryable());
        }

        public async Task<List<WorkSchedule>> GetByYearMonthAsync(DateTime SearchDate)
        {
            var result = await context.WorkSchedule
                .AsNoTracking()
                .Include(x => x.PatrolGroup)
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .Include(x => x.Equipment)
                .Where(x => x.WorkDate.Year == SearchDate.Year
                    && x.WorkDate.Month == SearchDate.Month)
                .OrderBy(x => x.WorkDate)
                .ThenBy(x => x.PatrolGroup.GroupName)
                .ThenBy(x => x.PatrolPath.Name)
                .ThenBy(x => x.PatrolPlace.Name)
                .ThenBy(x => x.Equipment.EquipmentName)
                .ToListAsync();
            return result;
        }

        public async Task<WorkSchedule> GetAsync(int id)
        {
            WorkSchedule item = await context.WorkSchedule.AsNoTracking()
                .Include(x => x.PatrolGroup)
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .Include(x => x.Equipment)
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddListAsync(WorkScheduleAdapterModel newItem)
        {
            var workDate = newItem.WorkDate;
            var groupId = newItem.GroupId;
            var addList = newItem.PathPlaceEquipments;

            foreach (var item in addList)
            {
                var addItem = new WorkSchedule()
                {
                    WorkDate = workDate,
                    GroupId = groupId,
                    PathId = item.PathId,
                    PlaceId = item.PlaceId,
                    EquipmentId = item.EquipmentId
                };

                await AddAsync(addItem);
            }
        }

        public async Task UpdateListAsync(WorkScheduleAdapterModel newItem)
        {
            var workDate = newItem.WorkDate;
            var groupId = newItem.GroupId;
            var addList = newItem.PathPlaceEquipments;

            // 先全部刪除
            await DeleteByHeaderAsync(workDate, groupId);

            foreach (var item in addList)
            {
                var addItem = new WorkSchedule()
                {
                    WorkDate = workDate,
                    GroupId = groupId,
                    PathId = item.PathId,
                    PlaceId = item.PlaceId,
                    EquipmentId = item.EquipmentId
                };

                await AddAsync(addItem);
            }
        }

        public async Task AddAsync(WorkSchedule paraObject)
        {
            try
            {
                await context.WorkSchedule.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                logger.LogError($"WorkSchedule AddAsync err = {ex.Message}");
            }

            return;
        }

        public async Task<WorkSchedule> UpdateAsync(WorkSchedule paraObject)
        {
            WorkSchedule item = await context.WorkSchedule
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<WorkSchedule>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<WorkSchedule> DeleteAsync(WorkSchedule paraObject)
        {
            await Task.Delay(100);
            WorkSchedule item = await context.WorkSchedule.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.WorkSchedule.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<bool> IsExistedAsync(bool isNew, DateTime? dateTime, int groupId, List<long> IdList)
        {
            if(isNew)
            {
                return await context.WorkSchedule
                .AsNoTracking()
                .Include(x => x.PatrolGroup)
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .Include(x => x.Equipment)
                .Where(x => x.WorkDate.Year == dateTime.Value.Year
                    && x.WorkDate.Month == dateTime.Value.Month
                    && x.WorkDate.Day == dateTime.Value.Day
                    && x.GroupId == groupId
                    && x.PatrolGroup.Status == "N"
                    && x.PatrolPath.Status == "N"
                    && x.PatrolPlace.Status == "N"
                    && x.Equipment.Status == "N")
                .AnyAsync();
            } else
            {
                return await context.WorkSchedule
                .AsNoTracking()
                .Include(x => x.PatrolGroup)
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolPlace)
                .Include(x => x.Equipment)
                .Where(x => x.WorkDate.Year == dateTime.Value.Year
                    && x.WorkDate.Month == dateTime.Value.Month
                    && x.WorkDate.Day == dateTime.Value.Day
                    && x.GroupId == groupId
                    && !IdList.Contains(x.Id)
                    && x.PatrolGroup.Status == "N"
                    && x.PatrolPath.Status == "N"
                    && x.PatrolPlace.Status == "N"
                    && x.Equipment.Status == "N")
                .AnyAsync();
            }
            
        }

        public async Task<int> DeleteByHeaderAsync(DateTime? dateTime, int groupId)
        {
            context.CleanAllEFCoreTracking<WorkSchedule>();
            List<WorkSchedule> items =
                await context.WorkSchedule
                .Where(x => x.WorkDate.Year == dateTime.Value.Year
                    && x.WorkDate.Month == dateTime.Value.Month
                    && x.WorkDate.Day == dateTime.Value.Day
                    && x.GroupId == groupId)
                .AsNoTracking().ToListAsync();
            if (items == null || items.Count < 1)
            {
                return 0;
            }
            else
            {
                context.WorkSchedule.RemoveRange(items);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<WorkSchedule>();
                return items.Count;
            }
        }

    }
}
