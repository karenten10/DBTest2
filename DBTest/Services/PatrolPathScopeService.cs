using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PatrolPathScopeService
    {
        private readonly InspectionDBContext context;

        public PatrolPathScopeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PatrolPathScope>> GetAsync()
        {
            return Task.FromResult(context.PatrolPathScope
                .Include(x => x.PatrolScope)
                .Include(x => x.PatrolPath)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<PatrolPathScope>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.PatrolPathScope
                .Include(x => x.PatrolPath)
                .Include(x => x.PatrolScope)
                .Where(x => x.PatrolScopeId == paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<PatrolPathScope> GetAsync(int id)
        {
            PatrolPathScope item = await context.PatrolPathScope.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(PatrolPathScope paraObject)
        {
            await context.PatrolPathScope.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolPathScope> UpdateAsync(PatrolPathScope paraObject)
        {
            PatrolPathScope item = await context.PatrolPathScope
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolPathScope>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolPathScope> DeleteAsync(PatrolPathScope paraObject)
        {
            PatrolPathScope item = await context.PatrolPathScope.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolPathScope.Remove(item);
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
