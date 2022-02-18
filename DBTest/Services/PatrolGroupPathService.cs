using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PatrolGroupPathService
    {
        private readonly InspectionDBContext context;

        public PatrolGroupPathService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PatrolGroupNpath>> GetAsync()
        {
            return Task.FromResult(context.PatrolGroupNpath
                .Include(x => x.Group)
                .Include(x => x.PatrolPath)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<PatrolGroupNpath>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.PatrolGroupNpath
                .Include(x => x.PatrolPath)
                .Include(x => x.Group)
                .Where(x => x.GroupId == paraObj).AsNoTracking().AsQueryable());
        }

        public async Task<PatrolGroupNpath> GetAsync(int id)
        {
            PatrolGroupNpath item = await context.PatrolGroupNpath.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(PatrolGroupNpath paraObject)
        {
            await context.PatrolGroupNpath.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PatrolGroupNpath> UpdateAsync(PatrolGroupNpath paraObject)
        {
            PatrolGroupNpath item = await context.PatrolGroupNpath
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PatrolGroupNpath>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PatrolGroupNpath> DeleteAsync(PatrolGroupNpath paraObject)
        {
            PatrolGroupNpath item = await context.PatrolGroupNpath.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.PatrolGroupNpath.Remove(item);
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
