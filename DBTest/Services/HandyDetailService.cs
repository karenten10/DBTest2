using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class HandyDetailService
    {
        private readonly InspectionDBContext context;

        public HandyDetailService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<HandyDetail>> GetAsync()
        {
            return Task.FromResult(context.HandyDetail
                .OrderByDescending(x => x.Id)
                .AsNoTracking().AsQueryable());
        }

        public async Task<HandyDetail> GetAsync(int id)
        {
            HandyDetail item = await context.HandyDetail.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        
        public async Task<IQueryable<HandyDetail>> GetByManagerIDAsync(int id)
        {
            return await Task.FromResult(context.HandyDetail
                .OrderBy(x => x.Status)
                .Include(x => x.HandyMaster)
                .ThenInclude(x => x.Create)
                .Where(x => x.ManagerId == id) // 撈出該主管底下的全部表單
                .AsNoTracking().AsQueryable());
        }

        public async Task AddAsync(HandyDetail paraObject)
        {
            await context.HandyDetail.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<HandyDetail> UpdateAsync(HandyDetail paraObject)
        {
            HandyDetail item = await context.HandyDetail
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
                    context.CleanAllEFCoreTracking<HandyDetail>();
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

        public async Task<HandyDetail> DeleteAsync(HandyDetail paraObject)
        {
            await Task.Delay(100);
            HandyDetail item = await context.HandyDetail.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.HandyDetail.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
