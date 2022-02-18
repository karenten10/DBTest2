using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class EquipmentExamService
    {
        private readonly InspectionDBContext context;

        public EquipmentExamService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<EquipmentExam>> GetAsync()
        {
            return Task.FromResult(context.EquipmentExam
                .Include(x => x.Equipment)
                .Include(x => x.PatrolPath)
                .AsNoTracking().AsQueryable());
        }
        public Task<IQueryable<EquipmentExam>> GetByMasterAsync(int paraObj)
        {
            var result = Task.FromResult(context.EquipmentExam
               .Include(x => x.PatrolPath)
               .Include(x => x.Equipment)
               .Where(x => x.PatrolPathId == paraObj).AsNoTracking().AsQueryable());
            return result;
        }

        public async Task<EquipmentExam> GetAsync(int id)
        {
            EquipmentExam item = await context.EquipmentExam.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(EquipmentExam paraObject)
        {
            await context.EquipmentExam.AddAsync(paraObject);
            await context.SaveChangesAsync();
            var item = await context.EquipmentExam.FirstOrDefaultAsync(x =>
            x.EquipmentId == paraObject.EquipmentId && x.PatrolPathId == paraObject.PatrolPathId);
            if (item != null)
            {
                var itemsForEquipmentExamItem = await context.EquipmentExamItem.Where(x =>
                x.EquipmentId == item.EquipmentId)
                    .OrderBy(x=>x.OrderId).ToListAsync();
                foreach (var fooItem in itemsForEquipmentExamItem)
                {
                    VirtualEquipmentExamItem virtualExamItem = new VirtualEquipmentExamItem()
                    {
                        EquipmentExamId = item.Id,
                        EquipmentId = fooItem.EquipmentId,
                        EquipmentExamItemId = fooItem.Id,
                        Status = "N",
                        OrderId = (int)fooItem.OrderId,
                        Name = fooItem.Name,
                    };
                    context.VirtualEquipmentExamItem.Add(virtualExamItem);
                }
                await context.SaveChangesAsync();
            }
            return;
        }

        public async Task<EquipmentExam> UpdateAsync(EquipmentExam paraObject)
        {
            EquipmentExam item = await context.EquipmentExam
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<EquipmentExam>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<EquipmentExam> DeleteAsync(EquipmentExam paraObject)
        {
            var items = await context.VirtualEquipmentExamItem.Where(x =>
            x.EquipmentId == paraObject.EquipmentId).AsNoTracking().ToListAsync();
            foreach (var fooItem in items)
            {
                #region 在這裡需要設定需要更新的紀錄欄位值
                // 
                var local = context.Set<VirtualEquipmentExamItem>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(fooItem.Id));

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    context.Entry(local).State = EntityState.Detached;
                }
                // set Modified flag in your entry
                //context.Entry(paraObject).State = EntityState.Modified;
                #endregion

                context.VirtualEquipmentExamItem.Remove(fooItem);
                await context.SaveChangesAsync();
            }
            EquipmentExam item = await context.EquipmentExam.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    context.EquipmentExam.Remove(item);
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
