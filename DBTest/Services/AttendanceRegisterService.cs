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
    public class AttendanceRegisterService
    {
        private readonly InspectionDBContext context;
        private readonly PersonService personService;
        private readonly LeaveTypeService leaveTypeService;
        private readonly ContractorShiftService contractorShiftService;
        private readonly WorkTypeService workTypeService;
        private readonly DepartmentService departmentService;
        private readonly ShiftSchedulingRulesService shiftSchedulingRulesService;

        public AttendanceRegisterService(InspectionDBContext context, PersonService personService,
            LeaveTypeService leaveTypeService, ContractorShiftService contractorShiftService,
            WorkTypeService workTypeService, DepartmentService departmentService,
            ShiftSchedulingRulesService shiftSchedulingRulesService)
        {
            this.context = context;
            this.personService = personService;
            this.leaveTypeService = leaveTypeService;
            this.contractorShiftService = contractorShiftService;
            this.workTypeService = workTypeService;
            this.departmentService = departmentService;
            this.shiftSchedulingRulesService = shiftSchedulingRulesService;
        }

        public Task<IQueryable<AttendanceRegister>> GetAsync()
        {
            return Task.FromResult(context.AttendanceRegister
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<AttendanceRegister> GetAsync(long id)
        {
            AttendanceRegister item = await context.AttendanceRegister
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(AttendanceRegister paraObject)
        {
            try
            {
                await context.AttendanceRegister.AddAsync(paraObject);
                await context.SaveChangesAsync();
            }
            catch (Exception) { }

            return;
        }

        public async Task<AttendanceRegister> UpdateAsync(AttendanceRegister paraObject)
        {
            AttendanceRegister item = await context.AttendanceRegister
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<AttendanceRegister>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<AttendanceRegister> DeleteAsync(AttendanceRegister paraObject)
        {
            await Task.Delay(100);
            AttendanceRegister item = await context.AttendanceRegister.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.AttendanceRegister.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<bool> IsExistDataInThisDateAsync(DateTime? dateTime)
        {
            try
            {
                return await context.AttendanceRegister
                .AsNoTracking()
                .AnyAsync(x => x.AttendanceDate.Year == dateTime.Value.Year &&
                    x.AttendanceDate.Month == dateTime.Value.Month);
            }
            catch (Exception)
            {
                return false;
            }

        }

        ///<summary>產生預排班表</summary>
        ///<remarks>
        ///預排班表規則：
        ///1. 針對部門為「空調」的人員，其中分為「固定」、「輪班」兩種排班方式：
        ///   ↳ 固定：
        ///     1. 平日星期一到五班別為「正常」
        ///     2. 假日六日排「休」、「例」
        ///   ↳ 輪班：
        ///     1. 做二休二
        ///     2. 每做二後替換班別，例如：AA 假假 BB 假假 AA 假假 BB
        ///     3. 排班順序由上個月尾巴接續排班
        ///     4. 班別「A-」等同班別「A」
        ///     5. 排假遇到星期六優先排「休」、遇到星期日優先排「例」、剩下放假排「空」
        ///     6. 並優先排掉「例」，再來是「休」
        ///2. 針對部門為「鍋爐」的人員，只有一種固定排班方式，同上
        ///
        ///註：輪班規則已移到Database.Init專案中產生
        /// </remarks>
        public async Task<bool> GenerateDataAsync(DateTime? dateTime, MagicHelper.ContractorType contractor)
        {
            await context.Database.BeginTransactionAsync();
            string Msg = "";

            try
            {
                List<DateTime> dateTimes = new List<DateTime>();
                for (int i = 1; i <= DateTime.DaysInMonth(dateTime.Value.Year, dateTime.Value.Month); i++)
                    dateTimes.Add(new DateTime(dateTime.Value.Year, dateTime.Value.Month, i));

                switch (contractor)
                {
                    case MagicHelper.ContractorType.正興:
                        var people = await personService.GetPersonByContractorAsync(contractor.ToString());
                        #region 特定的班別、假別、工種
                        //假別
                        int? 假別休Id = await leaveTypeService.GetIdByNameAsync("休");  //給禮拜六用
                        int? 假別例Id = await leaveTypeService.GetIdByNameAsync("例");  //給禮拜日用
                        int? 假別空Id = await leaveTypeService.GetIdByNameAsync("空");  //給除了禮拜六日之外用的預設假別
                        //班別
                        int? 班別正常Id = await contractorShiftService.GetIdByNameAsync("正常");  //給固定排班，但不在排班規則中的預設班別
                        int? 班別AId = await contractorShiftService.GetIdByNameAsync("A");        //輪班1預設班別
                        int? 班別BId = await contractorShiftService.GetIdByNameAsync("B");        //輪班2預設班別
                        //工種
                        int? 定檢1Id = await workTypeService.GetIdByNameAsync("定檢1");
                        #endregion

                        var 本月預排輪班表 = await context.WalkthroughShiftSchedule
                            .AsNoTracking()
                            .Where(x => x.AttendanceDate.Year == dateTime.Value.Year &&
                                x.AttendanceDate.Month == dateTime.Value.Month)
                            .ToListAsync();

                        foreach (var person in people)
                        {
                            #region 如已有排班資料，則跳過
                            var isExist = await context.AttendanceRegister
                                .AsNoTracking()
                                .AnyAsync(x => x.PersonId == person.Id &&
                                    x.AttendanceDate.Year == dateTime.Value.Year &&
                                    x.AttendanceDate.Month == dateTime.Value.Month);
                            if (isExist) continue;
                            #endregion

                            #region 決定排班規則
                            MagicHelper.ShiftRuleType 排班規則 = MagicHelper.ShiftRuleType.固定;
                            MagicHelper.正興部門 depName;
                            bool getDepNameresult = Enum.TryParse(await departmentService.GetDepartmentNameByPersonIdAsync(person.Id), out depName);
                            if (!getDepNameresult)
                            {
                                Msg += $"{person.Name} 未設定部門 ";
                                continue;
                            }
                            else
                            {
                                switch (depName)
                                {
                                    case MagicHelper.正興部門.空調:
                                        MagicHelper.ShiftRuleType shiftRule;
                                        bool shiftRuleResult = Enum.TryParse(await shiftSchedulingRulesService.GetShiftRulesNameByPersonIdAsync(person.Id), out shiftRule);
                                        if (!shiftRuleResult)
                                        {
                                            Msg += $"{person.Name} 未設定排班規則 ";
                                            continue;
                                        }
                                        else
                                        {
                                            排班規則 = shiftRule;
                                        }
                                        break;
                                    case MagicHelper.正興部門.鍋爐:
                                        排班規則 = MagicHelper.ShiftRuleType.固定;
                                        break;
                                }
                            }
                            #endregion

                            #region 開始依照規則排班
                            switch (排班規則)
                            {
                                case MagicHelper.ShiftRuleType.固定:
                                    foreach (var date in dateTimes)
                                    {
                                        int? 例假日Id = null;
                                        if (date.DayOfWeek == DayOfWeek.Saturday)
                                            例假日Id = 假別休Id;
                                        else if (date.DayOfWeek == DayOfWeek.Sunday)
                                            例假日Id = 假別例Id;

                                        if (例假日Id != null)
                                        {
                                            await context.AttendanceRegister.AddAsync(new AttendanceRegister
                                            {
                                                AttendanceDate = date,
                                                PersonId = person.Id,
                                                LeaveTypeId = 例假日Id,
                                                LeaveTypeSegment = MagicHelper.PeriodType.全天.ToString()
                                            });
                                        }
                                        else
                                        {
                                            await context.AttendanceRegister.AddAsync(new AttendanceRegister
                                            {
                                                AttendanceDate = date,
                                                PersonId = person.Id,
                                                ContractorShiftId = 班別正常Id,
                                                ContractorShiftSegment = MagicHelper.PeriodType.全天.ToString(),
                                                WorkTypeId = 定檢1Id  //工種暫時排定檢1
                                            });
                                        }
                                    }
                                    break;
                                case MagicHelper.ShiftRuleType.輪班1:
                                case MagicHelper.ShiftRuleType.輪班2:
                                case MagicHelper.ShiftRuleType.輪班3:
                                case MagicHelper.ShiftRuleType.輪班4:
                                    foreach (var date in dateTimes)
                                    {
                                        var getShiftData = 本月預排輪班表
                                            .FirstOrDefault(x => x.AttendanceDate == date && x.RuleType == 排班規則.ToString());
                                        if (getShiftData != null)
                                        {
                                            string shiftName = getShiftData.ShiftName;

                                            //目前有五種排班，兩種是班別(A/B)，三種是假別(例/休/空)
                                            if (shiftName == "A" | shiftName == "B")
                                            {
                                                int? 輪班班別Id = shiftName == "A" ? 班別AId : 班別BId;

                                                await context.AttendanceRegister.AddAsync(new AttendanceRegister
                                                {
                                                    AttendanceDate = date,
                                                    PersonId = person.Id,
                                                    ContractorShiftId = 輪班班別Id,
                                                    ContractorShiftSegment = MagicHelper.PeriodType.全天.ToString(),
                                                    WorkTypeId = 定檢1Id  //工種暫時排定檢1
                                                });
                                            }
                                            else
                                            {
                                                int? 輪班假別Id;
                                                if (shiftName == "例")
                                                    輪班假別Id = 假別例Id;
                                                else if(shiftName == "休")
                                                    輪班假別Id = 假別休Id;
                                                else
                                                    輪班假別Id = 假別空Id;

                                                await context.AttendanceRegister.AddAsync(new AttendanceRegister
                                                {
                                                    AttendanceDate = date,
                                                    PersonId = person.Id,
                                                    LeaveTypeId = 輪班假別Id,
                                                    LeaveTypeSegment = MagicHelper.PeriodType.全天.ToString()
                                                });
                                            }
                                        }
                                    }
                                    break;
                            }
                            #endregion
                        }
                        break;
                    default:
                        break;
                }

                await context.SaveChangesAsync();
                await context.Database.CommitTransactionAsync();
                string m = Msg;
                return true;
            }
            catch (Exception e)
            {
                await context.Database.RollbackTransactionAsync();
                string err = $"{Msg}\n{e}";
                return false;
            }
        }

        public async Task<List<AttendanceRegister>> GetDataAsync(DateTime? dateTime, long? depId)
        {
            var people = await context.PersonDepartment
                .Where(x => x.DepartmentId == depId)
                .Select(x => x.PersonId)
                .ToListAsync();

            return await context.AttendanceRegister
                .AsNoTracking()
                .Include(x => x.Person)
                .Include(x => x.LeaveType)
                .Include(x => x.ContractorShift)
                .Include(x => x.WorkType)
                .Where(x => x.AttendanceDate.Year == dateTime.Value.Year &&
                    x.AttendanceDate.Month == dateTime.Value.Month &&
                    people.Contains(x.PersonId) &&
                    x.Person.ContractorName == MagicHelper.ContractorType.正興.ToString() &&
                    x.Person.Status == MagicHelper.StatusNoCode)
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(DateTime? dateTime)
        {
            return await context.AttendanceRegister
                .AsNoTracking()
                .AnyAsync(x => x.AttendanceDate.Year == dateTime.Value.Year &&
                    x.AttendanceDate.Month == dateTime.Value.Month);
        }
    }
}
