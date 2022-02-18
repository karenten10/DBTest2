using Database.Models.Models;
using InspectionBlazor.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class CircularGaugeService
    {
        public InspectionDBContext context { get; }

        public CircularGaugeService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<double> GetDataForDailyAbnormalPercentageAsync()
        {
            try
            {
                DateTime today = DateTime.Now;
                int totalCount = await context.OutCome
                    .Include(x => x.Expand)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month && today.Day <= x.Expand.EndTime.Day)
                    .CountAsync();
                int abnormalCount = await context.OutCome
                    .Where(x => x.IsCompleted == MagicHelper.StatusYesCode && x.UpdateTime.Value.Year == today.Year &&
                    x.UpdateTime.Value.Month == today.Month && x.UpdateTime.Value.Day == today.Day && x.IsAbnormal == MagicHelper.StatusYesCode)
                    .CountAsync();

                return totalCount != 0 ? Math.Ceiling(((double)abnormalCount / (double)totalCount) * 100) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<double> GetDataForDailyPercentageAsync()
        {
            try
            {
                DateTime today = DateTime.Now;
                int totalCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month && today.Day <= x.Expand.EndTime.Day &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每日"))
                    .CountAsync();
                int dailyCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month && today.Day <= x.Expand.EndTime.Day &&
                    x.IsCompleted == MagicHelper.StatusYesCode && x.UpdateTime.Value.Year == today.Year &&
                    x.UpdateTime.Value.Month == today.Month && x.UpdateTime.Value.Day == today.Day &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每日"))
                    .CountAsync();

                return totalCount != 0 ? Math.Ceiling(((double)dailyCount / (double)totalCount) * 100) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<double> GetDataForWeeklyPercentageAsync()
        {
            try
            {
                DateTime today = DateTime.Now;
                int totalCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month && today.Day <= x.Expand.EndTime.Day &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每週"))
                    .CountAsync();
                int weeklyCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month && today.Day <= x.Expand.EndTime.Day &&
                    x.IsCompleted == MagicHelper.StatusYesCode && x.UpdateTime.Value.Year == today.Year &&
                    x.UpdateTime.Value.Month == today.Month && x.UpdateTime.Value.Day == today.Day &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每週"))
                    .CountAsync();

                return totalCount != 0 ? Math.Ceiling(((double)weeklyCount / (double)totalCount) * 100) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<double> GetDataForMonthlyPercentageAsync()
        {
            try
            {
                DateTime today = DateTime.Now;
                int totalCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每月"))
                    .CountAsync();
                int monthlyCount = await context.OutCome
                    .Include(x => x.Expand)
                    .ThenInclude(x => x.PatrolPathPeriod)
                    .Where(x => today.Year >= x.Expand.BeginTime.Year && today.Month >= x.Expand.BeginTime.Month && today.Day >= x.Expand.BeginTime.Day &&
                    today.Year <= x.Expand.EndTime.Year && today.Month <= x.Expand.EndTime.Month &&
                    x.IsCompleted == MagicHelper.StatusYesCode && x.UpdateTime.Value.Year == today.Year &&
                    x.UpdateTime.Value.Month == today.Month &&
                    x.Expand.PatrolPathPeriod.Cycle.Contains("每月"))
                    .CountAsync();

                return totalCount != 0 ? Math.Ceiling(((double)monthlyCount / (double)totalCount) * 100) : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
