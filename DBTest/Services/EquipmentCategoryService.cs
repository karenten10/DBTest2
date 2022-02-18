using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentCategoryService
    {
        private readonly InspectionDBContext context;

        public EquipmentCategoryService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<EquipmentCategory> GetAsync(int id)
        {
            EquipmentCategory item = await context.EquipmentCategory.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public Task<IQueryable<EquipmentCategory>> GetAsync()
        {
            return Task.FromResult(context.EquipmentCategory.AsNoTracking().AsQueryable());
        }
        public async Task AddAsync(EquipmentCategory paraObject)
        {
            await context.EquipmentCategory.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentCategory> UpdateAsync(EquipmentCategory paraObject)
        {
            EquipmentCategory item = await context.EquipmentCategory
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentCategory>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentCategory> DeleteAsync(EquipmentCategory paraObject)
        {
            await Task.Delay(100);
            EquipmentCategory item = await context.EquipmentCategory.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentCategory.Remove(item);
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

        public async Task<int> CounterDetailAsync(int id)
        {
            //  int counter = (await context.Detail.Where(x => x.MasterId == id).ToListAsync()).Count();
            int counter = 0;
            return counter;
        }
    }

}
