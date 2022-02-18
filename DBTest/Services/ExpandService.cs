using Database.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class ExpandService
    {
        private readonly InspectionDBContext context;

        public ExpandService(InspectionDBContext context)
        {
            this.context = context;
        }
        
        /// <summary>取得工具備註資訊</summary>
        public async Task<List<Expand>> GetExpandByPathOrPeriodAsync(int pathId, int? periodId, string dateStr)
        {
            var result = await context.Expand
                .AsNoTracking()
                .Where(x => x.PatrolPathId == pathId &&
                    x.Name.Contains(dateStr) &&
                    ((periodId != null && x.PatrolPathPeriodId == periodId) || (periodId == null)))
                .OrderBy(x => x.BeginTime)
                .ToListAsync();

            return result;
        }

        public async Task<Expand> GetAsync(long id)
        {
            Expand item = await context.Expand.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        /// <summary>只查出有資料的日期,月份,或年</summary>
        public async Task<List<string>> GetList4ReportAsync(string format, int pathId, int? periodId, DateTime beginDate, DateTime endDate)
        {
            var expands = await context.OutCome.AsNoTracking()
                .Include(x => x.Expand)
                .ThenInclude(x => x.PatrolPathPeriod)
                .Where(x => x.PatrolPathId == pathId
                   && (periodId == null || (periodId != null && x.Expand.PatrolPathPeriodId == periodId.Value))
                   && x.Expand.BeginTime >= beginDate && x.Expand.EndTime <= endDate
                   && (x.Expand.PatrolPathPeriod.Status == "N" || (x.Expand.PatrolPathPeriod.Status == "Y" && x.IsCompleted == "Y")))
                .Distinct()
                .OrderBy(x => x.Expand.BeginTime)
                .Select(x => x.Expand.BeginTime.ToString(format/*"yyyy-MM"*/))
                .ToListAsync();

            return expands;
        }
    }
}
