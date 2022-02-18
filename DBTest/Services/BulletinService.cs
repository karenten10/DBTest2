using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class BulletinService
    {
        private readonly InspectionDBContext context;

        public BulletinService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Bulletin>> GetAsync()
        {
            return Task.FromResult(context.Bulletin
                .OrderByDescending(x => x.AnnounceTime)
                .AsNoTracking().AsQueryable());
        }

        public async Task<Bulletin> GetAsync(int id)
        {
            Bulletin item = await context.Bulletin.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Bulletin paraObject)
        {
            await context.Bulletin.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<Bulletin> UpdateAsync(Bulletin paraObject)
        {
            Bulletin item = await context.Bulletin
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<Bulletin>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<Bulletin> DeleteAsync(Bulletin paraObject)
        {
            await Task.Delay(100);
            Bulletin item = await context.Bulletin.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.Bulletin.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
