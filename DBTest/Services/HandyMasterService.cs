using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class HandyMasterService
    {
        private readonly InspectionDBContext context;

        public HandyMasterService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<HandyMaster>> GetAsync()
        {
            return Task.FromResult(context.HandyMaster
                .OrderByDescending(x => x.Id)
                .AsNoTracking().AsQueryable());
        }

        public async Task<HandyMaster> GetAsync(int id)
        {
            HandyMaster item = await context.HandyMaster.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(HandyMaster paraObject)
        {
            await context.HandyMaster.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<HandyMaster> UpdateAsync(HandyMaster paraObject)
        {
            HandyMaster item = await context.HandyMaster
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
                    context.CleanAllEFCoreTracking<HandyMaster>();
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

        public async Task<HandyMaster> DeleteAsync(HandyMaster paraObject)
        {
            await Task.Delay(100);
            HandyMaster item = await context.HandyMaster.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.HandyMaster.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
