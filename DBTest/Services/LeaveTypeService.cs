using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class LeaveTypeService
    {
        private readonly InspectionDBContext context;

        public LeaveTypeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<LeaveType>> GetAsync()
        {
            return Task.FromResult(context.LeaveType
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<List<LeaveType>> GetAllAsync()
        {
            return await context.LeaveType
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            var result = await context.LeaveType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LeaveName == name);

            return result != null ? result.Id : null;
        }

        public async Task<string> GetNameByIdAsync(int? id)
        {
            var result = await context.LeaveType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.LeaveName : null;
        }

        public async Task AddAsync(LeaveType paraObject)
        {
            try
            {
                await context.LeaveType.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<LeaveType> UpdateAsync(LeaveType paraObject)
        {
            LeaveType item = await context.LeaveType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<LeaveType>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<LeaveType> DeleteAsync(LeaveType paraObject)
        {
            await Task.Delay(100);
            LeaveType item = await context.LeaveType.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.LeaveType.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
