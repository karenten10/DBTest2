using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class AssignedMattersService
    {
        private readonly InspectionDBContext context;

        public AssignedMattersService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<AssignedMatters>> GetAsync()
        {
            return Task.FromResult(context.AssignedMatters
                .OrderByDescending(x => x.AnnounceTime)
                .AsNoTracking().AsQueryable());
        }

        public async Task<AssignedMatters> GetAsync(int id)
        {
            AssignedMatters item = await context.AssignedMatters.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(AssignedMatters paraObject)
        {
            await context.AssignedMatters.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<AssignedMatters> UpdateAsync(AssignedMatters paraObject)
        {
            AssignedMatters item = await context.AssignedMatters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<AssignedMatters>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<AssignedMatters> DeleteAsync(AssignedMatters paraObject)
        {
            await Task.Delay(100);
            AssignedMatters item = await context.AssignedMatters.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.AssignedMatters.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
    }
}
