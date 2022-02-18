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
    public class WorkingPlanCollectionService
    {
        private readonly InspectionDBContext context;

        public WorkingPlanCollectionService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<WorkingPlanCollection>> GetAsync(DateTime? SearchDate)
        {
            return Task.FromResult(context.WorkingPlanCollection
                .AsNoTracking()
                .Include(x => x.Equipment)
                .ThenInclude(x => x.Section)
                .Include(x => x.Expand)
                .ThenInclude(x => x.PatrolPathPeriod)
                .Include(x => x.Equipment)
                .ThenInclude(x => x.EquipmentTemplate)
                .Where(x => x.WorkDate.Year == SearchDate.Value.Year && x.WorkDate.Month == SearchDate.Value.Month)
                .OrderBy(x => x.WorkDate)
                .AsQueryable());
        }

        public async Task<DateTime?> GetActualWorkDateByExpandIdAsync(long expandId, int equipmentId, int periodId)
        {
            return await context.OutCome
                .AsNoTracking()
                .Include(x => x.ExpandExamItem)
                .ThenInclude(x => x.EquipmentExamItem)
                .ThenInclude(x => x.Equipment)
                .Include(x => x.Expand)
                .Where(x => x.ExpandId == expandId && x.ExpandExamItem.EquipmentExamItem.Equipment.Id == equipmentId &&
                    x.Expand.PatrolPathPeriodId == periodId && x.IsCompleted == MagicHelper.StatusYesCode)
                .Select(x => x.UpdateTime)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkingPlanCollection> GetAsync(int id)
        {
            WorkingPlanCollection item = await context.WorkingPlanCollection.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<WorkingPlanCollection>> GetAllAsync()
        {
            return await context.WorkingPlanCollection
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task AddAsync(WorkingPlanCollection paraObject)
        {
            try
            {
                await context.WorkingPlanCollection.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<WorkingPlanCollection> UpdateAsync(WorkingPlanCollection paraObject)
        {
            WorkingPlanCollection item = await context.WorkingPlanCollection
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<WorkingPlanCollection>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<WorkingPlanCollection> DeleteAsync(WorkingPlanCollection paraObject)
        {
            await Task.Delay(100);
            WorkingPlanCollection item = await context.WorkingPlanCollection.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.WorkingPlanCollection.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<bool> IsExistedAsync(DateTime? dateTime)
        {
            return await context.WorkingPlanCollection
                .AsNoTracking()
                .Where(x => x.WorkDate.Year == dateTime.Value.Year && x.WorkDate.Month == dateTime.Value.Month &&
                    x.WorkDate.Day == dateTime.Value.Day)
                .AnyAsync();
        }

        public async Task GenerateDataAsync(DateTime? dateTime)
        {
            dateTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1);

            var workingPlans = await context.WorkingPlanCollection
                .AsNoTracking()
                .Where(x => x.WorkDate.Year == dateTime.Value.Year && x.WorkDate.Month == dateTime.Value.Month)
                .ToListAsync();

            List<WorkingPlanCollection> workingPlanCollections = new List<WorkingPlanCollection>();

            var 每日 = await 取得一個月內的時段Async(dateTime, "每日");
            var 每週 = await 取得一個月內的時段Async(dateTime, "每週");
            var 每月 = await 取得一個月內的時段Async(dateTime, "每月");
            var 每季 = await 取得超過一個月的時段Async(dateTime, "每季");
            var 每半年 = await 取得超過一個月的時段Async(dateTime, "每半年");
            var 每年 = await 取得超過一個月的時段Async(dateTime, "每年");

            依照原時段分配設備(每日, workingPlans, workingPlanCollections);
            依照原時段分配設備(每週, workingPlans, workingPlanCollections);
            await 依照工種時段分配設備Async(每月, dateTime, workingPlans, workingPlanCollections);
            await 依照工種時段分配設備Async(每季, dateTime, workingPlans, workingPlanCollections);
            await 依照工種時段分配設備Async(每半年, dateTime, workingPlans, workingPlanCollections);
            await 依照工種時段分配設備Async(每年, dateTime, workingPlans, workingPlanCollections);

            await context.WorkingPlanCollection.AddRangeAsync(workingPlanCollections);
            await context.SaveChangesAsync();
        }

        private async Task DEBUG未排到工種的設備Async(List<設備展開集合> 每日, List<設備展開集合> 每週, List<設備展開集合> 每月)
        {
            List<int> 沒排到工種的設備ID = new List<int>();
            var 每日沒排到的 = 每日.Where(x => x.WorkTypeId == null);
            foreach (var item in 每日沒排到的)
                沒排到工種的設備ID.Add(item.EquipmentId);
            var 每週沒排到的 = 每週.Where(x => x.WorkTypeId == null);
            foreach (var item in 每週沒排到的)
                沒排到工種的設備ID.Add(item.EquipmentId);
            var 每月沒排到的 = 每月.Where(x => x.WorkTypeId == null);
            foreach (var item in 每月沒排到的)
                沒排到工種的設備ID.Add(item.EquipmentId);
            沒排到工種的設備ID = 沒排到工種的設備ID.Distinct().ToList();
            var result = await context.Equipment
                .AsNoTracking()
                .Include(x => x.EquipmentExamItem)
                .Include(x => x.Section)
                .Where(x => 沒排到工種的設備ID.Contains(x.Id))
                .Select(x => new { 區段 = x.Section.Name, 設備 = x.EquipmentName })
                .ToListAsync();
        }

        private void 依照原時段分配設備(List<設備展開集合> 設備展開集合, List<WorkingPlanCollection> workingPlans,
            List<WorkingPlanCollection> workingPlanCollections)
        {
            foreach (var item in 設備展開集合)
            {
                if (IsExisted(workingPlans, item)) continue;

                workingPlanCollections.Add(new WorkingPlanCollection
                {
                    EquipmentId = item.EquipmentId,
                    ExpandId = item.ExpandId,
                    WorkDate = new DateTime(item.WalkthroughDate.Year, item.WalkthroughDate.Month, item.WalkthroughDate.Day)
                });
            }
        }

        private async Task 依照工種時段分配設備Async(List<設備展開集合> 設備展開集合, DateTime? dateTime,
            List<WorkingPlanCollection> workingPlans, List<WorkingPlanCollection> workingPlanCollections)
        {
            var WorkTypeList = 設備展開集合.Select(x => x.WorkTypeId).Distinct().ToArray();

            foreach (var item in WorkTypeList)
            {
                var WorkTypeEquipment = 設備展開集合.Where(x => x.WorkTypeId == item.Value).ToList();
                string[] WorkTypePeriod = (await context.WorkType
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == item.Value)).WorkingDay.Split('|');
                if (WorkTypePeriod.Length == 0) continue;
                int DaysInMonth = DateTime.DaysInMonth(dateTime.Value.Year, dateTime.Value.Month);
                int DayCount = 0;
                //Console.WriteLine("工種時段：" + string.Join(' ', WorkTypePeriod));

                foreach (var equipment in WorkTypeEquipment)
                {
                    if (IsExisted(workingPlans, equipment)) continue;

                    DayCount = CalcWorkDate(dateTime, WorkTypePeriod, DaysInMonth, DayCount);
                    workingPlanCollections.Add(new WorkingPlanCollection
                    {
                        EquipmentId = equipment.EquipmentId,
                        ExpandId = equipment.ExpandId,
                        WorkDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, DayCount)
                    });
                    //Console.WriteLine($"Equipment {equipment.EquipmentId}\tDate {new DateTime(dateTime.Value.Year, dateTime.Value.Month, DayCount).ToString("yyyy/MM/dd")} {new DateTime(dateTime.Value.Year, dateTime.Value.Month, DayCount).DayOfWeek}");
                }
            }
        }

        private bool IsExisted(List<WorkingPlanCollection> workingPlans, 設備展開集合 item)
        {
            return workingPlans.Where(x => x.EquipmentId == item.EquipmentId && x.ExpandId == item.ExpandId &&
                (item.WalkthroughDate.Year == 1 || x.WorkDate.Year == item.WalkthroughDate.Year && x.WorkDate.Month == item.WalkthroughDate.Month)).Any();
        }

        /// <summary>Recursive</summary>
        private int CalcWorkDate(DateTime? dateTime, string[] WorkTypePeriod, int DaysInMonth, int DayCount)
        {
            string[] DayOfWeekList = { "0", "1", "2", "3", "4", "5", "6" };

            DayCount += 1;
            if (DayCount > DaysInMonth) DayCount = 1;

            if (WorkTypePeriod.Contains(DayOfWeekList[(int)new DateTime(dateTime.Value.Year, dateTime.Value.Month, DayCount).DayOfWeek]))
                return DayCount;
            else
                return CalcWorkDate(dateTime, WorkTypePeriod, DaysInMonth, DayCount);
        }

        private readonly string[] 排除路線 = { "CO2量測", "F/C", "出回風口", "冷卻水塔每週耗水量統計", "電錶",
            "颱風前空調機房巡檢", "思源RF及精神樓1F熱水爐(月)", "各式負壓病房檢測", "中正樓重要機房巡檢", "主機房每月巡檢"};

        private async Task<List<設備展開集合>> 取得一個月內的時段Async(DateTime? dateTime, string period)
        {
            var result = (await
                (from a in context.Expand
                 join b in context.PatrolPathPeriod
                 on a.PatrolPathPeriodId equals b.Id
                 join c in context.ExpandExamItem
                 on a.Id equals c.ExpandId
                 join d in context.EquipmentExamItem
                 on c.EquipmentExamItemId equals d.Id
                 join e in context.Equipment
                 on d.EquipmentId equals e.Id
                 join f in context.PatrolPath
                 on a.PatrolPathId equals f.Id
                 join g in context.Department
                 on f.DepartmentId equals g.Id
                 join h in context.PatrolPathPeriodNexamItem
                 on new { PatrolPathPeriodId = b.Id, EquipmentExamItemId = d.Id } equals new { h.PatrolPathPeriodId, h.EquipmentExamItemId }
                 where b.Cycle.Contains(period) &&
                     a.BeginTime.Year == dateTime.Value.Year && a.BeginTime.Month == dateTime.Value.Month &&
                     g.DepartmentName == MagicHelper.正興部門.空調.ToString() &&
                     d.WorkTypeId != null && !排除路線.Contains(f.Name) &&
                     !f.Name.ToLower().Contains("sop")
                 select new
                 {
                     EquipmentId = e.Id,
                     ExpandId = a.Id,
                     WalkthroughDate = a.BeginTime,
                     WorkTypeId = d.WorkTypeId
                 })
                .ToListAsync())
                .GroupBy(x => new { x.EquipmentId, x.ExpandId, x.WalkthroughDate, x.WorkTypeId });

            List<設備展開集合> 設備展開集合 = new List<設備展開集合>();
            int 清潔Id = (await context.WorkType.AsNoTracking().FirstOrDefaultAsync(x => x.JobName == "清潔")).Id;

            foreach (var item in result)
            {
                var findEquipment = 設備展開集合
                    .FirstOrDefault(x => x.EquipmentId == item.Key.EquipmentId && x.ExpandId == item.Key.ExpandId);

                //假設有兩個工種，取除了清潔的工種
                if (findEquipment == null)
                {
                    設備展開集合.Add(new 設備展開集合
                    {
                        EquipmentId = item.Key.EquipmentId,
                        ExpandId = item.Key.ExpandId,
                        WalkthroughDate = item.Key.WalkthroughDate,
                        WorkTypeId = item.Key.WorkTypeId
                    });
                }
                else
                {
                    if (item.Key.WorkTypeId != 清潔Id)
                        findEquipment.WorkTypeId = item.Key.WorkTypeId;
                }
            }

            return 設備展開集合;
        }

        private async Task<List<設備展開集合>> 取得超過一個月的時段Async(DateTime? dateTime, string period)
        {
            var result = (await
                (from a in context.Expand
                 join b in context.PatrolPathPeriod
                 on a.PatrolPathPeriodId equals b.Id
                 join c in context.ExpandExamItem
                 on a.Id equals c.ExpandId
                 join d in context.EquipmentExamItem
                 on c.EquipmentExamItemId equals d.Id
                 join e in context.Equipment
                 on d.EquipmentId equals e.Id
                 join f in context.PatrolPath
                 on a.PatrolPathId equals f.Id
                 join g in context.Department
                 on f.DepartmentId equals g.Id
                 join h in context.PatrolPathPeriodNexamItem
                 on new { PatrolPathPeriodId = b.Id, EquipmentExamItemId = d.Id } equals new { h.PatrolPathPeriodId, h.EquipmentExamItemId }
                 where b.Cycle.Contains(period) &&
                     a.BeginTime.Year == dateTime.Value.Year && a.BeginTime.Month <= dateTime.Value.Month &&
                     a.EndTime.Year == dateTime.Value.Year && a.EndTime.Month >= dateTime.Value.Month &&
                     g.DepartmentName == MagicHelper.正興部門.空調.ToString() &&
                     d.WorkTypeId != null && !排除路線.Contains(f.Name) &&
                     !f.Name.ToLower().Contains("sop")
                 select new
                 {
                     EquipmentId = e.Id,
                     ExpandId = a.Id,
                     WorkTypeId = d.WorkTypeId
                 })
                .ToListAsync())
                .GroupBy(x => new { x.EquipmentId, x.ExpandId, x.WorkTypeId })
                .OrderBy(x => x.Key.EquipmentId)
                .ThenBy(x => x.Key.ExpandId);

            List<設備展開集合> 設備展開集合 = new List<設備展開集合>();
            int 清潔Id = (await context.WorkType.AsNoTracking().FirstOrDefaultAsync(x => x.JobName == "清潔")).Id;

            foreach (var item in result)
            {
                var findEquipment = 設備展開集合
                    .FirstOrDefault(x => x.EquipmentId == item.Key.EquipmentId && x.ExpandId == item.Key.ExpandId);

                //假設有兩個工種，取除了清潔的工種
                if (findEquipment == null)
                {
                    設備展開集合.Add(new 設備展開集合
                    {
                        EquipmentId = item.Key.EquipmentId,
                        ExpandId = item.Key.ExpandId,
                        WorkTypeId = item.Key.WorkTypeId
                    });
                }
                else
                {
                    if (item.Key.WorkTypeId != 清潔Id)
                        findEquipment.WorkTypeId = item.Key.WorkTypeId;
                }
            }

            switch (period)
            {
                case "每季":
                    {
                        int 商 = 設備展開集合.Count() / 3;
                        int 餘 = 設備展開集合.Count() % 3;

                        switch (dateTime.Value.Month)
                        {
                            case 1:
                            case 4:
                            case 7:
                            case 10:
                                設備展開集合 = 設備展開集合.Skip(商 * 0).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 2:
                            case 5:
                            case 8:
                            case 11:
                                設備展開集合 = 設備展開集合.Skip(商 * 1).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 3:
                            case 6:
                            case 9:
                            case 12:
                                設備展開集合 = 設備展開集合.Skip(商 * 2).ToList();
                                設備展開集合 = 設備展開集合.Take(商 + 餘).ToList();
                                break;
                        }
                    }
                    break;
                case "每半年":
                    {
                        int 商 = 設備展開集合.Count() / 6;
                        int 餘 = 設備展開集合.Count() % 6;

                        switch (dateTime.Value.Month)
                        {
                            case 1:
                            case 7:
                                設備展開集合 = 設備展開集合.Skip(商 * 0).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 2:
                            case 8:
                                設備展開集合 = 設備展開集合.Skip(商 * 1).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 3:
                            case 9:
                                設備展開集合 = 設備展開集合.Skip(商 * 2).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 4:
                            case 10:
                                設備展開集合 = 設備展開集合.Skip(商 * 3).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 5:
                            case 11:
                                設備展開集合 = 設備展開集合.Skip(商 * 4).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 6:
                            case 12:
                                設備展開集合 = 設備展開集合.Skip(商 * 5).ToList();
                                設備展開集合 = 設備展開集合.Take(商 + 餘).ToList();
                                break;
                        }
                    }
                    break;
                case "每年":
                    {
                        int 商 = 設備展開集合.Count() / 12;
                        int 餘 = 設備展開集合.Count() % 12;

                        switch (dateTime.Value.Month)
                        {
                            case 1:
                                設備展開集合 = 設備展開集合.Skip(商 * 0).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 2:
                                設備展開集合 = 設備展開集合.Skip(商 * 1).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 3:
                                設備展開集合 = 設備展開集合.Skip(商 * 2).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 4:
                                設備展開集合 = 設備展開集合.Skip(商 * 3).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 5:
                                設備展開集合 = 設備展開集合.Skip(商 * 4).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 6:
                                設備展開集合 = 設備展開集合.Skip(商 * 5).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 7:
                                設備展開集合 = 設備展開集合.Skip(商 * 6).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 8:
                                設備展開集合 = 設備展開集合.Skip(商 * 7).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 9:
                                設備展開集合 = 設備展開集合.Skip(商 * 8).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 10:
                                設備展開集合 = 設備展開集合.Skip(商 * 9).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 11:
                                設備展開集合 = 設備展開集合.Skip(商 * 10).ToList();
                                設備展開集合 = 設備展開集合.Take(商).ToList();
                                break;
                            case 12:
                                設備展開集合 = 設備展開集合.Skip(商 * 11).ToList();
                                設備展開集合 = 設備展開集合.Take(商 + 餘).ToList();
                                break;
                        }
                    }
                    break;
            }

            return 設備展開集合;
        }

        private IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
                yield return new DateTime(year, month, day);
        }

        private class 設備展開集合
        {
            public int EquipmentId { get; set; }
            public long ExpandId { get; set; }
            public DateTime WalkthroughDate { get; set; }
            public int? WorkTypeId { get; set; }
        }
    }
}
