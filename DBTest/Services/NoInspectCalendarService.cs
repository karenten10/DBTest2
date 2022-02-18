using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class NoInspectCalendarService
    {
        private readonly InspectionDBContext context;

        public NoInspectCalendarService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<NoInspectCalendar>> GetAsync()
        {
            return Task.FromResult(context.NoInspectCalendar
               .AsNoTracking()
               .Include(x=>x.NoInspectCalendarNpatrolPlace)
               .AsQueryable());
        }
        public Task<IQueryable<NoInspectCalendar>> GetAsyncByConditionDataModel(NoInspectionCalendarConditionDataModel ConditionDataModel)
        {
            return Task.FromResult(context.NoInspectCalendar
               .AsNoTracking()
               .Include(x => x.NoInspectCalendarNpatrolPlace)
               .Where(x=>x.PatrolPathId == ConditionDataModel.PatrolPathId)
               .OrderByDescending(x=>x.EndTime)
               .AsQueryable());
        }

        public async Task<NoInspectCalendar> GetAsync(int id)
        {
            NoInspectCalendar item = await context.NoInspectCalendar.AsNoTracking()
                .Include(x=>x.NoInspectCalendarNpatrolPlace)
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(NoInspectCalendar paraObject,List<int> checkedPlace)
        {
            try
            {
                await context.NoInspectCalendar.AddAsync(paraObject);
                await context.SaveChangesAsync();
                //加入 NoInspectCalendarNPlace
                await AddNoInspectCalendarNPlace(paraObject, checkedPlace);
            }
            catch (Exception) { }

            return;
        }

        private async Task AddNoInspectCalendarNPlace(NoInspectCalendar paraObject, List<int> checkedPlace)
        {
            if (checkedPlace.Any())
            {

                foreach (var item in checkedPlace)
                {
                    NoInspectCalendarNpatrolPlace _noInspectCalendarNpatrolPlace = new NoInspectCalendarNpatrolPlace();
                    _noInspectCalendarNpatrolPlace.NoInspectCalendarId = paraObject.Id;
                    _noInspectCalendarNpatrolPlace.PatrolPlaceId = item;
                    await context.NoInspectCalendarNpatrolPlace.AddAsync(_noInspectCalendarNpatrolPlace);
                    await context.SaveChangesAsync();

                }

            }
        }

        public async Task<NoInspectCalendar> UpdateAsync(NoInspectCalendar paraObject, List<int> checkedPlace)
        {
            NoInspectCalendar item = await context.NoInspectCalendar
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                await DeleteNoInspectCalendarNPlace(paraObject);
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<NoInspectCalendar>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await AddNoInspectCalendarNPlace(paraObject, checkedPlace);
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        private async Task DeleteNoInspectCalendarNPlace(NoInspectCalendar paraObject)
        {
            //取得所有NoInspectCalendarNPlace 筆數
            List<NoInspectCalendarNpatrolPlace> itemPlaceList = await context.NoInspectCalendarNpatrolPlace
            .AsNoTracking()
            .Where(x => x.NoInspectCalendarId == paraObject.Id)
            .ToListAsync();

            if (itemPlaceList.Any())
            {
                foreach (var itemPlace in itemPlaceList)
                {
                    //NoInspectCalendarNPlace
                    NoInspectCalendarNpatrolPlace place = await context.NoInspectCalendarNpatrolPlace
                    .AsNoTracking()
                    .Where(x => x.Id == itemPlace.Id)
                    .FirstOrDefaultAsync();

                    context.CleanAllEFCoreTracking<NoInspectCalendarNpatrolPlace>();
                    context.Entry(place).State = EntityState.Deleted;
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task<NoInspectCalendar> DeleteAsync(NoInspectCalendar paraObject)
        {
            await Task.Delay(100);
            NoInspectCalendar item = await context.NoInspectCalendar.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                await DeleteNoInspectCalendarNPlace(paraObject);
                context.NoInspectCalendar.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
      
    }
}
