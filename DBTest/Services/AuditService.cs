using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class AuditService
    {
        private readonly InspectionDBContext context;

        public AuditService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Audit>> GetAsync()
        {
            return Task.FromResult(context.Audit
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<Audit> GetAsync(int id)
        {
            Audit item = await context.Audit.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Audit paraObject)
        {
            try
            {
                await context.Audit.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<Audit> UpdateAsync(Audit paraObject)
        {
            Audit item = await context.Audit
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<Audit>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<Audit> DeleteAsync(Audit paraObject)
        {
            await Task.Delay(100);
            Audit item = await context.Audit.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.Audit.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
