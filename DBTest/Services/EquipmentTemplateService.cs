using AutoMapper;
using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InspectionBlazor.Services
{
    public class EquipmentTemplateService
    {
        private readonly InspectionDBContext context;
        private readonly IMapper mapper;
        private readonly ILogger<EquipmentTemplateService> logger;

        public EquipmentTemplateService(InspectionDBContext context,
            IMapper mapper,
            ILogger<EquipmentTemplateService> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public Task<IQueryable<EquipmentTemplate>> GetAsync()
        {
            return Task.FromResult(context.EquipmentTemplate
                .AsNoTracking().AsQueryable());
        }
        public async Task<EquipmentTemplate> GetAsync(int id)
        {
            EquipmentTemplate item = await context.EquipmentTemplate
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(EquipmentTemplate paraObject)
        {
            await context.EquipmentTemplate.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<EquipmentTemplate>();
            return;
        }

        public async Task<EquipmentTemplate> UpdateAsync(EquipmentTemplate paraObject)
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

            EquipmentTemplate item = await context.EquipmentTemplate
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentTemplate>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<EquipmentTemplate>();
                return paraObject;
            }
        }

        public async Task<EquipmentTemplate> DeleteAsync(EquipmentTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentTemplate item = await context.EquipmentTemplate.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentTemplate.Remove(item);
                await context.SaveChangesAsync();
                try
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                context.CleanAllEFCoreTracking<EquipmentTemplate>();
                return item;
            }
        }
        public async Task DisableIt(EquipmentTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentTemplate curritem = await context.EquipmentTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentTemplate>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(EquipmentTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentTemplate curritem = await context.EquipmentTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentTemplate>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task<bool> SyncThem(EquipmentTemplate paraObject)
        {
            bool result;
            try
            {
                await Task.Delay(100);
                context.CleanAllEFCoreTracking<EquipmentTemplate>();
                context.CleanAllEFCoreTracking<EquipmentExamItemTemplate>();
                context.CleanAllEFCoreTracking<EquipmentExamItem>();
                var allItems = await context.EquipmentExamItemTemplate
                    .AsNoTracking()
                    .Where(x => x.EquipmentTemplateId == paraObject.Id)
                    .ToListAsync();
                #region 逐一更新範本中的檢驗項目到現有套用的設備中
                var allEquips = await context.Equipment
                    .AsNoTracking()
                    .Where(x => x.EquipmentTemplateId == paraObject.Id)
                    .ToListAsync();
                foreach (var EquipItem in allEquips)
                {
                    var equipExamItems = await context.EquipmentExamItem
                        .AsNoTracking()
                        .Where(x => x.EquipmentId == EquipItem.Id)
                        .ToListAsync();
                    var lastEquipExamItems = await context.EquipmentExamItem
                        .AsNoTracking()
                        .OrderByDescending(x => x.OrderId)
                        .FirstOrDefaultAsync(x => x.EquipmentId == EquipItem.Id);

                    //if (lastEquipExamItems == null)
                    //{
                    //    continue;
                    //}
                    //var lastOrderId = lastEquipExamItems.OrderId + 10;
                    int lastOrderId = 0; // 預設從第1筆開始
                    int patrolPlaceId = EquipItem.PatrolPlaceId; // 預設巡檢點用設備的
                                                                 // 如果原本已有檢驗項目
                    if (lastEquipExamItems != null)
                    {
                        // 把找出最後1筆檢驗項目,順序往後加10
                        lastOrderId = lastEquipExamItems.OrderId.GetValueOrDefault() + 10;

                        // 巡檢點改用找出最後1筆的
                        patrolPlaceId = lastEquipExamItems.PatrolPlaceId;
                    }

                    #region 開始檢測，範本中的檢驗項目，是否都存在於現在設備內
                    foreach (var ExamTempItem in allItems)
                    {
                        var checkItem = equipExamItems
                            .FirstOrDefault(x => x.Guid == ExamTempItem.Guid);
                        if (checkItem == null)
                        {
                            #region 這個範本檢驗項目，不存在於該設備內
                            var addExamItem = mapper.Map<EquipmentExamItem>(ExamTempItem);
                            addExamItem.Id = 0;
                            addExamItem.IsSync = true;
                            addExamItem.EquipmentId = EquipItem.Id;
                            addExamItem.PatrolPlaceId = patrolPlaceId;
                            addExamItem.OrderId = lastOrderId;
                            addExamItem.Guid = ExamTempItem.Guid;
                            lastOrderId += 10;
                            context.Entry(addExamItem).State = EntityState.Added;
                            #endregion
                        }
                        else
                        {
                            #region 這個範本檢驗項目，存在於該設備內，需要進行更新
                            if (checkItem.IsSync == true)
                            {
                                var UpdateExamItem = mapper.Map<EquipmentExamItem>(ExamTempItem);
                                UpdateExamItem.Id = checkItem.Id;
                                UpdateExamItem.IsSync = checkItem.IsSync;
                                UpdateExamItem.EquipmentId = checkItem.EquipmentId;
                                UpdateExamItem.PatrolPlaceId = checkItem.PatrolPlaceId;
                                context.Entry(UpdateExamItem).State = EntityState.Modified;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                #endregion
                await context.SaveChangesAsync();

                context.CleanAllEFCoreTracking<EquipmentExamItem>();
                context.CleanAllEFCoreTracking<EquipmentTemplate>();
                context.CleanAllEFCoreTracking<EquipmentExamItemTemplate>();

                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError($"SyncThem err = {ex.Message}");
                result = false;
            }

            return result;
        }
    }
}
