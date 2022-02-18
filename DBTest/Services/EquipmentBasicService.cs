using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentBasicService
    {
        private readonly InspectionDBContext context;

        public EquipmentBasicService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentBasic>> GetAsync()
        {
            return Task.FromResult(context.EquipmentBasic
                .OrderByDescending(x => x.EquipmentId)
                .AsNoTracking().AsQueryable());
        }

        public async Task<EquipmentBasic> GetAsync(int id)
        {
            EquipmentBasic item = await context.EquipmentBasic.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public Task<IQueryable<EquipmentBasic>> GetByHeaderIDAsync(int id)
        {
            return Task.FromResult(context.EquipmentBasic
                .AsNoTracking()
                // INNER JOIN [Department] AS [d] ON [t].[DepartmentID] = [d].[DepartmentID]
                .Include(x => x.Equipment)
                .Where(x => x.EquipmentId == id) // 撈出該科系底下的全部課程
                .AsQueryable());
        }

        // 這個才是標準寫法
        public async Task<EquipmentBasic> GetByHeaderID2Async(int id)
        {
            return await context.EquipmentBasic
                .AsNoTracking()
                .Include(x => x.Equipment)
                .Where(x => x.EquipmentId == id) // 撈出該科系底下的全部課程
                .FirstOrDefaultAsync(); // 只撈出第一筆
        }

        public async Task AddAsync(EquipmentBasic paraObject)
        {
            await context.EquipmentBasic.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentBasic> UpdateAsync(EquipmentBasic paraObject)
        {
            EquipmentBasic item = await context.EquipmentBasic
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
                    context.CleanAllEFCoreTracking<EquipmentBasic>();
                    #endregion
                    // set Modified flag in your entry
                    context.Entry(paraObject).State = EntityState.Modified;

                    // save 
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string ex = e.ToString();
                }
                
                return paraObject;
            }
        }

        public async Task<EquipmentBasic> DeleteAsync(EquipmentBasic paraObject)
        {
            await Task.Delay(100);
            EquipmentBasic item = await context.EquipmentBasic.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentBasic.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
