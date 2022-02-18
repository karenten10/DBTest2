using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentExamItemService
    {
        private readonly InspectionDBContext context;

        public EquipmentExamItemService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentExamItem>> GetAsync()
        {
            return Task.FromResult(context.EquipmentExamItem
                .Include(x => x.Equipment)
                .Include(x => x.PatrolPlace)
                .OrderBy(x => x.OrderId)
                .AsNoTracking().AsQueryable());
        }

        public Task<IQueryable<EquipmentExamItem>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.EquipmentExamItem
                .Include(x => x.Equipment)
                .Include(x => x.PatrolPlace)
                .OrderBy(x => x.OrderId)
                .Where(x => x.EquipmentId == paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<EquipmentExamItem> GetAsync(int id)
        {
            EquipmentExamItem item = await context.EquipmentExamItem.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<IQueryable<EquipmentExamItem>> GetAllExamItemsByPeriodAsync(int pathId, int periodId, string searchKey = "")
        {
            var patrolPlaceIds = await context.PatrolPathPeriodNplace
                .Where(x => x.PatrolPathPeriodId == periodId)
                .Select(x=> x.PatroPlaceId)
                .ToArrayAsync();

            var examItems = context.EquipmentExamItem
                .AsNoTracking()
                .Where(x => patrolPlaceIds.Contains(x.PatrolPlaceId))
                .Include(x => x.Equipment)
                .ThenInclude(x => x.EquipmentExam)
                .ThenInclude(x => x.PatrolPath)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.Section)
                .OrderBy(x => x.Equipment.SectionId)
                .ThenBy(x => x.Equipment.EquipmentName)
                .ThenBy(x => x.OrderId)
                .Where(x => searchKey == "" || x.Equipment.EquipmentName.Contains(searchKey) |
                    x.Equipment.EquipmentIdExt.Contains(searchKey) |
                    x.Equipment.Section.Name.Contains(searchKey) | x.Name.Contains(searchKey))
                .AsQueryable();

            return examItems;
        }

        public async Task<bool> ExamItemWetherExistInPeriodAsync(int peroidId, int examItemId)
        {
            PatrolPathPeriodNexamItem item = await context.PatrolPathPeriodNexamItem.AsNoTracking()
                .FirstOrDefaultAsync(x => x.EquipmentExamItemId == examItemId &&
                x.PatrolPathPeriodId == peroidId);
            if (item == null)
                return false;
            else
                return true;
        }

        public async Task SelectAllExamItemToPeriodAsync(int periodId, int patrolPathId, string searchKey = "")
        {
            List<PatrolPathPeriodNexamItem> patrolPathPeriodNexamItems = new List<PatrolPathPeriodNexamItem>();

            var result = await GetAllExamItemsByPeriodAsync(patrolPathId, periodId, searchKey);
            foreach (var item in result)
            {
                bool isExist = await ExamItemWetherExistInPeriodAsync(periodId, item.Id);
                if (!isExist)
                {
                    patrolPathPeriodNexamItems.Add(new PatrolPathPeriodNexamItem()
                    {
                        EquipmentExamItemId = item.Id,
                        PatrolPathPeriodId = periodId
                    });
                }
            }

            await context.PatrolPathPeriodNexamItem.AddRangeAsync(patrolPathPeriodNexamItems);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<PatrolPathPeriodNexamItem>();
        }

        public async Task UnSelectAllExamItemToPeriodAsync(int periodId, string searchKey = "")
        {
            List<PatrolPathPeriodNexamItem> patrolPathPeriodNexamItems = new List<PatrolPathPeriodNexamItem>();

            if (searchKey == "")
            {
                patrolPathPeriodNexamItems = await context.PatrolPathPeriodNexamItem
                    .AsNoTracking()
                    .Where(x => x.PatrolPathPeriodId == periodId)
                    .ToListAsync();
            }
            else
            {
                var result = await GetAllExamItemsByPeriodAsync(0, periodId, searchKey);
                foreach (var item in result)
                {
                    var findId = await context.PatrolPathPeriodNexamItem
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.EquipmentExamItemId == item.Id && x.PatrolPathPeriodId == periodId);
                    if (findId == null) continue;

                    patrolPathPeriodNexamItems.Add(new PatrolPathPeriodNexamItem()
                    {
                        Id = findId.Id,
                        EquipmentExamItemId = item.Id,
                        PatrolPathPeriodId = periodId
                    });
                }
            }

            if (patrolPathPeriodNexamItems.Any())
            {
                context.PatrolPathPeriodNexamItem.RemoveRange(patrolPathPeriodNexamItems);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<PatrolPathPeriodNexamItem>();
            }
        }

        public async Task AddExamItemToPeriodAsync(int peroidId, int examItemId)
        {
            PatrolPathPeriodNexamItem item = new PatrolPathPeriodNexamItem()
            {
                EquipmentExamItemId = examItemId,
                PatrolPathPeriodId = peroidId
            };
            context.PatrolPathPeriodNexamItem.Add(item);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<PatrolPathPeriodNexamItem>();
        }

        public async Task RemoveExamItemToPeriodAsync(int peroidId, int examItemId)
        {
            var checkItem = await context.PatrolPathPeriodNexamItem.FirstOrDefaultAsync(x =>
               x.PatrolPathPeriodId == peroidId && x.EquipmentExamItemId == examItemId);
            if (checkItem != null)
            {
                context.PatrolPathPeriodNexamItem.Remove(checkItem);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<PatrolPathPeriodNexamItem>();
            }
        }

        public async Task AddAsync(EquipmentExamItem paraObject)
        {
            var searchItem = context.EquipmentExamItem
                .Where(x => x.EquipmentId == paraObject.EquipmentId)
                .OrderBy(x => x.OrderId)
                .LastOrDefault();
            if (searchItem != null)
            {
                paraObject.OrderId = searchItem.OrderId + 10;
            }
            await context.EquipmentExamItem.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentExamItem> UpdateAsync(EquipmentExamItem paraObject)
        {
            EquipmentExamItem item = await context.EquipmentExamItem
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentExamItem>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentExamItem> DeleteAsync(EquipmentExamItem paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItem item = await context.EquipmentExamItem.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentExamItem.Remove(item);
                await context.SaveChangesAsync();
                try
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }

        public async Task MoveUp(EquipmentExamItem paraObject)
        {
            if (paraObject.OrderId == 0) return;
            EquipmentExamItem nextItem = await context.EquipmentExamItem
                .OrderByDescending(x => x.OrderId)
                .Where(x => x.OrderId < paraObject.OrderId && x.EquipmentId == paraObject.EquipmentId)
                .Take(1).FirstOrDefaultAsync();
            if (nextItem == null) return;
            nextItem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == nextItem.Id);
            EquipmentExamItem curritem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            int swapOrderId = (int)curritem.OrderId;
            curritem.OrderId = nextItem.OrderId;
            nextItem.OrderId = swapOrderId;
            context.Entry(curritem).State = EntityState.Modified;
            context.Entry(nextItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task DisableIt(EquipmentExamItem paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItem curritem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = "Y";
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(EquipmentExamItem paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItem curritem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = "N";
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task MoveDown(EquipmentExamItem paraObject)
        {
            EquipmentExamItem nextItem = await context.EquipmentExamItem
                .OrderBy(x => x.OrderId)
                .Where(x => x.OrderId > paraObject.OrderId && x.EquipmentId == paraObject.EquipmentId)
                .Take(1).FirstOrDefaultAsync();
            if (nextItem == null) return;
            nextItem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == nextItem.Id);
            EquipmentExamItem curritem = await context.EquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            int swapOrderId = (int)curritem.OrderId;
            curritem.OrderId = nextItem.OrderId;
            nextItem.OrderId = swapOrderId;
            context.Entry(curritem).State = EntityState.Modified;
            context.Entry(nextItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        /// <summary>回傳審核依據除了標準以外的檢查項目</summary>
        public async Task<List<EquipmentExamItem>> GetEquipmentItemByEquipmentIdForCharViewAsync(int EquipmentId)
        {
            var result = await context.EquipmentExamItem
                .Where(x => x.Status == "N" && x.EquipmentId == EquipmentId && x.ExamConditionName != "標準")
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<string> GetTemplateNameAsync(int itemId)
        {
            var findGuid = await context.EquipmentExamItem
                .AsNoTracking()
                .Where(x => x.Id == itemId)
                .Select(x => x.Guid)
                .FirstOrDefaultAsync();

            return findGuid != null 
                ? await context.EquipmentExamItemTemplate
                    .AsNoTracking()
                    .Include(x => x.EquipmentTemplate)
                    .Where(x => x.Guid == findGuid.Value)
                    .Select(x => x.EquipmentTemplate.Title)
                    .FirstOrDefaultAsync()
                : string.Empty;
        }
    }
}
