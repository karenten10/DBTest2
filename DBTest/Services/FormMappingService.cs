using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class FormMappingService
    {
        private readonly InspectionDBContext context;

        public FormMappingService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<FormMapping>> GetAsync()
        {
            return Task.FromResult(context.FormMapping
                .Include(x => x.Equipment)
                .Include(x => x.FormReport)
                .AsNoTracking().AsQueryable());
        }

        public async Task<FormMapping> GetAsync(int id)
        {
            FormMapping item = await context.FormMapping
                .Include(x => x.Equipment)
                .Include(x => x.FormReport)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(FormMapping paraObject)
        {
            await context.FormMapping.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<FormMapping>();
            return;
        }

        public async Task<FormMapping> UpdateAsync(FormMapping paraObject)
        {
            FormMapping item = await context.FormMapping
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<FormMapping>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FormMapping>();
                return paraObject;
            }
        }

        public async Task<FormMapping> DeleteAsync(FormMapping paraObject)
        {
            await Task.Delay(100);
            FormMapping item = await context.FormMapping.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.FormMapping.Remove(item);
                await context.SaveChangesAsync();
                try
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                context.CleanAllEFCoreTracking<FormMapping>();
                return item;
            }
        }
    }
}
