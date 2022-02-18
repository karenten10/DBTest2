using Database.Models.Models;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class FormReportService
    {
        private readonly InspectionDBContext context;

        public FormReportService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<FormReport>> GetAsync()
        {
            return Task.FromResult(context.FormReport
                .AsNoTracking().AsQueryable());
        }

        public async Task<FormReport> GetAsync(int id)
        {
            var formReport = await context.FormReport.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

            return formReport;
        }

        public async Task AddAsync(FormReport paraObject)
        {
            await context.FormReport.AddAsync(paraObject);
            await context.SaveChangesAsync();
            context.CleanAllEFCoreTracking<FormReport>();
            return;
        }

        public async Task<FormReport> UpdateAsync(FormReport paraObject)
        {
            FormReport item = await context.FormReport
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<FormReport>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FormReport>();
                return paraObject;
            }
        }

        public async Task<FormReport> DeleteAsync(FormReport paraObject)
        {
            await Task.Delay(100);
            FormReport item = await context.FormReport.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.FormReport.Remove(item);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<FormReport>();
                return item;
            }
        }

        public async Task<List<FormReport>> GetFormReportBy設備巡檢Async(List<long?> loginDepts)
        {
            var result = await context.FormReport
                        //.Where(x=>x.Code == "1")
                        .Where(x=>(loginDepts == null || (loginDepts != null && loginDepts.Contains(x.DepartmentId))) )
                        .AsNoTracking()
                        .ToListAsync();

            return result;
        }

       

    }
}
