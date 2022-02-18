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
    public class RepairEquipmentRepairRemakerService
    {
        private readonly InspectionDBContext context;

        public RepairEquipmentRepairRemakerService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<RepairEquipmentNRepairRemarkAdapterModel>> GetAsync()
        {
            context.CleanAllEFCoreTracking<RepairEquipmentNRepairRemark>();
            IQueryable<RepairEquipmentNRepairRemarkAdapterModel> RepairEquipmentNRepairRemarkAdapterModels = null;

            RepairEquipmentNRepairRemarkAdapterModels = from a in context.RepairEquipmentNRepairRemark
                                                        join b in context.RepairRemaker on a.RepairRemakerId equals b.Id
                                                  join c in context.RepairEquipment on a.RepairEquipmentId equals c.Id
                                                  select new RepairEquipmentNRepairRemarkAdapterModel
                                                  {
                                                      Id = a.Id,
                                                      RepairRemakerId = a.RepairRemakerId,
                                                      RepairRemakerName = b.RepairRemakerName,
                                                      RepairEquipmentId = a.RepairEquipmentId,
                                                      RepairEquipmentName = c.Name
                                                  };

            context.CleanAllEFCoreTracking<RepairEquipmentNRepairRemark>();



            return Task.FromResult(RepairEquipmentNRepairRemarkAdapterModels);
        }
        public Task<IQueryable<RepairEquipmentNRepairRemarkAdapterModel>> GetByMasterAsync(int paraObj)
        {
            context.CleanAllEFCoreTracking<RepairEquipmentNRepairRemark>();
            IQueryable<RepairEquipmentNRepairRemarkAdapterModel> RepairEquipmentNRepairRemarkAdapterModels = null;

            RepairEquipmentNRepairRemarkAdapterModels = from a in context.RepairEquipmentNRepairRemark
                                                        join b in context.RepairRemaker on a.RepairRemakerId equals b.Id
                                                        join c in context.RepairEquipment on a.RepairEquipmentId equals c.Id
                                                        where a.RepairEquipmentId == paraObj
                                                        select new RepairEquipmentNRepairRemarkAdapterModel
                                                        {
                                                            Id = a.Id,
                                                            RepairRemakerId = a.RepairRemakerId,
                                                            RepairRemakerName = b.RepairRemakerName,
                                                            RepairEquipmentId = a.RepairEquipmentId,
                                                            RepairEquipmentName = c.Name
                                                        };

            context.CleanAllEFCoreTracking<RepairEquipmentNRepairRemark>();



            return Task.FromResult(RepairEquipmentNRepairRemarkAdapterModels);
        }

        public async Task<RepairEquipmentNRepairRemark> GetAsync(int id)
        {
            RepairEquipmentNRepairRemark item = await context.RepairEquipmentNRepairRemark.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(RepairEquipmentNRepairRemark paraObject)
        {
            await context.RepairEquipmentNRepairRemark.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<RepairEquipmentNRepairRemark> UpdateAsync(RepairEquipmentNRepairRemark paraObject)
        {
            RepairEquipmentNRepairRemark item = await context.RepairEquipmentNRepairRemark
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairEquipmentNRepairRemark>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairEquipmentNRepairRemark> DeleteAsync(RepairEquipmentNRepairRemark paraObject)
        {
            RepairEquipmentNRepairRemark item = await context.RepairEquipmentNRepairRemark.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.RepairEquipmentNRepairRemark.Remove(item);
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
