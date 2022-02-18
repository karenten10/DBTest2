using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class RepairEquipmentMasterService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<RepairEquipmentMasterService> logger;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public DepartmentService DepartmentService { get; }

        public RepairEquipmentMasterService(InspectionDBContext context, ILogger<RepairEquipmentMasterService> logger
            , AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public async Task<List<RepairEquipment>> GetAsync()
        {
            return await context.RepairEquipment.AsNoTracking().ToListAsync();
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
            RepairEquipment item = await context.RepairEquipment.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.RepairEquipment.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(RepairEquipment paraObject)
        {
            await Task.Delay(100);
            RepairEquipment curritem = await context.RepairEquipment
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<RepairEquipment>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(RepairEquipment paraObject)
        {
            await Task.Delay(100);
            RepairEquipment curritem = await context.RepairEquipment
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<RepairEquipment>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
    }
}
