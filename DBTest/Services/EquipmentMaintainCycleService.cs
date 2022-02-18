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
    public class EquipmentMaintainCycleService
    {
        private readonly InspectionDBContext context;

        public EquipmentMaintainCycleService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<EquipmentMaintainCycleAdapterModel>> GetAsync()
        {
            List<EquipmentMaintainCycleAdapterModel> equipmentMaintainCycleAdapterModels = new List<EquipmentMaintainCycleAdapterModel>();

            var result = await context.EquipmentBasic
                .AsNoTracking()
                .Include(x => x.Equipment)
                .ThenInclude(x => x.Section)
                .Where(x => x.LastMaintainDate != null &&
                    !(x.MaintainCycleYear == null && x.MaintainCycleMonth == null && x.MaintainCycleDay == null))
                .ToListAsync();

            foreach (var item in result)
            {
                DateTime needMaintainDate = item.LastMaintainDate.Value;
                string cycle = "";
                if (item.MaintainCycleYear.HasValue)
                {
                    cycle += $"{item.MaintainCycleYear.Value}年";
                    needMaintainDate = needMaintainDate.AddYears(item.MaintainCycleYear.Value);
                }

                if (item.MaintainCycleMonth.HasValue)
                {
                    cycle += $"{item.MaintainCycleMonth.Value}月";
                    needMaintainDate = needMaintainDate.AddMonths(item.MaintainCycleMonth.Value);
                }

                if (item.MaintainCycleDay.HasValue)
                {
                    cycle += $"{item.MaintainCycleDay.Value}日";
                    needMaintainDate = needMaintainDate.AddDays(item.MaintainCycleDay.Value);
                }

                if (DateTime.Now > needMaintainDate)
                {
                    equipmentMaintainCycleAdapterModels.Add(new EquipmentMaintainCycleAdapterModel
                    {
                        EquipmentBasicId = item.Id,
                        EquipmentId = item.EquipmentId,
                        EquipmentName = item.Equipment.EquipmentName,
                        SectionName = item.Equipment.Section.Name,
                        LastMaintainDate = item.LastMaintainDate,
                        OverdueDay = (int)Math.Ceiling(new TimeSpan(DateTime.Now.Ticks - needMaintainDate.Ticks).TotalDays),
                        Cycle = cycle,
                        Spec = item.Spec,
                        ButtonDisabled = false,
                        ButtonContent = "更換後更新"
                    });
                }
            }

            return equipmentMaintainCycleAdapterModels
                .OrderByDescending(x => x.OverdueDay)
                .ThenBy(x => x.SectionName)
                .ThenBy(x => x.EquipmentName)
                .AsQueryable();
        }

        public async Task UpdateLastMaintainDateAsync(EquipmentMaintainCycleAdapterModel item)
        {
            var findRecord = await context.EquipmentBasic
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == item.EquipmentBasicId);

            if (findRecord != null)
            {
                findRecord.LastMaintainDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                context.CleanAllEFCoreTracking<EquipmentBasic>();
                context.Entry(findRecord).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
