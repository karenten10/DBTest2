using Database.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InspectionBlazor.Extensions;

namespace InspectionBlazor.Services
{
    public class EquipmentCategoryPartsService
    {
        private readonly InspectionDBContext context;

        public EquipmentCategoryPartsService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentCategoryParts>> GetAsync()
        {
            return Task.FromResult(context.EquipmentCategoryParts.Include(x => x.EquipmentCategory).AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<EquipmentCategoryParts>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.EquipmentCategoryParts
                .Include(x => x.EquipmentCategory)
                .Where(x => x.EquipmentCategoryId == paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<EquipmentCategoryParts> GetAsync(int id)
        {
            EquipmentCategoryParts item = await context.EquipmentCategoryParts.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(EquipmentCategoryParts paraObject)
        {
            await context.EquipmentCategoryParts.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentCategoryParts> UpdateAsync(EquipmentCategoryParts paraObject)
        {
            EquipmentCategoryParts item = await context.EquipmentCategoryParts
         .AsNoTracking()
         .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentCategoryParts>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentCategoryParts> DeleteAsync(EquipmentCategoryParts paraObject)
        {
            EquipmentCategoryParts item = await context.EquipmentCategoryParts.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.EquipmentCategoryParts.Remove(item);
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
