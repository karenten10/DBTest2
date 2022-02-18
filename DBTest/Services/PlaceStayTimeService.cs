using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PlaceStayTimeService
    {
        private readonly InspectionDBContext context;

        public PlaceStayTimeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PlaceStayTime>> GetAsync()
        {
            return Task.FromResult(context.PlaceStayTime
                .OrderByDescending(x => x.Id)
                .AsNoTracking().AsQueryable());
        }

        public async Task<List<PlaceStayTime>> GetByPeriodIdAsync(int periodId)
        {
            var result = new List<PlaceStayTime>();

            var queryData = await context.PlaceStayTime
                .OrderByDescending(x => x.PatroPlaceId)
                .Include(x => x.PatroPlace)
                .Where(x => x.PatrolPathPeriodId == periodId) // 撈出該時段全部資料
                .AsNoTracking().ToListAsync();

            if(queryData != null && queryData.Count()>0)
            {
                result = queryData;
            }

            return result;
        }

        public async Task<PlaceStayTime> GetAsync(int id)
        {
            PlaceStayTime item = await context.PlaceStayTime.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        /// <summary>檢查資料是否存在</summary>
        /// <param name="periodId">時段</param>
        /// <param name="placeId">巡檢點</param>
        /// <returns>true：已存在</returns>
        public async Task<bool> CheckStayIsExistAsync(long Id, int periodId, int placeId)
        {
            var result = await context.PlaceStayTime
                .Where(x => (Id <= 0 && x.PatrolPathPeriodId == periodId && x.PatroPlaceId == placeId) // 新增
                    || (x.Id != Id && x.PatrolPathPeriodId == periodId && x.PatroPlaceId == placeId)) // 修改
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result != null ? true : false;
        }

        public async Task AddAsync(PlaceStayTime paraObject)
        {
            await context.PlaceStayTime.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PlaceStayTime> UpdateAsync(PlaceStayTime paraObject)
        {
            PlaceStayTime item = await context.PlaceStayTime
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
                    context.CleanAllEFCoreTracking<PlaceStayTime>();
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

        public async Task<PlaceStayTime> DeleteAsync(PlaceStayTime paraObject)
        {
            await Task.Delay(100);
            PlaceStayTime item = await context.PlaceStayTime.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.PlaceStayTime.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
