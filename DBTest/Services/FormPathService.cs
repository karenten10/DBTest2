using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class FormPathService
    {
        private readonly InspectionDBContext context;

        public FormPathService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<FormPath>> GetAsync()
        {
            return Task.FromResult(context.FormPath
                .Include(x => x.PatrolPath)
                .Include(x => x.FormReport)
                .AsNoTracking().AsQueryable());
        }

        public async Task<FormPath> GetAsync(int id)
        {
            FormPath item = await context.FormPath
                .Include(x => x.PatrolPath)
                .Include(x => x.FormReport)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<FormPath> GetAsyncByReportId(int reportId)
        {
            FormPath item = await context.FormPath
                .Include(x => x.PatrolPath)
                .Include(x => x.FormReport)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FormReportId == reportId);
            return item;
        }

        public async Task AddAsync(FormPath paraObject)
        {
            await context.FormPath.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<FormPath>();
            return;
        }

        public async Task<FormPath> UpdateAsync(FormPath paraObject)
        {
            FormPath item = await context.FormPath
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<FormPath>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FormPath>();
                return paraObject;
            }
        }

        public async Task<FormPath> DeleteAsync(FormPath paraObject)
        {
            await Task.Delay(100);
            FormPath item = await context.FormPath.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.FormPath.Remove(item);
                await context.SaveChangesAsync();
                try
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                context.CleanAllEFCoreTracking<FormPath>();
                return item;
            }
        }
    }
}
