using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class ShiftSchedulingRulesService
    {
        private readonly InspectionDBContext context;

        public ShiftSchedulingRulesService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<ShiftSchedulingRules>> GetAsync()
        {
            return Task.FromResult(context.ShiftSchedulingRules
               .AsNoTracking()
               .AsQueryable());
        }

        public async Task<ShiftSchedulingRules> GetAsync(int id)
        {
            ShiftSchedulingRules item = await context.ShiftSchedulingRules.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<ShiftSchedulingRulesPeople>> GetAllAsync()
        {
            return await context.ShiftSchedulingRulesPeople
               .AsNoTracking()
               .Include(x => x.ShiftSchedulingRules)
               .ToListAsync();
        }

        public async Task AddAsync(ShiftSchedulingRules paraObject, int?[] peopleId)
        {
            try
            {
                await context.ShiftSchedulingRules.AddAsync(paraObject);
                await context.SaveChangesAsync();

                if (peopleId != null)
                {
                    foreach (var item in peopleId)
                    {
                        ShiftSchedulingRulesPeople shiftSchedulingRulesPeople = new ShiftSchedulingRulesPeople
                        {
                            ShiftSchedulingRulesId = paraObject.Id,
                            PersonId = item.Value
                        };
                        await context.ShiftSchedulingRulesPeople.AddAsync(shiftSchedulingRulesPeople);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception) { }

            return;
        }

        public async Task<ShiftSchedulingRules> UpdateAsync(ShiftSchedulingRules paraObject, int?[] peopleId)
        {
            ShiftSchedulingRules item = await context.ShiftSchedulingRules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<ShiftSchedulingRules>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();

                #region ShiftSchedulingRulesPeople
                // delete detail
                var del = context.ShiftSchedulingRulesPeople.Where(x => x.ShiftSchedulingRulesId == paraObject.Id);
                context.ShiftSchedulingRulesPeople.RemoveRange(del);
                await context.SaveChangesAsync();

                // add detail
                if (peopleId != null)
                {
                    foreach (var list in peopleId)
                    {
                        await context.ShiftSchedulingRulesPeople.AddAsync(new ShiftSchedulingRulesPeople
                        {
                            ShiftSchedulingRulesId = paraObject.Id,
                            PersonId = list.Value
                        });
                        await context.SaveChangesAsync();
                    }
                }
                #endregion

                return paraObject;
            }
        }

        public async Task<ShiftSchedulingRules> DeleteAsync(ShiftSchedulingRules paraObject)
        {
            await Task.Delay(100);
            ShiftSchedulingRules item = await context.ShiftSchedulingRules.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.ShiftSchedulingRules.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<int?[]> GetPeopleAsync(int shiftSchedulingRulesId)
        {
            var result = await context.ShiftSchedulingRulesPeople
                .Where(x => x.ShiftSchedulingRulesId == shiftSchedulingRulesId)
                .ToListAsync();

            if (result.Count > 0)
            {
                int?[] people = new int?[result.Count];
                for (int i = 0; i < result.Count(); i++)
                {
                    people[i] = result[i].PersonId;
                }
                return people;
            }

            return null;
        }

        public async Task<List<int>> GetAllShiftSchedulingRulesPeopleAsync(int? shiftSchedulingRulesId)
        {
            return await context.ShiftSchedulingRulesPeople
                .AsNoTracking()
                .Where(x => x.ShiftSchedulingRulesId != shiftSchedulingRulesId)
                .Select(x => x.PersonId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<string> GetShiftRulesNameByPersonIdAsync(int personId)
        {
            return await context.ShiftSchedulingRulesPeople
                .AsNoTracking()
                .Include(x => x.ShiftSchedulingRules)
                .Where(x => x.PersonId == personId)
                .Select(x => x.ShiftSchedulingRules.RuleType)
                .FirstOrDefaultAsync();
        }
    }
}
