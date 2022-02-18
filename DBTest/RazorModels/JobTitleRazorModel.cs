using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.RazorModels {
    using AutoMapper;
    using Database.Models.Models;
    using InspectionBlazor.AdapterModels;
    using InspectionBlazor.Helpers;
    using InspectionBlazor.Interfaces;
    using InspectionBlazor.Services;
    using Microsoft.AspNetCore.Components.Forms;
    using Syncfusion.Blazor.Grids;
    public class JobTitleRazorModel {
        #region Constructor
        public JobTitleRazorModel(JobTitleService CurrentService,
           InspectionDBContext InspectionDBContext,
           IMapper Mapper) {
            this.CurrentService = CurrentService;
            inspectionDBContext = InspectionDBContext;
            mapper = Mapper;
        }
        #endregion

        #region Property
        public SfGrid<JobTitleAdapterModel> Grid { get; set; }
        public string[] InitSearch = (new string[] { "Name" });
        public bool isVisibleRecord { get; set; } = false;
        public JobTitleAdapterModel CurrentRecord { get; set; } = new JobTitleAdapterModel();
        public JobTitleAdapterModel CurrentNeedDeleteRecord { get; set; } = new JobTitleAdapterModel();


        #region 訊息說明之對話窗使用的變數
        public ConfirmBoxModel ConfirmMessageBox { get; set; } = new ConfirmBoxModel();
        public MessageBoxModel MessageBox { get; set; } = new MessageBoxModel();
        #endregion

        public string DialogTitle { get; set; } = "";
        #endregion

        #region Field
        public bool ShowPicker { get; set; } = false;
        bool newRecordMode;
        private readonly JobTitleService CurrentService;
        private readonly InspectionDBContext inspectionDBContext;
        private readonly IMapper mapper;
        IRazorPage thisRazorComponent;
        private bool isVisibleConfirm { get; set; } = false;
        public EditContext LocalEditContext { get; set; }
        #endregion

        #region Method
        public void Setup(IRazorPage componentBase,
            SfGrid<JobTitleAdapterModel> grid) {
            thisRazorComponent = componentBase;
            Grid = grid;
            //RazorModel
        }
        public void OnOpenPicker() {
            ShowPicker = true;
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args) {
            if (args.Item.Text == "新增") {
                CurrentRecord = new JobTitleAdapterModel();

                DialogTitle = "新增紀錄";
                newRecordMode = true;
                isVisibleRecord = true;
            }
        }
        public void OnCommandClicked(CommandClickEventArgs<JobTitleAdapterModel> args) {
            JobTitleAdapterModel item = args.RowData as JobTitleAdapterModel;
            if (args.CommandColumn.ButtonOption.Content == "修改") {
                CurrentRecord = item;
                DialogTitle = "修改紀錄";
                isVisibleRecord = true;
                newRecordMode = false;

            } else if (args.CommandColumn.ButtonOption.Content == "刪除") {
                #region 檢查關聯資料是否存在
                #endregion
                CurrentNeedDeleteRecord = item;
                ConfirmMessageBox.Show("400px", "200px", "警告", "確認要刪除這筆紀錄嗎？");
            }
        }

        public async Task RemoveThisRecord(bool NeedDelete) {
            if (NeedDelete == true) {
                await CurrentService.DeleteAsync(mapper.Map<JobTitle>(CurrentNeedDeleteRecord));
                Grid.Refresh();
            }
            ConfirmMessageBox.Hidden();
        }

        public void OnCancel() {
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
                if (await CurrentService.CheckJobTitleIsExistAsync(CurrentRecord.Id, CurrentRecord.Name))
                {
                    MessageBox.Show("400px", "200px", "提醒", "此職稱已存在，請重新輸入");
                    return;
                }

                if (newRecordMode == true)
                {
                    await CurrentService.AddAsync(mapper.Map<JobTitle>(CurrentRecord));
                    Grid.Refresh();
                }
                else
                {
                    await CurrentService.UpdateAsync(mapper.Map<JobTitle>(CurrentRecord));
                    Grid.Refresh();
                }
                isVisibleRecord = false;
            }
        }

        public async Task HandleValidSubmit() {
            
        }

        #endregion
    }
}
