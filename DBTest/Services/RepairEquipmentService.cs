using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class RepairEquipmentService
    {
        private readonly InspectionDBContext context;

        public RepairEquipmentService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<RepairEquipment>> GetAsync()
        {
            return Task.FromResult(context.RepairEquipment
                .AsNoTracking().AsQueryable());
        }

        public async Task<RepairEquipment> GetAsync(int id)
        {
            RepairEquipment item = await context.RepairEquipment.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(RepairEquipment paraObject)
        {
            await context.RepairEquipment.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<RepairEquipment> UpdateAsync(RepairEquipment paraObject)
        {
            RepairEquipment item = await context.RepairEquipment
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairEquipment>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairEquipment> DeleteAsync(RepairEquipment paraObject)
        {
            await Task.Delay(100);
            RepairEquipment item = await context.RepairEquipment.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.RepairEquipment.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
        public Task<IQueryable<RepairEquipment>> GetByMasterAsync(int paraObj)
        {
            return Task.FromResult(context.RepairEquipment
                .Where(x => x.RepairEquipmentGroupId == paraObj).AsNoTracking().AsQueryable());
        }


    }
}
