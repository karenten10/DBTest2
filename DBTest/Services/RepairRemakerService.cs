using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class RepairRemakerService
    {
        private readonly InspectionDBContext context;

        public RepairRemakerService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<RepairRemaker>> GetAsync()
        {
            return Task.FromResult(context.RepairRemaker
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<RepairRemaker> GetAsync(int id)
        {
            RepairRemaker item = await context.RepairRemaker.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(RepairRemaker paraObject)
        {
            try
            {
                await context.RepairRemaker.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<RepairRemaker> UpdateAsync(RepairRemaker paraObject)
        {
            RepairRemaker item = await context.RepairRemaker
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairRemaker>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairRemaker> DeleteAsync(RepairRemaker paraObject)
        {
            await Task.Delay(100);
            RepairRemaker item = await context.RepairRemaker.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.RepairRemaker.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
