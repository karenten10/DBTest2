using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PadMessageService
    {
        private readonly InspectionDBContext context;

        public PadMessageService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PadMessage>> GetAsync()
        {
            return Task.FromResult(context.PadMessage
                .OrderByDescending(x => x.Id)
                .Include(x => x.Department)
                .AsNoTracking().AsQueryable());
        }

        public async Task AddAsync(PadMessage paraObject)
        {
            await context.PadMessage.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PadMessage> UpdateAsync(PadMessage paraObject)
        {
            PadMessage item = await context.PadMessage
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);

            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    #region 在這裡需要設定需要解除快取紀錄
                    context.CleanAllEFCoreTracking<PadMessage>();
                    #endregion
                    // set Modified flag in your entry
                    context.Entry(paraObject).State = EntityState.Modified;

                    // save 
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string ex = e.ToString();
                }
                
                return paraObject;
            }
        }

        public async Task<PadMessage> DeleteAsync(PadMessage paraObject)
        {
            await Task.Delay(100);
            PadMessage item = await context.PadMessage.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.PadMessage.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
