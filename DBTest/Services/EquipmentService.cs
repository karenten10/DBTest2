using AutoMapper;
using Dapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentService
    {
        private readonly InspectionDBContext context;
        private readonly IMapper mapper;

        public EquipmentService(InspectionDBContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<IQueryable<Equipment>> GetAsync()
        {
            return Task.FromResult(context.Equipment
                .Include(x => x.Section)
                .Include(x => x.PatrolPlace)
                .Include(x => x.EquipmentTemplate)
                .AsNoTracking()
                .OrderBy(x => x.Section.Name)
                .ThenBy(x=> x.EquipmentName)
                .AsQueryable());
        }

        public Task<IQueryable<Equipment>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.Equipment
                .Include(x => x.Section)
                .Include(x => x.PatrolPlace)
                .Include(x => x.EquipmentTemplate)
                .Where(x => x.SectionId == paraObj)
                .AsNoTracking().AsQueryable());
        }

        public async Task<Equipment> GetAsync(int id)
        {
            Equipment item = await context.Equipment
                .Include(x => x.Section)
                .Include(x => x.PatrolPlace)
                .Include(x => x.EquipmentTemplate)
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Equipment paraObject)
        {
            await context.Equipment.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<Equipment>();
            return;
        }

        public async Task<Equipment> UpdateAsync(Equipment paraObject)
        {
            #region EF Core 追蹤查詢所造成的問題說明
            // 若再進行搜尋該修改紀錄的時候，使用了 追蹤查詢 (也就是，沒有使用 .AsNoTracking()方法)
            // 將會造成快取記錄在電腦端，而這裡若要進行 
            // context.Entry(paraObject).State = EntityState.Modified; 呼叫與更新的時候
            // 將會造成問題
            // System.InvalidOperationException: The instance of entity type 'Person' cannot be tracked 
            // because another instance with the same key value for {'PersonId'} is already being tracked. 
            // When attaching existing entities, ensure that only one entity instance with a given key value
            // is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' 
            // to see the conflicting key values.
            #endregion

            Equipment item = await context.Equipment
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    #region 在這裡需要設定需要解除快取紀錄
                    context.CleanAllEFCoreTracking<Equipment>();
                    #endregion
                    // set Modified flag in your entry
                    context.Entry(paraObject).State = EntityState.Modified;

                    // save 
                    await context.SaveChangesAsync();
                    context.CleanAllEFCoreTracking<Equipment>();
                }
                catch (Exception ex)
                {
                    var aa = ex;
                }
               
                return paraObject;
            }
        }

        public async Task<Equipment> DeleteAsync(Equipment paraObject)
        {
            await Task.Delay(100);
            Equipment item = await context.Equipment.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.Equipment.Remove(item);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<Equipment>();
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

        public async Task DisableIt(Equipment paraObject)
        {
            await Task.Delay(100);
            Equipment curritem = await context.Equipment
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Equipment>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<Equipment>();
            return;
        }

        public async Task EnableIt(Equipment paraObject)
        {
            await Task.Delay(100);
            Equipment curritem = await context.Equipment
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Equipment>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<Equipment>();
            return;
        }

        public async Task<List<Equipment>> GetEquipmentByScopeIDAsync(int scopeId)
        {
            var result = await context.Equipment.Include(x => x.PatrolPlace)
                               .ThenInclude(x => x.PatrolScope)
                               .Where(x => x.PatrolPlace.PatrolScope.Id == scopeId)
                               .Select(x => new Equipment { Id = x.Id, EquipmentName = x.EquipmentName })
                               .Distinct()
                               .ToListAsync();
            return result;
        }

        public async Task ApplyTemplate(Equipment paraObject)
        {
            await Task.Delay(100);
            foreach (var item in context.Set<Equipment>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            foreach (var item in context.Set<EquipmentTemplate>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            Equipment curritem = await context.Equipment
                .AsNoTracking()
                .Include(x => x.EquipmentTemplate)
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            var anyEquipExamItem = await context.EquipmentExamItem
                .AsNoTracking()
                .OrderByDescending(x => x.OrderId)
                .FirstOrDefaultAsync(x => x.EquipmentId == curritem.Id);

            #region 依據樣板紀錄，進行套用
            if (curritem.EquipmentTemplateId.HasValue)
            {
                var fooTemplate = await context.EquipmentTemplate
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == curritem.EquipmentTemplateId);
                if (fooTemplate != null)
                {
                    var fooTemplateItems = await context.EquipmentExamItemTemplate
                        .AsNoTracking()
                        .Include(x => x.EquipmentTemplate)
                        .Where(x => x.EquipmentTemplateId == fooTemplate.Id)
                        .ToListAsync();
                    context.CleanAllEFCoreTracking<EquipmentExamItem>();
                    int lastOrderId = 0; // 預設從第1筆開始
                    int patrolPlaceId = curritem.PatrolPlaceId; // 預設巡檢點用設備的
                    // 如果原本已有檢驗項目
                    if (anyEquipExamItem != null)
                    {
                        // 把找出最後1筆檢驗項目,順序往後加10
                        lastOrderId = anyEquipExamItem.OrderId.GetValueOrDefault() + 10;

                        // 巡檢點改用找出最後1筆的
                        patrolPlaceId = anyEquipExamItem.PatrolPlaceId;
                    }

                    foreach (var item in fooTemplateItems)
                    {
                        var barEquipExamItem = await context.EquipmentExamItem
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => (x.EquipmentId == curritem.Id && x.Guid == item.Guid));
                        // 不存在相同guid才要新增
                        if (barEquipExamItem == null)
                        {
                            var addExamItem = mapper.Map<EquipmentExamItem>(item);
                            addExamItem.Id = 0;
                            addExamItem.IsSync = true;
                            addExamItem.EquipmentId = curritem.Id;
                            addExamItem.PatrolPlaceId = patrolPlaceId;
                            addExamItem.OrderId = lastOrderId;
                            lastOrderId += 10;
                            context.Entry(addExamItem).State = EntityState.Added;
                        }
                    }
                    await context.SaveChangesAsync();
                    context.CleanAllEFCoreTracking<EquipmentExamItem>();
                }
            }
            #endregion
            return;
        }

        public Task<List<Equipment>> GetEquipmentALL()
        {
            return Task.FromResult(context.Equipment
             .AsNoTracking()
             .Where(x => x.Status == "N")
             .ToList());
        }

        public async Task<List<PathPlaceEquipment>> GetPathPlaceEquipment(List<PathPlace> pathPlaceList)
        {
            var resultList = new List<PathPlaceEquipment>();

            int index = 0;
            foreach (var item in pathPlaceList)
            {
                var equipmentList = await 
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
                       && c.Id == item.PlaceId
                    select new PathPlaceEquipment
                    {
                        FullId = e.Id + "-" + i.Id + "-" + c.Id + "-" + a.Id,
                        PathId = e.Id,
                        PathName = e.Name,
                        ScopeId = i.Id,
                        ScopeName = i.Name,
                        PlaceId = c.Id, 
                        PlaceName = c.Name,
                        EquipmentId = a.Id, 
                        EquipmentName = a.EquipmentName,
                        PathPlaceName = e.Name + "-" + i.Name + "-" + c.Name,
                        FullName = e.Name + "-" + i.Name + "-" + c.Name + "-" + a.EquipmentName
                    }).Distinct().ToListAsync();

                foreach (var itemEquipment in equipmentList)
                {
                    // itemEquipment.Index = index++;
                    resultList.Add(itemEquipment);
                }
            }

            resultList = resultList.OrderBy(x => x.FullName).ToList();

            return resultList;
        }
    }
}
