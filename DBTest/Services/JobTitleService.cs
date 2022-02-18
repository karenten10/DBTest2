using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class JobTitleService
    {
        private readonly InspectionDBContext context;

        public JobTitleService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<JobTitle>> GetAsync()
        {
            return Task.FromResult(context.JobTitle
                .AsNoTracking().AsQueryable());
        }

        public async Task AddAsync(JobTitle paraObject)
        {
            await context.JobTitle.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<JobTitle> UpdateAsync(JobTitle paraObject)
        {
            JobTitle item = await context.JobTitle
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<JobTitle>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<JobTitle> DeleteAsync(JobTitle paraObject)
        {
            await Task.Delay(100);
            JobTitle item = await context.JobTitle.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {


                context.JobTitle.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<List<JobTitle>> GetJobTitleAllAsync()
        {
            return await context.JobTitle
             .AsNoTracking()
             .ToListAsync();
        }

        /// <summary>檢查職稱是否存在</summary>
        public async Task<bool> CheckJobTitleIsExistAsync(int id, string jobTitle)
        {
            var result = await context.JobTitle
                .Where(x => x.Id != id && x.Name == jobTitle)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result != null ? true : false;
        }

        public async Task<string> GetJobTitleByPeopleAsync(int?[] ids)
        {
            string name = string.Empty;

            if (ids != null)
            {
                var result = await context.Person
                    .Include(x => x.JobTitle)
                    .AsNoTracking()
                    .Where(x => ids.Contains(x.Id) && x.JobTitle != null)
                    .Select(x => x.JobTitle.Name)
                    .Distinct()
                    .ToListAsync();

                foreach (var item in result)
                    name += $"{item}、";

                if (name.Length > 0)
                    name = name.Substring(0, name.Length - 1);
            }

            return name;
        }
    }
}
