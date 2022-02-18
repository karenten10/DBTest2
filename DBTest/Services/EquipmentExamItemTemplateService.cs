using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentExamItemTemplateService
    {
        private readonly InspectionDBContext context;

        public EquipmentExamItemTemplateService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentExamItemTemplate>> GetAsync()
        {
            return Task.FromResult(context.EquipmentExamItemTemplate
                .Include(x => x.EquipmentTemplate)
                .OrderBy(x => x.OrderId)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<EquipmentExamItemTemplate>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.EquipmentExamItemTemplate
                .Include(x => x.EquipmentTemplate)
                .OrderBy(x => x.OrderId)
                .Where(x => x.EquipmentTemplateId == paraObj)
                .AsNoTracking()
                .AsQueryable());
        }
        public Task<IQueryable<EquipmentExamItemTemplate>> GetRelationAsync(IQueryable<EquipmentExamItemTemplate> paraObj)
        {

            return Task.FromResult(paraObj
                .Include(x => x.EquipmentTemplate).
                AsNoTracking().AsQueryable());
        }

        public async Task<EquipmentExamItemTemplate> GetAsync(int id)
        {
            EquipmentExamItemTemplate item = await context.EquipmentExamItemTemplate
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(EquipmentExamItemTemplate paraObject)
        {
            var searchItem = context.EquipmentExamItemTemplate
                .Where(x => x.EquipmentTemplateId == paraObject.EquipmentTemplateId)
                .OrderBy(x => x.OrderId)
                .LastOrDefault();
            if (searchItem != null)
            {
                paraObject.OrderId = searchItem.OrderId + 10;
            }
            await context.EquipmentExamItemTemplate.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentExamItemTemplate> UpdateAsync(EquipmentExamItemTemplate paraObject)
        {
            EquipmentExamItemTemplate item = await context.EquipmentExamItemTemplate
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentExamItemTemplate>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentExamItemTemplate> DeleteAsync(EquipmentExamItemTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItemTemplate item = await context.EquipmentExamItemTemplate.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentExamItemTemplate.Remove(item);
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

        public async Task DisableIt(EquipmentExamItemTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItemTemplate curritem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItemTemplate>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = "Y";
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task EnableIt(EquipmentExamItemTemplate paraObject)
        {
            await Task.Delay(100);
            EquipmentExamItemTemplate curritem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItemTemplate>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = "N";
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task MoveUp(EquipmentExamItemTemplate paraObject)
        {
            if (paraObject.OrderId == 0) return;
            EquipmentExamItemTemplate nextItem = await context.EquipmentExamItemTemplate
                .OrderByDescending(x => x.OrderId)
                .Where(x => x.OrderId < paraObject.OrderId && x.EquipmentTemplateId == paraObject.EquipmentTemplateId)
                .Take(1).FirstOrDefaultAsync();
            if (nextItem == null) return;
            nextItem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == nextItem.Id);
            EquipmentExamItemTemplate curritem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItemTemplate>().Local)
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

        public async Task MoveDown(EquipmentExamItemTemplate paraObject)
        {
            EquipmentExamItemTemplate nextItem = await context.EquipmentExamItemTemplate
                .OrderBy(x => x.OrderId)
                .Where(x => x.OrderId > paraObject.OrderId && x.EquipmentTemplateId == paraObject.EquipmentTemplateId)
                .Take(1).FirstOrDefaultAsync();
            if (nextItem == null) return;
            nextItem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == nextItem.Id);
            EquipmentExamItemTemplate curritem = await context.EquipmentExamItemTemplate
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<EquipmentExamItemTemplate>().Local)
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
    }
}
