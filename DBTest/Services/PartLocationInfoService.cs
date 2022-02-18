using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspectionBlazor.DataModels;
using InspectionShare.Helpers;
using InspectionBlazor.AdapterModels;
namespace InspectionBlazor.Services
{
    public class PartLocationInfoService
    {
        private readonly InspectionDBContext context;

        public PartLocationInfoService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PartLocationInfo>> GetAsync()
        {
            return Task.FromResult(context.PartLocationInfo
                .AsNoTracking().AsQueryable());
        }
        public async Task AddAsync(PartLocationInfo paraObject)
        {
            try
            {
                await context.PartLocationInfo.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }
          
            return;
        }

        public async Task<PartLocationInfo> UpdateAsync(PartLocationInfo paraObject)
        {
            PartLocationInfo item = await context.PartLocationInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LocationId == paraObject.LocationId);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PartLocationInfo>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<string> DeleteAsync(PartLocationInfo paraObject)
        {
            await Task.Delay(100);
            string result = "";

            try
            {
                PartLocationInfo item = await context.PartLocationInfo.FirstOrDefaultAsync(x => x.LocationId == paraObject.LocationId);
                if (item == null)
                {
                    result = "找不到該筆資料";
                }
                else
                {
                    context.PartLocationInfo.Remove(item);
                    await context.SaveChangesAsync();
                    result = "刪除成功";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<List<PartLocationInfo>> GetPartLocationInfoAsync()
        {
            var result = await context.PartLocationInfo
                        .AsNoTracking()
                        .ToListAsync();

            return result;
        }

    }
}
