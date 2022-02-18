using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services {
    public class MobileDeviceService {
        private readonly InspectionDBContext context;

        public MobileDeviceService(InspectionDBContext context) {
            this.context = context;
        }

        public Task<IQueryable<MobileDevice>> GetAsync() {
            return Task.FromResult(context.MobileDevice
                .AsNoTracking().AsQueryable());
        }

        public async Task<MobileDevice> GetAsync(int id) {
            MobileDevice item = await context.MobileDevice
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(MobileDevice paraObject) {
            await context.MobileDevice.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<MobileDevice> UpdateAsync(MobileDevice paraObject) {
            MobileDevice item = await context.MobileDevice
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null) {
                return null;
            } else {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<MobileDevice>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<MobileDevice> DeleteAsync(MobileDevice paraObject) {
            await Task.Delay(100);
            MobileDevice item = await context.MobileDevice.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null) {
                return null;
            } else {


                context.MobileDevice.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
    }
}
