using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.RazorModels
{
    using AutoMapper;
    using Database.Models.Models;
    using InspectionBlazor.AdapterModels;
    using InspectionBlazor.Interfaces;
    using InspectionBlazor.Services;
    using Syncfusion.Blazor.Grids;
    using Microsoft.AspNetCore.Components.Forms;
    using System.Data;
    using InspectionShare.Helpers;

    public class AuthorityRazorModel
    {
        #region Constructor
        public AuthorityRazorModel(AuthorityService CurrentService,
           InspectionDBContext InspectionDBContext,
           MenuListService menuListService,
           IMapper Mapper)
        {
            this.CurrentService = CurrentService;
            inspectionDBContext = InspectionDBContext;
            this.menuListService = menuListService;
            mapper = Mapper;
        }
        #endregion

        #region Property
        public SfGrid<AuthorityAdapterModel> Grid { get; set; }
        public string[] InitSearch = new string[] { "Name" };
        public bool isVisibleRecord { get; set; } = false;
        public AuthorityAdapterModel CurrentRecord { get; set; } = new AuthorityAdapterModel();
        public AuthorityAdapterModel CurrentNeedDeleteRecord { get; set; } = new AuthorityAdapterModel();
        public List<MenuList> menuList = null;
        public List<MyAuthList> myAuthLists = new List<MyAuthList>();

        #region 訊息說明之對話窗使用的變數
        public ConfirmBoxModel ConfirmMessageBox { get; set; } = new ConfirmBoxModel();
        public MessageBoxModel MessageBox { get; set; } = new MessageBoxModel();
        #endregion

        public string DialogTitle { get; set; } = "";
        #endregion

        #region Field
        public bool ShowPicker { get; set; } = false;
        bool newRecordMode;
        private readonly AuthorityService CurrentService;
        private readonly InspectionDBContext inspectionDBContext;
        private readonly MenuListService menuListService;
        private readonly IMapper mapper;
        IRazorPage thisRazorComponent;
        private bool isVisibleConfirm { get; set; } = false;
        public EditContext LocalEditContext { get; set; }
        #endregion

        #region Method
        public void Setup(IRazorPage componentBase,
            SfGrid<AuthorityAdapterModel> grid)
        {
            thisRazorComponent = componentBase;
            Grid = grid;
        }
        public void OnOpenPicker()
        {
            ShowPicker = true;
        }

        public async Task ToolbarClickHandlerAsync(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "新增")
            {
                CurrentRecord = new AuthorityAdapterModel()
                {

                };
                DialogTitle = "新增權限";
                newRecordMode = true;

                await GetListAsync();
                isVisibleRecord = true;
            }
        }

        public async Task OnCommandClickedAsync(CommandClickEventArgs<AuthorityAdapterModel> args)
        {
            AuthorityAdapterModel item = args.RowData as AuthorityAdapterModel;
            if (args.CommandColumn.ButtonOption.Content == "修改")
            {
                CurrentRecord = item;
                DialogTitle = "編輯權限";
                newRecordMode = false;

                await GetListAsync();
                isVisibleRecord = true;
            }
            else if (args.CommandColumn.ButtonOption.Content == "刪除")
            {
                #region 檢查關聯資料是否存在
                #endregion
                CurrentNeedDeleteRecord = item;
                ConfirmMessageBox.Show("400px", "200px", "警告", "確認要刪除這筆紀錄嗎？");
            }
        }

        private async Task GetListAsync()
        {
            myAuthLists.Clear();
            List<AuthorityDetail> authorityDetail = await CurrentService.GetDetailAsync(CurrentRecord.Id);
            menuList = await menuListService.GetListAsync();

            foreach (var item in menuList)
            {
                item.Name = item.ParentCode == null ? item.Name : $"　➭ {item.Name}";

                bool isAuth = false;
                if (authorityDetail != null)
                {
                    var result = authorityDetail.Where(x => x.MenuCode == item.Code);
                    if (result.Count() > 0) isAuth = true;
                }

                myAuthLists.Add(new MyAuthList
                {
                    Code = item.Code,
                    isChecked = isAuth,
                    ParentCode = item.ParentCode
                });
            }
        }

        public async Task RemoveThisRecord(bool NeedDelete)
        {
            if (NeedDelete == true)
            {
                await CurrentService.DeleteAsync(mapper.Map<Authority>(CurrentNeedDeleteRecord));
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
                    await CurrentService.AddAsync(mapper.Map<Authority>(CurrentRecord), myAuthLists);
                    Grid.Refresh();
                }
                else
                {
                    await CurrentService.UpdateAsync(mapper.Map<Authority>(CurrentRecord), myAuthLists);
                    Grid.Refresh();
                }
                isVisibleRecord = false;
            }
        }

        public async Task HandleValidSubmit() { }

        public bool isChecked(string _code)
        {
            return MenuHelper.必要選單.Contains(_code) ?
                    true :
                    myAuthLists.Where(x => x.Code == _code).FirstOrDefault().isChecked;
        }

        public void CheckboxClicked(string _code, object checkedValue)
        {
            SetAuthStatus(_code, (bool)checkedValue);
        }

        private void SetAuthStatus(string _code, bool _isCheck)
        {
            MyAuthList find = myAuthLists.FirstOrDefault(x => x.Code == _code);

            if (find != null)
            {
                if (find.ParentCode != null)
                {
                    myAuthLists.FirstOrDefault(x => x.Code == _code).isChecked = _isCheck;
                    if (_isCheck)
                    {
                        if (!myAuthLists.FirstOrDefault(x => x.Code == find.ParentCode).isChecked)
                        {
                            myAuthLists.FirstOrDefault(x => x.Code == find.ParentCode).isChecked = _isCheck;
                        }
                    }
                    else
                    {
                        var check = myAuthLists.Where(x => x.ParentCode == find.ParentCode).ToList();
                        bool unCheck = false;
                        foreach (var item in check)
                        {
                            if (item.isChecked)
                            {
                                unCheck = true;
                                break;
                            }
                        }
                        myAuthLists.FirstOrDefault(x => x.Code == find.ParentCode).isChecked = unCheck;
                    }
                }
                else
                {
                    List<MyAuthList> findKid = myAuthLists
                        .Where(x => (x.ParentCode == _code) || x.Code == _code)
                        .ToList();

                    foreach (var item in findKid)
                    {
                        item.isChecked = _isCheck;
                    }
                }
            }
        }

        public bool IsDisabled(string _code)
        {
            return MenuHelper.必要選單.Contains(_code) ? true : false;
        }
        #endregion

        public class MyAuthList
        {
            public string Code { get; set; }
            public bool isChecked { get; set; }
            public string ParentCode { get; set; }
        }
    }
}
