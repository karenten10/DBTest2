using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;

namespace InspectionBlazor.Services
{
    public class VirtualEquipmentExamItemService
    {
        private readonly InspectionDBContext context;

        public VirtualEquipmentExamItemService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<VirtualEquipmentExamItem>> GetAsync()
        {
            return Task.FromResult(context.VirtualEquipmentExamItem
                .Include(x => x.EquipmentExam)
                .Include(x=>x.Equipment)
                .Include(x => x.EquipmentExamItem)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<VirtualEquipmentExamItem>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.VirtualEquipmentExamItem
                .Include(x => x.EquipmentExam)
                .Include(x => x.Equipment)
                .Include(x => x.EquipmentExamItem)
                .Where(x=>x.EquipmentExamId==paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<VirtualEquipmentExamItem> GetAsync(int id)
        {
            VirtualEquipmentExamItem item = await context.VirtualEquipmentExamItem.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(VirtualEquipmentExamItem paraObject)
        {
            await context.VirtualEquipmentExamItem.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<VirtualEquipmentExamItem> UpdateAsync(VirtualEquipmentExamItem paraObject)
        {
            VirtualEquipmentExamItem item = await context.VirtualEquipmentExamItem
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<VirtualEquipmentExamItem>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<VirtualEquipmentExamItem> DeleteAsync(VirtualEquipmentExamItem paraObject)
        {
            VirtualEquipmentExamItem item = await context.VirtualEquipmentExamItem.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.VirtualEquipmentExamItem.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(VirtualEquipmentExamItem paraObject)
        {
            await Task.Delay(100);
            VirtualEquipmentExamItem curritem = await context.VirtualEquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<VirtualEquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(VirtualEquipmentExamItem paraObject)
        {
            await Task.Delay(100);
            VirtualEquipmentExamItem curritem = await context.VirtualEquipmentExamItem
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<VirtualEquipmentExamItem>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
    }
}
