using Database.Models.Models;
using InspectionShare.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class MenuListService
    {
        InspectionDBContext context;

        public MenuListService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<List<MenuList>> GetListAsync()
        {
            List<MenuList> result = await context.MenuList
                .Where(x => !MenuHelper.一般使用者權限排除.Contains(x.Code))
                .AsNoTracking()
                .OrderBy(x => x.Index)
                .ToListAsync();
            return result;
        }

        public async Task<List<MenuList>> GetListAsync(int personId)
        {
            var result = new List<MenuList>();
            var person = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personId);

            if (person != null)
            {
                var authorityDetail = await context.AuthorityDetail
                    .AsNoTracking()
                    .Where(x => x.AuthorityId == person.AuthorityId)
                    .Select(x => x.MenuCode)
                    .ToArrayAsync();

                if (authorityDetail.Count() > 0)
                {
                    result = await context.MenuList
                        .AsNoTracking()
                        .Where(x => authorityDetail.Contains(x.Code))
                        .OrderBy(x => x.Index)
                        .ToListAsync();
                }
            }
            CleanTracking.Clean<Person>(context);
            CleanTracking.Clean<AuthorityDetail>(context);
            CleanTracking.Clean<MenuList>(context);

            return result;
        }

        public async Task<List<MenuList>> GetListByAdminAsync(string account)
        {
            if (account.ToLower().Equals("httc"))
            {
                return await context.MenuList
                    .AsNoTracking()
                    .OrderBy(x => x.Index)
                    .ToListAsync();
            }
            else
            {
                return await context.MenuList
                    .Where(x => !MenuHelper.客戶系統管理員權限排除.Contains(x.Code))
                    .AsNoTracking()
                    .OrderBy(x => x.Index)
                    .ToListAsync();
            }
        }
    }
}
