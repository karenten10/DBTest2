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
    public class RepairEquipmentGroupService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<PatrolPathService> logger;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public DepartmentService DepartmentService { get; }

        public RepairEquipmentGroupService(InspectionDBContext context, ILogger<PatrolPathService> logger
            , AuthenticationStateProvider authenticationStateProvider, DepartmentService departmentService)
        {
            this.context = context;
            this.logger = logger;
            AuthenticationStateProvider = authenticationStateProvider;
            DepartmentService = departmentService;
        }

        public async Task<List<RepairEquipmentGroup>> GetAsync()
        {
            return await context.RepairEquipmentGroup.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(RepairEquipmentGroup paraObject)
        {
            await context.RepairEquipmentGroup.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<RepairEquipmentGroup> UpdateAsync(RepairEquipmentGroup paraObject)
        {
            RepairEquipmentGroup item = await context.RepairEquipmentGroup
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairEquipmentGroup>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairEquipmentGroup> DeleteAsync(RepairEquipmentGroup paraObject)
        {
            RepairEquipmentGroup item = await context.RepairEquipmentGroup.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.RepairEquipmentGroup.Remove(item);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return item;
            }
        }
        public async Task DisableIt(RepairEquipmentGroup paraObject)
        {
            await Task.Delay(100);
            RepairEquipmentGroup curritem = await context.RepairEquipmentGroup
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<RepairEquipmentGroup>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(RepairEquipmentGroup paraObject)
        {
            await Task.Delay(100);
            RepairEquipmentGroup curritem = await context.RepairEquipmentGroup
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<RepairEquipmentGroup>().Local)
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
