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
    public class ContractEmployeesService
    {
        private readonly InspectionDBContext context;

        public ContractEmployeesService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<ContractEmployees>> GetAsync()
        {
            return Task.FromResult(context.ContractEmployees
                .AsNoTracking()
                .AsQueryable());
        }

        public async Task<ContractEmployees> GetAsync(int id)
        {
            ContractEmployees item = await context.ContractEmployees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<ContractEmployees>> GetByDateAsync(DateTime? dateTime, long? departmentId)
        {
            return await context.ContractEmployees
                .AsNoTracking()
                .Where(x => x.ContractDate.Year == dateTime.Value.Year && x.ContractDate.Month == dateTime.Value.Month
                    && x.DepartmentId == departmentId.Value)
                .ToListAsync();
        }

        public async Task AddAsync(ContractEmployees paraObject)
        {
            await context.ContractEmployees.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<ContractEmployees> UpdateAsync(ContractEmployees paraObject)
        {
            ContractEmployees item = await context.ContractEmployees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<ContractEmployees>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<ContractEmployees> DeleteAsync(ContractEmployees paraObject)
        {
            await Task.Delay(100);
            ContractEmployees item = await context.ContractEmployees.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.ContractEmployees.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task GenerateDataAsync(DateTime? dateTime, long? departmentId)
        {
            var find = await context.ContractEmployees
                .AsNoTracking()
                .AnyAsync(x => x.ContractDate.Year == dateTime.Value.Year && x.ContractDate.Month == dateTime.Value.Month
                    && x.DepartmentId == departmentId.Value);
            var departmentName = (await context.Department
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == departmentId)).DepartmentName;

            if (!find)
            {
                List<ContractEmployees> contractEmployees = new List<ContractEmployees>();

                for (int i = 1; i <= DateTime.DaysInMonth(dateTime.Value.Year, dateTime.Value.Month); i++)
                {
                    int NumberOfPeople = 0;
                    var DayOfWeek = new DateTime(dateTime.Value.Year, dateTime.Value.Month, i).DayOfWeek;

                    if (departmentName == MagicHelper.正興部門.空調.ToString())
                    {
                        if (DayOfWeek == DayOfWeek.Saturday)
                            NumberOfPeople = 15;
                        else if (DayOfWeek == DayOfWeek.Sunday)
                            NumberOfPeople = 6;
                        else
                            NumberOfPeople = 40;
                    }
                    else if (departmentName == MagicHelper.正興部門.鍋爐.ToString())
                    {
                        if (DayOfWeek == DayOfWeek.Saturday)
                            NumberOfPeople = 6;
                        else if (DayOfWeek == DayOfWeek.Sunday)
                            NumberOfPeople = 5;
                        else
                            NumberOfPeople = 7;
                    }

                    contractEmployees.Add(new ContractEmployees
                    {
                        DepartmentId = departmentId.Value,
                        ContractDate = new DateTime(dateTime.Value.Year, dateTime.Value.Month, i),
                        NumberOfPeople = NumberOfPeople
                    });
                }

                await context.ContractEmployees.AddRangeAsync(contractEmployees);
                await context.SaveChangesAsync();
            }
        }
    }
}
