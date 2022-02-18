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
    public class EquipmentComponentService
    {
        private readonly InspectionDBContext context;

        public EquipmentComponentService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentComponent>> GetAsync()
        {
            return Task.FromResult(context.EquipmentComponent
                .OrderByDescending(x => x.EquipmentId)
                .AsNoTracking().AsQueryable());
        }

        public async Task<EquipmentComponent> GetAsync(int id)
        {
            EquipmentComponent item = await context.EquipmentComponent.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public Task<IQueryable<EquipmentComponent>> GetByHeaderIDAsync(int id)
        {
            return Task.FromResult(context.EquipmentComponent
                .AsNoTracking()
                // INNER JOIN [Department] AS [d] ON [t].[DepartmentID] = [d].[DepartmentID]
                .Include(x => x.Equipment)
                .Where(x => x.EquipmentId == id) // 撈出該科系底下的全部課程
                .AsQueryable());
        }

        public async Task AddAsync(EquipmentComponent paraObject)
        {
            await context.EquipmentComponent.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<EquipmentComponent> UpdateAsync(EquipmentComponent paraObject)
        {
            EquipmentComponent item = await context.EquipmentComponent
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentComponent>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentPart> DeleteAsync(EquipmentPartAdapterModel paraObject)
        {
            await Task.Delay(100);
            EquipmentPart item = await context.EquipmentPart
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.PartId == paraObject.OldPartId && x.EquipmentId == paraObject.EquipmentId);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.EquipmentPart.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task AddAsync(EquipmentPartAdapterModel paraObject)
        {
            var checkeEquipment = await context.EquipmentPart
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PartId == paraObject.PartId && x.EquipmentId == paraObject.EquipmentId);


            if(checkeEquipment==null)
            {
                EquipmentPart equipmentPart = new EquipmentPart();
                equipmentPart.EquipmentId = paraObject.EquipmentId;
                equipmentPart.PartId = paraObject.PartId;
                equipmentPart.PositionName = paraObject.PositionName;
                equipmentPart.Qty = paraObject.Qty;
                await context.EquipmentPart.AddAsync(equipmentPart);
                await context.SaveChangesAsync();

            }

            return;
        }

        public async Task<EquipmentPart> UpdateAsync(EquipmentPartAdapterModel paraObject)
        {
            var checkeEquipment = await context.EquipmentPart
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.PartId == paraObject.PartId && x.EquipmentId == paraObject.EquipmentId);

            if(checkeEquipment != null)
            {
                EquipmentPart item = await context.EquipmentPart
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.PartId == paraObject.OldPartId && x.EquipmentId == paraObject.EquipmentId);

                if (item == null)
                {
                    return null;
                }
                else
                {
                    #region 在這裡需要設定需要解除快取紀錄
                    context.CleanAllEFCoreTracking<EquipmentPart>();
                    #endregion
                    // set Modified flag in your entry
                    item.EquipmentId = paraObject.EquipmentId;
                    item.PartId = paraObject.PartId;
                    item.PositionName = paraObject.PositionName;
                    item.Qty = paraObject.Qty;
                    context.Entry(item).State = EntityState.Modified;

                    // save 
                    await context.SaveChangesAsync();
                    return item;
                }
            }

            return null;
        }
    }
}
