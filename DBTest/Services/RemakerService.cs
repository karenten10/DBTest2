using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class RemakerService
    {
        private readonly InspectionDBContext context;

        public RemakerService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Remaker>> GetAsync()
        {
            return Task.FromResult(context.Remaker
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<Remaker> GetAsync(int id)
        {
            Remaker item = await context.Remaker.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Remaker paraObject)
        {
            try
            {
                await context.Remaker.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<Remaker> UpdateAsync(Remaker paraObject)
        {
            Remaker item = await context.Remaker
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<Remaker>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<Remaker> DeleteAsync(Remaker paraObject)
        {
            await Task.Delay(100);
            Remaker item = await context.Remaker.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.Remaker.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
