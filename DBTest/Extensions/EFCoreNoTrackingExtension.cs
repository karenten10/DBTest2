using Database.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Extensions
{
    public static class EFCoreNoTrackingExtension
    {
        public static void CleanAllEFCoreTracking<T>(this InspectionDBContext dbContext) where T : class
        {
            #region 解除快取所有的紀錄
            var locals = dbContext.Set<T>()
                .Local;
            foreach (var localItem in locals)
            {
                dbContext.Entry(localItem).State = EntityState.Detached;
                //dbContext.Set<T>().Local.Remove(localItem);
            }
            #endregion
        }
    }
}
