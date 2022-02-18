using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using InspectionShare.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class DepartmentService
    {
        private readonly InspectionDBContext context;
        public AuthenticationStateProvider AuthenticationStateProvider { get; }

        public DepartmentService(InspectionDBContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            this.context = context;
            AuthenticationStateProvider = authenticationStateProvider;
        }

        public Task<IQueryable<Department>> GetAsync()
        {
            return Task.FromResult(context.Department
                .AsNoTracking().AsQueryable());
        }

        public async Task<Department> GetAsync(int id)
        {
            Department item = await context.Department.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<Department> GetAsyncLong(long id)
        {
            Department item = await context.Department.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<MyDropDownListModel>> GetListAsync()
        {
            var result = await context.Department.AsNoTracking().ToListAsync();
            List<MyDropDownListModel> myDropDownListModels = new List<MyDropDownListModel>();
            foreach (var item in result)
            {
                myDropDownListModels.Add(new MyDropDownListModel
                {
                    ID = item.Id.ToString(),
                    Text = item.DepartmentName
                });
            }
            return myDropDownListModels;
        }

        public async Task<List<Department>> GetOnListAsync()
        {
            return await context.Department.AsNoTracking()
                .Where(x => x.Status == "N")
                .ToListAsync();
        }

        public async Task<List<long?>> GetSubDepts(long parentId)
        {
            List<long?> childDepts = new List<long?>();

            var subDepts = await context.Department.AsNoTracking()
                    .Where(x => x.ParentId == parentId)
                    .ToListAsync();

            if (subDepts != null && subDepts.Count() > 0)
            {
                foreach (var item in subDepts)
                {
                    childDepts.Add(item.Id);

                    var sub2 = await GetSubDepts(item.Id);

                    childDepts.AddRange(sub2);
                }
            }

            return childDepts;
        }

        public async Task<List<long?>> GetLoginDeptsAsync(int UserId, string Account)
        {
            List<long?> loginDepts = new List<long?>();

            if (ShareMagicHelper.系統管理員帳號.Contains(Account.ToLower()))
            {
                loginDepts = null; // 管理員可看到全部 => 回傳null
            }
            else
            {
                // 根據登入者撈出部門清單(含底下部門)
                // 因非管理員只能看到自己部門資料(含底下部門)
                // loginDepts.Add(3); 

                var rootDepts = await context.PersonDepartment.AsNoTracking()
                    .Where(x => x.PersonId == UserId)
                    .ToListAsync();

                if (rootDepts != null && rootDepts.Count() > 0)
                {
                    foreach (var item in rootDepts)
                    {
                        loginDepts.Add(item.DepartmentId);
                        // 撈出子部門清單
                        if (item.DepartmentId != null)
                        {
                            var subDepts = await GetSubDepts(item.DepartmentId.Value);
                            loginDepts.AddRange(subDepts);
                        }
                    }
                }
            }

            return loginDepts;
        }

        public async Task AddAsync(Department paraObject)
        {
            await context.Department.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<Department> UpdateAsync(Department paraObject)
        {
            Department item = await context.Department
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<Department>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<Department> DeleteAsync(Department paraObject)
        {
            await Task.Delay(100);
            Department item = await context.Department.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.Department.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<string> GetParentNameAsync(int ParentId)
        {
            var result = await context.Department
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == ParentId);

            return result != null ? result.DepartmentName : "";
        }

        public async Task DisableIt(Department paraObject)
        {
            await Task.Delay(100);
            Department curritem = await context.Department
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Department>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(Department paraObject)
        {
            await Task.Delay(100);
            Department curritem = await context.Department
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Department>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<Department>> GetDepartmentAllByLoginAsync()
        {
            UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();

            var loginDepts = await GetLoginDeptsAsync(UserId, Account);

            var result = await context.Department
                .AsNoTracking()
                .Where(x => loginDepts == null || loginDepts.Contains(x.Id))
                .ToListAsync();

            return result;
        }

        public async Task<List<Department>> GetDepartmentAllByLoginFor正興Async()
        {
            var result = await context.Department
                .AsNoTracking()
                .Where(x => x.DepartmentName == MagicHelper.正興部門.空調.ToString() ||
                    x.DepartmentName == MagicHelper.正興部門.鍋爐.ToString())
                .ToListAsync();


            return result;
            //UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            //(int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();

            //List<string> deps = new List<string>();

            //if (ShareMagicHelper.系統管理員帳號.Contains(Account))
            //{
            //    deps.Add(MagicHelper.正興部門.空調.ToString());
            //    deps.Add(MagicHelper.正興部門.鍋爐.ToString());
            //}
            //else
            //{
            //    var depName = await context.PersonDepartment
            //        .Include(x => x.Department)
            //        .Where(x => x.PersonId == UserId)
            //        .Select(x => x.Department.DepartmentName)
            //        .FirstOrDefaultAsync();

            //    if(depName == MagicHelper.正興部門.空調.ToString())
            //        deps.Add(MagicHelper.正興部門.空調.ToString());
            //    else if(depName == MagicHelper.正興部門.鍋爐.ToString())
            //        deps.Add(MagicHelper.正興部門.鍋爐.ToString());
            //}

            //var result = await context.Department
            //    .AsNoTracking()
            //    .Where(x => deps.Contains(x.DepartmentName))
            //    .ToListAsync();

            //return result;
        }

        public async Task<List<Department>> GetDepartmentAllAsync()
        {
            return await context.Department
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<string> GetDepartmentNameAsync(int DepartmentId)
        {
            var department = await context.Department
                .Where(x => x.Id == DepartmentId)
                .FirstOrDefaultAsync();

            return department != null ? department.DepartmentName : "";
        }

        public async Task<bool> CheckDepartmentIsExistAsync(long id, string department)
        {
            var result = await context.Department
                .Where(x => x.Id != id && x.DepartmentName == department)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result != null ? true : false;
        }

        public async Task<string> GetAllPersonDepartmenNameAsync(int userId)
        {
            var result = await context.PersonDepartment
                .AsNoTracking()
                .Include(x => x.Department)
                .Where(x => x.PersonId == userId)
                .ToListAsync();

            string DeartmentNames = "";
            foreach (var item in result)
            {
                DeartmentNames += item.Department.DepartmentName + "、";
            }
            if (DeartmentNames.Length > 0) DeartmentNames = DeartmentNames.Substring(0, DeartmentNames.Length - 1);

            return DeartmentNames;
        }

        public async Task<string> GetDepartmentNameByPersonIdAsync(int personId)
        {
            return await context.PersonDepartment
                .AsNoTracking()
                .Include(x => x.Department)
                .Where(x => x.PersonId == personId)
                .Select(x => x.Department.DepartmentName)
                .FirstOrDefaultAsync();
        }
    }
}
