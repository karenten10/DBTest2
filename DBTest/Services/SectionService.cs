using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class SectionService
    {
        private readonly InspectionDBContext context;

        public SectionService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Section>> GetAsync()
        {
            return Task.FromResult(context.Section.AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<Section>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.Section
                .Include(x => x.Equipment)
                .Where(x => x.Id == paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<Section> GetAsync(int id)
        {
            Section item = await context.Section.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Section paraObject)
        {
            await context.Section.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<Section> UpdateAsync(Section paraObject)
        {
            Section item = await context.Section
       .AsNoTracking()
       .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<Section>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<Section> DeleteAsync(Section paraObject)
        {
            Section item = await context.Section.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.Section.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }

        public async Task DisableIt(Section paraObject)
        {
            await Task.Delay(100);
            Section curritem = await context.Section
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Section>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(Section paraObject)
        {
            await Task.Delay(100);
            Section curritem = await context.Section
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Section>().Local)
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
