using Dapper;
using Database.Models.Models;
using InspectionShare.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PadSettingsService
    {
        InspectionDBContext context;

        public PadSettingsService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<List<PadSettings>> GetListAsync()
        {
            List<PadSettings> result = await context.PadSettings
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task UpdateAsync(string _歷史查詢天數, string _提前巡檢時數)
        {
            var 歷史查詢天數 = await context.PadSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Key == "歷史查詢天數");

            if (歷史查詢天數 != null)
            {
                歷史查詢天數.Value = _歷史查詢天數;
                context.PadSettings.Update(歷史查詢天數);
            }

            var 提前巡檢時數 = await context.PadSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Key == "提前巡檢時數");

            if (提前巡檢時數 != null)
            {
                提前巡檢時數.Value = _提前巡檢時數;
                context.PadSettings.Update(提前巡檢時數);
            }

            await context.SaveChangesAsync();
        }
    }
}
