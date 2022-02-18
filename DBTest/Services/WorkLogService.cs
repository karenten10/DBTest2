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
    public class WorkLogService
    {
        private readonly InspectionDBContext context;
        private readonly AttendanceRegisterService attendanceRegisterService;
        private readonly DepartmentService departmentService;
        private readonly WorkTypeService workTypeService;

        public WorkLogService(InspectionDBContext context, AttendanceRegisterService attendanceRegisterService,
            DepartmentService departmentService, WorkTypeService workTypeService)
        {
            this.context = context;
            this.attendanceRegisterService = attendanceRegisterService;
            this.departmentService = departmentService;
            this.workTypeService = workTypeService;
        }

        public async Task<IQueryable<WorkLog>> GetAsync(DateTime? SearchDate, long? depId)
        {
            var result = await attendanceRegisterService.GetDataAsync(SearchDate, depId);
            var workTypeIds = result
                .Where(x => x.AttendanceDate.Year == SearchDate.Value.Year && x.AttendanceDate.Month == SearchDate.Value.Month
                    && x.AttendanceDate.Day == SearchDate.Value.Day)
                .Select(x => x.WorkTypeId)
                .Distinct()
                .ToList();

            return context.WorkLog
                .AsNoTracking()
                .Where(x => x.WorkLogDate.Year == SearchDate.Value.Year && x.WorkLogDate.Month == SearchDate.Value.Month
                    && x.WorkLogDate.Day == SearchDate.Value.Day && workTypeIds.Contains(x.WorkTypeId))
                .OrderBy(x => x.WorkTypeId)
                .ThenBy(x => x.ContractorShiftId)
                .AsQueryable();
        }

        public async Task GenerateDataAsync(DateTime? dateTime, long? depId)
        {
            var depName = await departmentService.GetDepartmentNameAsync((int)depId.Value);
            var result = await attendanceRegisterService.GetDataAsync(dateTime, depId);
            var g = result.Where(x => x.AttendanceDate.Year == dateTime.Value.Year && x.AttendanceDate.Month == dateTime.Value.Month
                && x.AttendanceDate.Day == dateTime.Value.Day && x.WorkTypeId != null && x.ContractorShiftId != null)
                .GroupBy(x => new { x.WorkTypeId, x.ContractorShiftId });

            DateTime newTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
            List<WorkLog> workLogs = new List<WorkLog>();

            foreach (var item in g)
            {
                var isExisted = await context.WorkLog
                    .AsNoTracking()
                    .AnyAsync(x => x.WorkLogDate.Year == newTime.Year && x.WorkLogDate.Month == newTime.Month
                        && x.WorkLogDate.Day == newTime.Day && x.ContractorShiftId == item.Key.ContractorShiftId
                        && x.WorkTypeId == item.Key.WorkTypeId);

                if (!isExisted)
                {
                    if (depName == MagicHelper.正興部門.鍋爐.ToString())
                    {
                        workLogs.Add(new WorkLog
                        {
                            WorkLogDate = newTime,
                            ContractorShiftId = item.Key.ContractorShiftId.Value,
                            WorkTypeId = item.Key.WorkTypeId.Value,
                            WorkingArea = "全院",
                            WorkingContent = (await workTypeService.GetAsync(item.Key.WorkTypeId.Value)).JobName
                        });
                    }
                    else
                    {
                        workLogs.Add(new WorkLog
                        {
                            WorkLogDate = newTime,
                            ContractorShiftId = item.Key.ContractorShiftId.Value,
                            WorkTypeId = item.Key.WorkTypeId.Value
                        });
                    }
                }
            }

            if (workLogs.Any())
            {
                await context.WorkLog.AddRangeAsync(workLogs);
                await context.SaveChangesAsync();
            }
        }

        public async Task<(string, int)> GetAttendanceRegisterPersonNameAsync(int workTypeId, int contractorShiftId, DateTime dateTime, long? depId)
        {
            var result = await attendanceRegisterService.GetDataAsync(dateTime, depId);
            var 全天班 = result
                .Where(x => x.AttendanceDate.Year == dateTime.Year && x.AttendanceDate.Month == dateTime.Month
                    && x.AttendanceDate.Day == dateTime.Day && x.WorkTypeId == workTypeId 
                    && x.ContractorShiftId == contractorShiftId && x.LeaveTypeId == null)
                .ToList();
            var 半天班 = result
                .Where(x => x.AttendanceDate.Year == dateTime.Year && x.AttendanceDate.Month == dateTime.Month
                    && x.AttendanceDate.Day == dateTime.Day && x.WorkTypeId == workTypeId
                    && x.ContractorShiftId == contractorShiftId && x.LeaveTypeId != null)
                .ToList();

            string NameArrayString = "";
            foreach (var item in 全天班)
                NameArrayString += $"{item.Person.Name}、";
            foreach (var item in 半天班)
                NameArrayString += $"{item.Person.Name}*、";
            if (NameArrayString.Length > 0) NameArrayString = NameArrayString.Substring(0, NameArrayString.Length - 1);

            return (NameArrayString, 全天班.Count() + 半天班.Count());
        }

        public async Task<WorkLog> GetAsync(int id)
        {
            WorkLog item = await context.WorkLog.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(WorkLog paraObject)
        {
            await context.WorkLog.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<WorkLog> UpdateAsync(WorkLog paraObject)
        {
            WorkLog item = await context.WorkLog
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<WorkLog>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<WorkLog> DeleteAsync(WorkLog paraObject)
        {
            await Task.Delay(100);
            WorkLog item = await context.WorkLog.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.WorkLog.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
    }
}
