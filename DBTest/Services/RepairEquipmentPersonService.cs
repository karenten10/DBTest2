using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class RepairEquipmentPersonService
    {
        private readonly InspectionDBContext context;

        public RepairEquipmentPersonService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<RepairEquipmentNPersonAdapterModel>> GetAsync()
        {
            context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();
            IQueryable<RepairEquipmentNPersonAdapterModel> RepairEquipmentNPersonAdapterModels = null;

            RepairEquipmentNPersonAdapterModels = from a in context.RepairEquipmentNPerson
                                                  join b in context.Person on a.PersonId equals b.Id
                                                  join c in context.RepairEquipment on a.RepairEquipmentId equals c.Id
                                                  select new RepairEquipmentNPersonAdapterModel
                                                  {
                                                      PersonId = a.PersonId,
                                                      PersonName = b.Name,
                                                      RepairEquipmentId = a.RepairEquipmentId,
                                                      RepairEquipmentName = c.Name
                                                  };

            context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();



            return Task.FromResult(RepairEquipmentNPersonAdapterModels);
        }
        public Task<IQueryable<RepairEquipmentNPersonAdapterModel>> GetByMasterAsync(int paraObj)
        {
            context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();
            IQueryable<RepairEquipmentNPersonAdapterModel> RepairEquipmentNPersonAdapterModels = null;

            RepairEquipmentNPersonAdapterModels = from a in context.RepairEquipmentNPerson
                                                  join b in context.Person on a.PersonId equals b.Id
                                                  join c in context.RepairEquipment on a.RepairEquipmentId equals c.Id
                                                  where a.RepairEquipmentId == paraObj
                                                  select new RepairEquipmentNPersonAdapterModel
                                                  {
                                                      Id = a.Id,
                                                      PersonId = a.PersonId,
                                                      PersonName = b.Name,
                                                      RepairEquipmentId = a.RepairEquipmentId,
                                                      RepairEquipmentName = c.Name
                                                  };

            context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();



            return Task.FromResult(RepairEquipmentNPersonAdapterModels);
        }

        public async Task<RepairEquipmentNPerson> GetAsync(int id)
        {
            RepairEquipmentNPerson item = await context.RepairEquipmentNPerson.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(RepairEquipmentNPerson paraObject)
        {
            await context.RepairEquipmentNPerson.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<RepairEquipmentNPerson> UpdateAsync(RepairEquipmentNPerson paraObject)
        {
            RepairEquipmentNPerson item = await context.RepairEquipmentNPerson
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairEquipmentNPerson> DeleteAsync(RepairEquipmentNPerson paraObject)
        {
            RepairEquipmentNPerson item = await context.RepairEquipmentNPerson.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.RepairEquipmentNPerson.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
    }
}
