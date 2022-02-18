using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class UpdateFileService
    {
        private readonly InspectionDBContext context;

        public UpdateFileService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<UpdateFile>> GetAsync()
        {
            return Task.FromResult(context.UpdateFile
               .AsNoTracking()
               .OrderByDescending(x => x.UpdateTime)
               .AsQueryable());
        }

        public async Task<bool> IsDuplicate(string fileName)
        {
            var result = await context.UpdateFile
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FileName.Contains(fileName));

            return result != null ? true : false;
        }

        public async Task<string> GetLastVersionFile()
        {
            var result = await context.UpdateFile
                .AsNoTracking()
                .OrderByDescending(x => x.FileName)
                .FirstOrDefaultAsync();

            return result != null ? result.FileName : string.Empty;
        }

        public async Task<UpdateFile> GetAsync(int id)
        {
            UpdateFile item = await context.UpdateFile.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(UpdateFile paraObject)
        {
            try
            {
                await context.UpdateFile.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<UpdateFile> UpdateAsync(UpdateFile paraObject)
        {
            UpdateFile item = await context.UpdateFile
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<UpdateFile>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<UpdateFile> DeleteAsync(UpdateFile paraObject)
        {
            await Task.Delay(100);
            UpdateFile item = await context.UpdateFile.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.UpdateFile.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
