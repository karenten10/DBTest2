using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.RazorModels
{
    using AutoMapper;
    using Database.Models.Models;
    using InspectionBlazor.AdapterModels;
    using InspectionBlazor.Helpers;
    using InspectionBlazor.Interfaces;
    using InspectionBlazor.Services;
    using Microsoft.AspNetCore.Components.Forms;
    using Syncfusion.Blazor.Data;
    using Syncfusion.Blazor.DropDowns;
    using Syncfusion.Blazor.Grids;
    public class PersonnelChangeRazorModel
    {
        #region Constructor
        public PersonnelChangeRazorModel(PersonnelChangeService CurrentService,
           InspectionDBContext InspectionDBContext, PersonService personService,
           IMapper Mapper)
        {
            this.CurrentService = CurrentService;
            inspectionDBContext = InspectionDBContext;
            this.personService = personService;
            mapper = Mapper;
        }
        #endregion

        #region Property
        public SfGrid<PersonnelChangeAdapterModel> Grid { get; set; }
        public string[] InitSearch = (new string[] { "PersonName", "JobTitleName", "MustQualify", "OptionalQualify", "Memo" });
        public bool isVisibleRecord { get; set; } = false;
        public PersonnelChangeAdapterModel CurrentRecord { get; set; } = new PersonnelChangeAdapterModel();
        public PersonnelChangeAdapterModel CurrentNeedDeleteRecord { get; set; } = new PersonnelChangeAdapterModel();


        #region 訊息說明之對話窗使用的變數
        public ConfirmBoxModel ConfirmMessageBox { get; set; } = new ConfirmBoxModel();
        public MessageBoxModel MessageBox { get; set; } = new MessageBoxModel();
        #endregion

        public string DialogTitle { get; set; } = "";
        #endregion

        #region Field
        public bool ShowPicker { get; set; } = false;
        bool newRecordMode;
        private readonly PersonnelChangeService CurrentService;
        private readonly InspectionDBContext inspectionDBContext;
        private readonly PersonService personService;
        private readonly IMapper mapper;
        IRazorPage thisRazorComponent;
        private bool isVisibleConfirm { get; set; } = false;
        public List<Person> people { get; set; } = new List<Person>();
        public EditContext LocalEditContext { get; set; }
        public List<string> listMonths = new List<string>();
        public List<string> listYears = new List<string>();
        public string SelectedYear = "";
        public string SelectedMonth = "";
        #endregion

        #region Method
        public async Task SetupAsync(IRazorPage componentBase,
            SfGrid<PersonnelChangeAdapterModel> grid)
        {
            thisRazorComponent = componentBase;
            Grid = grid;

            people = await personService.GetPersonContractorAsync(MagicHelper.ContractorType.正興.ToString());
        }
        public async Task InitAsync()
        {
            int Year = DateTime.Now.AddYears(-1912).Year;

            for (int i = Year; i < Year + 3; i++)
                listYears.Add($"{i} 年");

            for (int i = 1; i <= 12; i++)
                listMonths.Add($"{i} 月");

            await Task.Delay(100);
        }

        public async Task ToolbarClickHandlerAsync(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "新增")
            {
                CurrentRecord = new PersonnelChangeAdapterModel()
                {
                    Status = MagicHelper.StatusNoCode,
                    StatusName = MagicHelper.StatusNo,
                    IsChanged = MagicHelper.StatusNoCode,
                    IsChangedName = MagicHelper.StatusNo,
                    ChangedCount = 1,
                    OriginalCount = 1,
                    VacancyCount = 1,
                };

                DialogTitle = "新增紀錄";
                newRecordMode = true;
                isVisibleRecord = true;
            }
        }
        public async Task OnCommandClickedAsync(CommandClickEventArgs<PersonnelChangeAdapterModel> args)
        {
            PersonnelChangeAdapterModel item = args.RowData as PersonnelChangeAdapterModel;
            if (args.CommandColumn.ButtonOption.Content == "修改")
            {
                CurrentRecord = item;
                CurrentRecord.PersonIds = await CurrentService.GetPersonIdsAsync(CurrentRecord.Id);

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
                await CurrentService.DeleteAsync(mapper.Map<PersonnelChange>(CurrentNeedDeleteRecord));
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
                if (newRecordMode == true)
                {
                    await CurrentService.AddAsync(mapper.Map<PersonnelChange>(CurrentRecord), CurrentRecord.PersonIds);
                    Grid.Refresh();
                }
                else
                {
                    await CurrentService.UpdateAsync(mapper.Map<PersonnelChange>(CurrentRecord), CurrentRecord.PersonIds);
                    Grid.Refresh();
                }
                isVisibleRecord = false;
            }
        }

        public async Task HandleValidSubmit()
        {

        }

        #endregion
        public async Task DisableIt(PersonnelChangeAdapterModel item)
        {
            await CurrentService.DisableIt(mapper.Map<PersonnelChange>(item));
            Grid.Refresh();
        }
        public async Task EnableIt(PersonnelChangeAdapterModel item)
        {
            await CurrentService.EnableIt(mapper.Map<PersonnelChange>(item));
            Grid.Refresh();
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
