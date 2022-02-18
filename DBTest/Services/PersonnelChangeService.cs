using AutoMapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PersonnelChangeService
    {
        private readonly InspectionDBContext context;

        public IMapper Mapper { get; }

        public PersonnelChangeService(InspectionDBContext context, IMapper mapper)
        {
            this.context = context;
            Mapper = mapper;
        }

        public async Task<List<PersonnelChangeAdapterModel>> GetAsync()
        {
            var result = await context.PersonnelChange
                .AsNoTracking()
                .ToListAsync();

            List<PersonnelChangeAdapterModel> personnelChangeAdapterModels = Mapper.Map<List<PersonnelChangeAdapterModel>>(result);

            return personnelChangeAdapterModels;
        }

        public async Task<PersonnelChange> GetAsync(int id)
        {
            PersonnelChange item = await context.PersonnelChange.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(PersonnelChange paraObject, int?[] ids)
        {
            await context.PersonnelChange.AddAsync(paraObject);
            await context.SaveChangesAsync();

            if (ids != null)
            {
                foreach (var item in ids)
                {
                    await context.PersonnelChangePeople.AddAsync(new PersonnelChangePeople
                    {
                        PersonnelChangeId = paraObject.Id,
                        PersonId = item.Value,
                    });
                    await context.SaveChangesAsync();
                }
            }
            return;
        }

        public async Task<PersonnelChange> UpdateAsync(PersonnelChange paraObject, int?[] ids)
        {
            PersonnelChange result = await context.PersonnelChange
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (result == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PersonnelChange>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;
                // save 
                await context.SaveChangesAsync();

                if (ids != null)
                {
                    var remove = context.PersonnelChangePeople
                        .Where(x => x.PersonnelChangeId == paraObject.Id);
                    context.PersonnelChangePeople.RemoveRange(remove);
                    await context.SaveChangesAsync();
                    context.CleanAllEFCoreTracking<PersonnelChangePeople>();

                    foreach (var item in ids)
                    {
                        await context.PersonnelChangePeople.AddAsync(new PersonnelChangePeople
                        {
                            PersonnelChangeId = paraObject.Id,
                            PersonId = item.Value,
                        });
                        await context.SaveChangesAsync();
                    }
                }

                return paraObject;
            }
        }

        public async Task<PersonnelChange> DeleteAsync(PersonnelChange paraObject)
        {
            await Task.Delay(100);
            PersonnelChange item = await context.PersonnelChange.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.PersonnelChange.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task DisableIt(PersonnelChange paraObject)
        {
            await Task.Yield();
            PersonnelChange curritem = await context.PersonnelChange
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PersonnelChange>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task EnableIt(PersonnelChange paraObject)
        {
            await Task.Yield();
            PersonnelChange curritem = await context.PersonnelChange
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<PersonnelChange>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<int?[]> GetPersonIdsAsync(int personnelChangeId)
        {
            var result = await context.PersonnelChangePeople
                .AsNoTracking()
                .Where(x => x.PersonnelChangeId == personnelChangeId)
                .ToListAsync();

            if (result.Count > 0)
            {
                int?[] ids = new int?[result.Count];
                for (int i = 0; i < result.Count(); i++)
                {
                    ids[i] = result[i].PersonId;
                }
                return ids;
            }
            else
            {
                return null;
            }
        }
    }
}
