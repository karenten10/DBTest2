using AutoMapper;
using Dapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using InspectionShare.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PersonService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<PersonService> logger;

        public DepartmentService DepartmentService { get; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public IMapper Mapper { get; }

        public PersonService(InspectionDBContext context, ILogger<PersonService> logger,
            DepartmentService departmentService,
            AuthenticationStateProvider authenticationStateProvider,
            IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            DepartmentService = departmentService;
            AuthenticationStateProvider = authenticationStateProvider;
            Mapper = mapper;
        }

        public async Task<IQueryable<PersonAdapterModel>> GetAsync(string _account, int _userId)
        {

            //判斷是不是系統管理員
            var checkAdmin = await context.Person
                .AsNoTracking()
                .Where(x => ShareMagicHelper.鴻才系統管理員帳號.Contains(_account))
                .FirstOrDefaultAsync();

            if(checkAdmin == null)
            {
                List<long?> listUserDep = new List<long?>();

                //取得使用者有哪些部門
                var userDeps = await context.PersonDepartment
                    .AsNoTracking()
                    .Where(x => x.PersonId == _userId).ToListAsync();

                if (userDeps.Any())
                {
                    foreach (var item in userDeps)
                    {
                        listUserDep.Add(item.DepartmentId);
                    }
                }

                //根據使用者的部門取得該看到的人員
                List<int> listUser = new List<int>();

                var users = await context.PersonDepartment
                    .AsNoTracking()
                    .Where(x => listUserDep.Contains(x.DepartmentId))
                    .ToListAsync();

                if (users.Any())
                {
                    foreach (var item in users)
                    {
                        listUser.Add(item.PersonId);
                    }
                }

                //鴻才管理員可看到所有帳號包含管理員帳號，除此之外一般使用者或客戶管理員不會看到管理員帳號
                var result = await context.Person
                    .AsNoTracking()
                    .Include(x => x.JobTitle)
                    .Where(x => ShareMagicHelper.鴻才系統管理員帳號.Contains(_account) ||
                        (!ShareMagicHelper.鴻才系統管理員帳號.Contains(_account) &&
                        !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()) &&
                        listUser.Contains(x.Id)))
                    .ToListAsync();

                var personAdapterModels = Mapper.Map<List<PersonAdapterModel>>(result);
                var personDep = await context.PersonDepartment
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .ToListAsync();

                foreach (var item in personAdapterModels)
                {
                    string DeartmentNames = "";
                    foreach (var dep in personDep.Where(x => x.PersonId == item.Id))
                    {
                        DeartmentNames += dep.Department.DepartmentName + "、";
                    }
                    if (DeartmentNames.Length > 0) DeartmentNames = DeartmentNames.Substring(0, DeartmentNames.Length - 1);
                    item.PersonDepartmentName = DeartmentNames;
                }
                return personAdapterModels.AsQueryable();
            }
            else
            {

                //鴻才管理員可看到所有帳號包含管理員帳號，除此之外一般使用者或客戶管理員不會看到管理員帳號
                var result = await context.Person
                    .AsNoTracking()
                    .Include(x => x.JobTitle)
                    .Where(x => ShareMagicHelper.鴻才系統管理員帳號.Contains(_account) ||
                        (!ShareMagicHelper.鴻才系統管理員帳號.Contains(_account) &&
                        !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower())))
                    .ToListAsync();

                var personAdapterModels = Mapper.Map<List<PersonAdapterModel>>(result);
                var personDep = await context.PersonDepartment
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .ToListAsync();

                foreach (var item in personAdapterModels)
                {
                    string DeartmentNames = "";
                    foreach (var dep in personDep.Where(x => x.PersonId == item.Id))
                    {
                        DeartmentNames += dep.Department.DepartmentName + "、";
                    }
                    if (DeartmentNames.Length > 0) DeartmentNames = DeartmentNames.Substring(0, DeartmentNames.Length - 1);
                    item.PersonDepartmentName = DeartmentNames;
                }
                return personAdapterModels.AsQueryable();
            }
        }

        public Task<List<Person>> GetPersonALL()
        {
            return Task.FromResult(context.Person
             .AsNoTracking()
             .Where(x => x.Status == "N" && !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()))
             .ToList());
        }

        public async Task<List<Person>> GetPersonAllByDeptId(long? rootDeptId)
        {
            var subDepts = new List<long?>();

            // 沒有傳值進來表示要查全部
            if (rootDeptId == null)
            {
                subDepts = null;
            }
            else
            {
                subDepts = await DepartmentService.GetSubDepts(rootDeptId.Value);
                subDepts.Add(rootDeptId); // 把自己加進去
            }

            // 撈出登入者部門(含子部門)
            UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
            (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();
            var loginDepts = await DepartmentService.GetLoginDeptsAsync(UserId, Account);

            var personDepartments = await context.PersonDepartment
                .AsNoTracking()
                .Where(x => (subDepts == null || subDepts.Contains(x.DepartmentId))
                    && (loginDepts == null || loginDepts.Contains(x.DepartmentId)))
                .ToListAsync();

            var personIds = new List<int>();
            foreach (var item in personDepartments)
            {
                personIds.Add(item.PersonId);
            }

            var result = await context.Person
                .AsNoTracking()
                .Where(x => personIds.Contains(x.Id)
                    && x.Status == "N" && !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()))
                .ToListAsync();

            return result;
        }

        public async Task<List<Person>> GetPersonALLAsync()
        {
            return await context.Person
                .AsNoTracking()
                .Where(x => x.Status == MagicHelper.StatusNoCode
                    && !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Person>> GetPersonContractorAsync(string contractor)
        {
            return await context.Person
                .AsNoTracking()
                .Where(x => x.Status == MagicHelper.StatusNoCode
                    && !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower())
                    && x.ContractorName == contractor)
                .ToListAsync();
        }

        public async Task<List<PersonDepartmentForPersonView>> GetPersonManagerAll()
        {
            List<long> auth = await context.AuthorityDetail
                .AsNoTracking()
                .Where(x => x.MenuCode == MagicHelper.主管簽核功能表代號)
                .Select(x => x.AuthorityId)
                .ToListAsync();

            var result = await context.Person
                .AsNoTracking()
                .Include(x => x.Authority)
                .Where(x => x.Status == MagicHelper.StatusNoCode
                    && auth.Contains((long)x.AuthorityId)
                    && !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()))
                .ToListAsync();

            List<PersonDepartmentForPersonView> personDepartmentForPersonViews = new List<PersonDepartmentForPersonView>();

            foreach (var item in result)
            {
                var findDepartment = await context.PersonDepartment
                    .AsNoTracking()
                    .Include(x => x.Department)
                    .Where(x => x.PersonId == item.Id)
                    .Select(x => x.Department.DepartmentName)
                    .FirstOrDefaultAsync();

                if (findDepartment == null) continue;

                personDepartmentForPersonViews.Add(new PersonDepartmentForPersonView
                {
                    PersonId = item.Id,
                    PersonName = item.Name,
                    DepartmentName = findDepartment
                });
            }

            return personDepartmentForPersonViews;
        }

        public async Task<(bool result, string message, Person person)> LoginAsync
            (string account, string password, bool webLogin, string inputValidateCode = "", string checkValidateCode = "")
        {
            bool result = false;
            string message = "";

            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Account == account);
            if (item != null)
            {
                {
                    (string salt, string encodePassword) =
                        PasswordHelper.GetPasswordSHA(item.Salt, password);
                    {
                        if (encodePassword == item.Password)
                        {
                            if (inputValidateCode == checkValidateCode)
                            {
                                result = true;
                            }
                            else
                            {
                                message = "驗證碼錯誤";
                            }
                        }
                        else
                        {
                            message = "此帳號或密碼不正確";
                        }
                    }
                }
            }
            else
            {
                message = "此帳號不存在";
            }

            return (result, message, item);
        }

        public async Task<int?[]> GetPersonManagerAsync(int personId)
        {
            try
            {
                return await context.PersonManager
                    .Where(x => x.PersonId == personId)
                    .Select(x => x.ManagerId)
                    .ToArrayAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<long?[]> GetPersonDepartmentAsync(int personId)
        {
            return await context.PersonDepartment
                .Where(x => x.PersonId == personId)
                .Select(x => x.DepartmentId)
                .ToArrayAsync();
        }

        public async Task<List<Person>> GetPersonByDeptIdAsync(long deptId)
        {
            return await context.PersonDepartment.Where(x => x.DepartmentId == deptId)
                .Join(context.Person,
                d => d.PersonId,
                p => p.Id,
                (d, p) => new Person
                {
                    Id = p.Id,
                    Name = p.Name,
                    Account = p.Account
                })
                .Where(x => !ShareMagicHelper.系統管理員帳號.Contains(x.Account.ToLower()))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int?[]> GetPersonByDepartmentIdAsync(long departmentId)
        {
            var result = await context.PersonDepartment
                .Where(x => x.DepartmentId == departmentId)
                .ToListAsync();

            if (result.Count > 0)
            {
                int?[] r = new int?[result.Count];
                for (int i = 0; i < result.Count(); i++)
                {
                    r[i] = result[i].PersonId;
                }
                return r;
            }
            return null;
        }

        public async Task<Person> GetAsync(int id)
        {
            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(Person paraObject, int?[] personManagerIds, long?[] personDepartmentIds, int lastUpdateUser)
        {
            paraObject.LastUpdateTime = DateTime.Now;
            paraObject.LastUpdateUser = lastUpdateUser;

            await context.Person.AddAsync(paraObject);
            await context.SaveChangesAsync();

            if (personManagerIds != null)
            {
                foreach (var item in personManagerIds)
                {
                    PersonManager personManager = new PersonManager
                    {
                        PersonId = paraObject.Id,
                        ManagerId = item
                    };
                    await context.PersonManager.AddAsync(personManager);
                    await context.SaveChangesAsync();
                }
            }

            if (personDepartmentIds != null)
            {
                foreach (var item in personDepartmentIds)
                {
                    PersonDepartment personDepartment = new PersonDepartment
                    {
                        PersonId = paraObject.Id,
                        DepartmentId = item
                    };
                    await context.PersonDepartment.AddAsync(personDepartment);
                    await context.SaveChangesAsync();
                }
            }

            return;
        }

        public async Task<Person> UpdateAsync(Person paraObject, int?[] personManagerIds, long?[] personDepartmentIds, int lastUpdateUser)
        {
            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                paraObject.LastUpdateTime = DateTime.Now;
                paraObject.LastUpdateUser = lastUpdateUser;

                context.CleanAllEFCoreTracking<Person>();
                context.Entry(paraObject).State = EntityState.Modified;

                await context.SaveChangesAsync();

                #region PersonManager
                // delete detail
                var aa = context.PersonManager.Where(x => x.PersonId == paraObject.Id);
                context.PersonManager.RemoveRange(aa);
                await context.SaveChangesAsync();

                // add detail
                if (personManagerIds != null)
                {
                    foreach (var list in personManagerIds)
                    {
                        await context.PersonManager.AddAsync(new PersonManager
                        {
                            PersonId = paraObject.Id,
                            ManagerId = list
                        });
                        await context.SaveChangesAsync();
                    }
                }
                #endregion

                #region PersonDepartment
                // delete detail
                var bb = context.PersonDepartment.Where(x => x.PersonId == paraObject.Id);
                context.PersonDepartment.RemoveRange(bb);
                await context.SaveChangesAsync();

                // add detail
                if (personDepartmentIds != null)
                {
                    foreach (var list in personDepartmentIds)
                    {
                        await context.PersonDepartment.AddAsync(new PersonDepartment
                        {
                            PersonId = paraObject.Id,
                            DepartmentId = list
                        });
                        await context.SaveChangesAsync();
                    }
                }
                #endregion

                return paraObject;
            }
        }

        public async Task<Person> UpdateSingleAsync(Person paraObject, int lastUpdateUser)
        {
            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);

            if (item == null)
            {
                return null;
            }
            else
            {
                try
                {
                    #region 在這裡需要設定需要解除快取紀錄
                    context.CleanAllEFCoreTracking<Person>();
                    #endregion
                    // set Modified flag in your entry
                    paraObject.PasswordUpdateTime = DateTime.Now;
                    paraObject.LastUpdateTime = DateTime.Now;
                    paraObject.LastUpdateUser = lastUpdateUser;
                    context.Entry(paraObject).State = EntityState.Modified;

                    // save 
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string ex = e.ToString();
                }

                return paraObject;
            }
        }

        public async Task<Person> DeleteAsync(Person paraObject)
        {
            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.CleanAllEFCoreTracking<Person>();
                context.Person.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
        public async Task DisableIt(Person paraObject)
        {
            await Task.Delay(100);
            Person curritem = await context.Person
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Person>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusYesCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task EnableIt(Person paraObject)
        {
            await Task.Delay(100);
            Person curritem = await context.Person
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            #region 在這裡需要設定需要更新的紀錄欄位值
            foreach (var item in context.Set<Person>().Local)
            {
                context.Entry(item).State = EntityState.Detached;
            }
            #endregion
            curritem.Status = MagicHelper.StatusNoCode;
            context.Entry(curritem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return;
        }
        public async Task<string> GetPersonName(int id)
        {
            string name = string.Empty;

            Person item = await context.Person
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                name = item.Name;
            }

            return name;
        }

        /// <summary>透過人員Id取得該人員所有所屬部門的異常通知人員Mail</summary>
        public async Task<List<string>> GetDepartmentReportMailAsync(int personId)
        {
            List<string> mails = new List<string>();

            var PersonDepartment = await context.PersonDepartment
                .AsNoTracking()
                .Include(x => x.Department)
                .Where(x => x.PersonId == personId)
                .ToListAsync();

            foreach (var item in PersonDepartment)
            {
                try
                {
                    if (item.Department.UnreportUsers != null)
                    {
                        var Mail = await context.Person
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == item.Department.UnreportUsers &&
                                                 !string.IsNullOrEmpty(x.Email));
                        if (Mail.Email.Contains("@")) mails.Add(Mail.Email);
                    }
                }
                catch (Exception) { }
            }

            return mails;
        }

        /// <summary>檢查帳號是否存在</summary>
        public async Task<bool> CheckAccountIsExistAsync(int userId, string account)
        {
            var result = await context.Person
                .Where(x => x.Id != userId && x.Account == account)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result != null ? true : false;
        }

        /// <summary>是否更改密碼</summary>
        public async Task<bool> IsChangePasswordAsync(string account)
        {
            var result = await context.Person
                .Where(x => x.Account == account)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                if (result.PasswordUpdateTime != null)
                {
                    return DateTime.Now.AddDays(-MagicHelper.改密碼天數) > result.PasswordUpdateTime ? true : false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<string>> GetAuthorityMenuCodeByPersonAsync(int personId)
        {
            var result = await context.Person
                .Include(x => x.Authority)
                .ThenInclude(x => x.AuthorityDetail)
                .Where(x => x.Id == personId)
                .AsNoTracking()
                .ToListAsync();

            List<string> menuCodes = new List<string>();

            foreach (var item in result)
            {
                if (item.Authority != null)
                {
                    var detail = item.Authority.AuthorityDetail;
                    foreach (var item2 in detail)
                    {
                        menuCodes.Add(item2.MenuCode);
                    }
                }
            }

            return menuCodes;
        }

        public async Task LoginHistoryAsync(int personId)
        {
            LoginHistory loginHistory = new LoginHistory
            {
                UserId = personId,
                LoginTime = DateTime.Now
            };
            await context.LoginHistory.AddAsync(loginHistory);
            await context.SaveChangesAsync();
        }

        public async Task<string> GetPeopleName(int?[] ids)
        {
            string name = string.Empty;

            if (ids != null)
            {
                var result = await context.Person
                    .AsNoTracking()
                    .Where(x => ids.Contains(x.Id))
                    .Select(x => x.Name)
                    .ToListAsync();

                foreach (var item in result)
                    name += $"{item}、";

                if (name.Length > 0)
                    name = name.Substring(0, name.Length - 1);
            }

            return name;
        }

        public async Task<List<Person>> GetPersonByContractorAsync(string contractor)
        {
            return await context.Person
                .AsNoTracking()
                .Where(x => x.ContractorName == contractor && x.Status == MagicHelper.StatusNoCode)
                .ToListAsync();
        }
    }
}
