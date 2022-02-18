
using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class FireFightReportSaveService
    {
        private readonly InspectionDBContext context;

        public FireFightReportSaveService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<FireFightReportSave>> GetAsync()
        {
            return Task.FromResult(context.FireFightReportSave
                .AsNoTracking().AsQueryable());
        }

        public async Task<FireFightReportSave> GetAsync(int id)
        {
            var FireFightReportSave = await context.FireFightReportSave.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return FireFightReportSave;
        }

        public async Task AddAsync(FireFightReportSave paraObject)
        {
            await context.FireFightReportSave.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<FireFightReportSave>();
            return;
        }

        public async Task<int> AddAsyncReturnId(FireFightReportSave paraObject)
        {
            await context.FireFightReportSave.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<FireFightReportSave>();

            return paraObject.Id;
        }

        public async Task<FireFightReportSave> AddOrUpdateAsync(string queryDate, string building, string key, string value)
        {
            FireFightReportSave item = await context.FireFightReportSave
                .Where(x => x.QueryDate == queryDate
                    && x.Building == building
                    && x.Key == key)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (item == null)
            {
                var paraObject = new FireFightReportSave()
                {
                    QueryDate = queryDate,
                    Building = building,
                    Key = key,
                    Value = value
                };
                await context.FireFightReportSave.AddAsync(paraObject);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                return paraObject;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                #endregion
                // set Modified flag in your entry
                item.Value = value;
                context.Entry(item).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                return item;
            }
        }

        public async Task<FireFightReportSave> UpdateAsync(FireFightReportSave paraObject)
        {
            FireFightReportSave item = await context.FireFightReportSave
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                return paraObject;
            }
        }

        public async Task<FireFightReportSave> DeleteAsync(FireFightReportSave paraObject)
        {
            await Task.Delay(100);
            FireFightReportSave item = await context.FireFightReportSave.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.FireFightReportSave.Remove(item);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FireFightReportSave>();
                return item;
            }
        }

        public async Task<FireFightReportSave> GetReportSaveByIdsAsync(string queryDate,  string building, string key)
        {
            var result = await context.FireFightReportSave
                        .Where(x => 
                            x.QueryDate == queryDate
                            && x.Building == building
                            && x.Key == key)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

            return result;
        }



    }
}
