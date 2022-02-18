using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class CanMessageService
    {
        private readonly InspectionDBContext context;

        public CanMessageService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<CanMessage>> GetAsync()
        {
            return Task.FromResult(context.CanMessage
                .OrderByDescending(x => x.Id)
                .AsNoTracking().AsQueryable());
        }

        public async Task<CanMessage> GetAsync(int id)
        {
            CanMessage item = await context.CanMessage.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        
        public async Task<IQueryable<CanMessage>> GetByCreateIdNTypeAsync(int id, string type)
        {
            return await Task.FromResult(context.CanMessage
                .OrderByDescending(x => x.UpdateTime)
                .Include(x => x.Create)
                .Where(x => x.CreateId == id // 撈出該人員底下的全部罐頭訊息
                    && x.Type==type) // F: 表單簽核備註, I: 檢驗項目簽核備註
                .AsNoTracking().AsQueryable());
        }

        public async Task<List<CanMessage>> GetByCreateIdNTypeListAsync(int id, string type)
        {
            return await context.CanMessage
                .OrderByDescending(x => x.UpdateTime)
                .Include(x => x.Create)
                .Where(x => x.CreateId == id // 撈出該人員底下的全部罐頭訊息
                    && x.Type == type) // F: 表單簽核備註, I: 檢驗項目簽核備註
                .ToListAsync();
        }

        public async Task AddAsync(CanMessage paraObject)
        {
            await context.CanMessage.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<CanMessage> UpdateAsync(CanMessage paraObject)
        {
            CanMessage item = await context.CanMessage
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
                    context.CleanAllEFCoreTracking<CanMessage>();
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

        public async Task<CanMessage> DeleteAsync(CanMessage paraObject)
        {
            await Task.Delay(100);
            CanMessage item = await context.CanMessage.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.CanMessage.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

    }
}
