using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class WorkTypeService
    {
        private readonly InspectionDBContext context;

        public WorkTypeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<WorkType>> GetAsync()
        {
            return Task.FromResult(context.WorkType
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<WorkType> GetAsync(int id)
        {
            WorkType item = await context.WorkType.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<WorkType>> GetAllAsync()
        {
            return await context.WorkType
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task AddAsync(WorkType paraObject, int?[] people)
        {
            try
            {
                await context.WorkType.AddAsync(paraObject);
                await context.SaveChangesAsync();

                if (people != null)
                {
                    foreach (var person in people)
                    {
                        await context.WorkTypePeople.AddAsync(new WorkTypePeople
                        {
                            PersonId = person.Value,
                            WorkTypeId = paraObject.Id
                        });
                        await context.SaveChangesAsync();
                    }
                    context.CleanAllEFCoreTracking<WorkTypePeople>();
                }
            }
            catch (Exception) { }

            return;
        }

        public async Task<WorkType> UpdateAsync(WorkType paraObject, int?[] people)
        {
            WorkType item = await context.WorkType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<WorkType>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;
                // save 
                await context.SaveChangesAsync();

                var findAllPeople = context.WorkTypePeople
                    .AsNoTracking()
                    .Where(x => x.WorkTypeId == paraObject.Id);
                context.WorkTypePeople.RemoveRange(findAllPeople);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<WorkTypePeople>();

                if (people != null)
                {
                    foreach (var person in people)
                    {
                        await context.WorkTypePeople.AddAsync(new WorkTypePeople
                        {
                            PersonId = person.Value,
                            WorkTypeId = paraObject.Id
                        });
                        await context.SaveChangesAsync();
                    }
                    context.CleanAllEFCoreTracking<WorkTypePeople>();
                }

                return paraObject;
            }
        }

        public async Task<WorkType> DeleteAsync(WorkType paraObject)
        {
            await Task.Delay(100);
            WorkType item = await context.WorkType.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.WorkType.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<int?[]> GetWorkingDayAsync(int id)
        {
            var result = await context.WorkType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result?.WorkingDay != null)
            {
                var splitStr = result.WorkingDay.Split('|');
                if (splitStr.Length > 0)
                {
                    int?[] array = new int?[splitStr.Length];
                    for (int i = 0; i < splitStr.Length; i++)
                        array[i] = int.Parse(splitStr[i]);
                    return array;
                }
            }
            return null;
        }

        public async Task<int?> GetIdByNameAsync(string name)
        {
            var result = await context.WorkType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.JobName == name);

            return result != null ? result.Id : null;
        }

        public async Task<string> GetNameByIdAsync(int? id)
        {
            var result = await context.WorkType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result != null ? result.JobName : null;
        }

        public async Task<int?[]> GetWorkTypePeopleAsync(int id)
        {
            return await context.WorkTypePeople
                .AsNoTracking()
                .Where(x => x.WorkTypeId == id)
                .Select(x => (int?)x.PersonId)
                .ToArrayAsync();
        }
    }
}
