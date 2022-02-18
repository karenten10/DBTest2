using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class ContractorShiftService
    {
        private readonly InspectionDBContext context;

        public ContractorShiftService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<ContractorShift>> GetAsync()
        {
            return Task.FromResult(context.ContractorShift
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<List<ContractorShift>> GetAllAsync()
        {
            return await context.ContractorShift
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<ContractorShift> GetAsync(int id)
        {
            ContractorShift item = await context.ContractorShift.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<decimal> GetHoursAsync(int id)
        {
            ContractorShift item = await context.ContractorShift.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item.Hours != null ? item.Hours.Value : 0;
        }

        public async Task AddAsync(ContractorShift paraObject)
        {
            try
            {
                await context.ContractorShift.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<ContractorShift> UpdateAsync(ContractorShift paraObject)
        {
            ContractorShift item = await context.ContractorShift
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<ContractorShift>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<ContractorShift> DeleteAsync(ContractorShift paraObject)
        {
            await Task.Delay(100);
            ContractorShift item = await context.ContractorShift.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.ContractorShift.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            var result = await context.ContractorShift
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ShiftName == name);

            return result != null ? result.Id : null;
        }

        public async Task<string?> GetNameByIdAsync(int? id)
        {
            var result = await context.ContractorShift
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.ShiftName : null;
        }
    }
}
