using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.RazorModels
{
    using AutoMapper;
    using Database.Models.Models;
    using InspectionBlazor.AdapterModels;
    using InspectionBlazor.DataModels;
    using InspectionBlazor.Helpers;
    using InspectionBlazor.Interfaces;
    using InspectionBlazor.Services;
    using DBTest.Shared;
    using Syncfusion.Blazor.Grids;
    using Microsoft.AspNetCore.Components.Forms;
    using Syncfusion.Blazor.Inputs;
    using InspectionShare.Helpers;
    using System.IO;
    using InspectionShare.Enums;
    using Microsoft.EntityFrameworkCore;
    using Syncfusion.Blazor.DropDowns;
    using Syncfusion.Blazor.Data;
    using Microsoft.AspNetCore.Components.Authorization;

    public class PersonRazorModel
    {
        #region Constructor
        public PersonRazorModel(PersonService CurrentService,
            JobTitleService jobTitleService,
            DepartmentService _departmentService,
            AuthorityService authorityService,
            IMapper Mapper,
            AuthenticationStateProvider authenticationStateProvider,
            PersonManagerService personManagerService,
            PersonDepartmentService personDepartmentService)
        {
            this.JobTitleService = jobTitleService;
            this.CurrentService = CurrentService;
            this.authorityService = authorityService;
            departmentService = _departmentService;
            mapper = Mapper;
            AuthenticationStateProvider = authenticationStateProvider;
            PersonManagerService = personManagerService;
            PersonDepartmentService = personDepartmentService;
        }
        #endregion

        #region Property
        public SfAutoComplete<int?, JobTitle> JobTitleIdAutoObj { get; set; }
        public SfGrid<PersonAdapterModel> Grid { get; set; }
        public string[] InitSearch = (new string[] { "Name", "Account", "Email", "PersonDepartmentName", "JobTitleName" });
        public bool isVisibleRecord { get; set; } = false;
        public PersonAdapterModel CurrentRecord { get; set; } = new PersonAdapterModel();
        public PersonAdapterModel CurrentNeedDeleteRecord { get; set; } = new PersonAdapterModel();

        //新增jobTitle提供下拉選單使用
        public List<JobTitle> listJobTitle = new List<JobTitle>();
        public List<AuthorityAdapterModel> listAuthority = new List<AuthorityAdapterModel>();
        public List<Department> listDepartment = new List<Department>();
        public List<PersonDepartmentForPersonView> person { get; set; } = new List<PersonDepartmentForPersonView>();
        public List<Department> department = new List<Department>();
        #region 訊息說明之對話窗使用的變數
        public ConfirmBoxModel ConfirmMessageBox { get; set; } = new ConfirmBoxModel();
        public MessageBoxModel MessageBox { get; set; } = new MessageBoxModel();
        #endregion

        public string DialogTitle { get; set; } = "";

        public List<string> listMonths = new List<string>();
        public List<string> listYears = new List<string>();
        public string SelectedMonth = "";
        public string SelectedYear = "";
        #endregion

        #region Field
        public bool ShowPicker { get; set; } = false;
        bool newRecordMode;
        private readonly PersonService CurrentService;
        private readonly JobTitleService JobTitleService;
        private readonly AuthorityService authorityService;
        private readonly DepartmentService departmentService;
        private readonly IMapper mapper;
        IRazorPage thisRazorComponent;
        private bool isVisibleConfirm { get; set; } = false;
        public string DepartmentId { get; set; }
        public EditContext LocalEditContext { get; set; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; }
        public PersonManagerService PersonManagerService { get; }
        public PersonDepartmentService PersonDepartmentService { get; }
        public bool IsReadyToShowGrid { get; set; } = false;
        #endregion

        #region Method
        public void Setup(IRazorPage componentBase,
            SfGrid<PersonAdapterModel> grid)
        {
            thisRazorComponent = componentBase;
            Grid = grid;
        }

        public async Task InitAsync()
        {
            department = await departmentService.GetDepartmentAllAsync();
            person = await CurrentService.GetPersonManagerAll();
            listJobTitle = await JobTitleService.GetJobTitleAllAsync();
            listAuthority = await authorityService.GetForPersonAsync();
            IsReadyToShowGrid = true;

            int Year = DateTime.Now.AddYears(-1912).Year;

            for (int i = Year; i < Year + 3; i++)
                listYears.Add($"{i} 年");

            for (int i = 1; i <= 12; i++)
                listMonths.Add($"{i} 月");
        }

        public void OnOpenPicker()
        {
            ShowPicker = true;
        }

        public async Task ToolbarClickHandlerAsync(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "新增")
            {
                CurrentRecord = new PersonAdapterModel()
                {
                    Status = MagicHelper.StatusNoCode,
                    StatusName = MagicHelper.StatusNo
                };

                DialogTitle = "新增紀錄";
                newRecordMode = true;
                isVisibleRecord = true;
            }
        }

        public async Task OnCommandClickedAsync(CommandClickEventArgs<PersonAdapterModel> args)
        {
            PersonAdapterModel item = args.RowData as PersonAdapterModel;
            if (args.CommandColumn.ButtonOption.Content == "修改")
            {
                CurrentRecord = item;
                CurrentRecord.PersonManagerId = await PersonManagerService.GetPersonManagerAsync(CurrentRecord.Id);
                CurrentRecord.PersonDepartmentId = await PersonDepartmentService.GetPersonDepartmentAsync(CurrentRecord.Id);

                DialogTitle = "修改紀錄";
                isVisibleRecord = true;
                newRecordMode = false;
            }
            else if (args.CommandColumn.ButtonOption.Content == "刪除")
            {
                #region 檢查關聯資料是否存在
                #endregion
                CurrentNeedDeleteRecord = item;
                ConfirmMessageBox.Show("400px", "200px", "警告", "確認要刪除這筆紀錄嗎？");
            }
        }

        public async Task RemoveThisRecord(bool NeedDelete)
        {
            if (NeedDelete == true)
            {
                await CurrentService.DeleteAsync(mapper.Map<Person>(CurrentNeedDeleteRecord));
                Grid.Refresh();
            }
            ConfirmMessageBox.Hidden();
        }

        public void OnCancel()
        {
            isVisibleRecord = false;
        }

        public void OnEditContestChanged(EditContext context)
        {
            LocalEditContext = context;
        }

        public async Task OnSaveAsync()
        {
            #region 進行 Form Validation 檢查驗證作業
            if (LocalEditContext.Validate() == false)
            {
                return;
            }
            #endregion

            if (isVisibleRecord == true)
            {
                if (await CurrentService.CheckAccountIsExistAsync(CurrentRecord.Id, CurrentRecord.Account))
                {
                    CurrentRecord.Account = null;
                    MessageBox.Show("400px", "200px", "提醒", "此帳號已存在，請重新輸入");
                    return;
                }

                int?[] PersonManagerIds = CurrentRecord.PersonManagerId;
                long?[] PersonDepartmentIds = CurrentRecord.PersonDepartmentId;

                UserHelper userHelper = new UserHelper(AuthenticationStateProvider);
                (int UserId, string Account, string UserName) = await userHelper.GetUserInformation2Async();

                if (newRecordMode == true)
                {
                    PasswordHelper.GetPasswordSHA(CurrentRecord);
                    await CurrentService.AddAsync(mapper.Map<Person>(CurrentRecord), PersonManagerIds, PersonDepartmentIds, UserId);
                }
                else
                {
                    if (string.IsNullOrEmpty(CurrentRecord.PasswordPlainText) == false)
                        PasswordHelper.GetPasswordSHA(CurrentRecord);
                    await CurrentService.UpdateAsync(mapper.Map<Person>(CurrentRecord), PersonManagerIds, PersonDepartmentIds, UserId);
                }

                Grid.Refresh();
                isVisibleRecord = false;
            }
        }

        public async Task HandleValidSubmit() { }

        #endregion
        public async Task DisableIt(PersonAdapterModel item)
        {
            await CurrentService.DisableIt(mapper.Map<Person>(item));
            Grid.Refresh();
        }
        public async Task EnableIt(PersonAdapterModel item)
        {
            await CurrentService.EnableIt(mapper.Map<Person>(item));
            Grid.Refresh();
        }

        // 職稱下拉filter
        public async Task JobTitleIdOnFilter(FilteringEventArgs args)
        {
            args.PreventDefaultAction = true;
            WhereFilter whereFilter = new WhereFilter() { Field = "Name", Operator = "contains", value = args.Text, IgnoreCase = true };// 可以用名稱查詢
            var query = new Query().Where(whereFilter);
            query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
            await JobTitleIdAutoObj.Filter(listJobTitle, query);
        }
        public void SelectedYearChanged(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, string> args)
        {
            SelectedYear = args.Value;
        }

        public void SelectedMonthChanged(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, string> args)
        {
            SelectedMonth = args.Value;
        }
        public void showErrorMessage()
        {
            MessageBox.Show("400px", "200px", "提醒", "尚未選擇年份或月份");
        }
    }
}
