using DBTest.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InspectionBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using InspectionBlazor.RazorModels;
using InspectionBlazor.Helpers;
using Database.Models.Models;

namespace DBTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddSyncfusionBlazor();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            

            services.AddDbContext<InspectionDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("InspectionDBContext")),
                    ServiceLifetime.Transient);

            RegisterInspectionService(services);

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

        }

        private static void RegisterInspectionService(IServiceCollection services)
        {
            #region µù¥UªA°È
            services.AddTransient<WorkLogService>();
            services.AddTransient<CircularGaugeService>();
            services.AddTransient<PersonDepartmentService>();
            services.AddTransient<PersonManagerService>();
            services.AddTransient<FormMappingService>();
            services.AddTransient<FormPathService>();
            services.AddTransient<FormReportService>();
            services.AddTransient<NoInspectCalendarService>();
            // services.AddTransient<FaultImproveService>();
            // services.AddTransient<ManagerApprovalService>();
            services.AddTransient<AuditService>();
            // services.AddTransient<ImageRepositoryService>();
            services.AddTransient<EquipmentExamItemTemplateService>();
            services.AddTransient<EquipmentTemplateService>();
            services.AddTransient<PersonService>();
            services.AddTransient<VirtualEquipmentExamItemService>();
            services.AddTransient<EquipmentExamService>();
            services.AddTransient<PatrolPlaceService>();
            services.AddTransient<SectionService>();
            services.AddTransient<EquipmentService>();
            services.AddTransient<PatrolPathService>();
            services.AddTransient<PatrolScopeService>();
            services.AddTransient<PatrolPathScopeService>();
            services.AddTransient<EquipmentExamItemService>();
            // services.AddTransient<PatrolPathPeriodService>();
            services.AddTransient<PatrolPathNplaceService>();
            services.AddTransient<BulletinService>();
            services.AddTransient<AuthorityService>();
            services.AddTransient<MenuListService>();
            services.AddTransient<JobTitleService>();
            services.AddTransient<DepartmentService>();
            services.AddTransient<AssignedMattersService>();
            services.AddTransient<InspectionRecordService>();
            services.AddTransient<InspectionRecordStatisticsService>();
            services.AddTransient<DaliyAbnormalService>();
            services.AddTransient<EquipmentCategoryService>();
            services.AddTransient<EquipmentCategoryPartsService>();
            services.AddTransient<RemakerService>();
            services.AddTransient<EquipmentBasicService>();
            services.AddTransient<EquipmentComponentService>();
            //services.AddTransient<EquipmentMaintenService>();
            //services.AddTransient<EquipmentFixService>();
            // services.AddTransient<EquipmentFileService>();
            // services.AddTransient<DocRepositoryService>();
            services.AddTransient<EquipmentTrendChartService>();
            services.AddTransient<MobileDeviceService>();
            // services.AddTransient<OutComeEditService>();
            services.AddTransient<HandyMasterService>();
            services.AddTransient<HandyDetailService>();
            services.AddTransient<CanMessageService>();
            services.AddTransient<FaultImproveDetailService>();
            services.AddTransient<PadSettingsService>();
            services.AddTransient<ExpandService>();
            services.AddTransient<UpdateFileService>();
            services.AddTransient<PadMessageService>();
            services.AddTransient<PlaceStayTimeService>();
            services.AddTransient<ExceptionMessageService>();
            services.AddTransient<SmtpService>();
            services.AddTransient<PersonnelChangeService>();
            services.AddTransient<WorkTypeService>();
            services.AddTransient<ContractorShiftService>();
            services.AddTransient<LeaveTypeService>();
            services.AddTransient<AttendanceRegisterService>();
            services.AddTransient<WorkingPlanCollectionService>();
            services.AddTransient<ShiftSchedulingRulesService>();
            services.AddTransient<ContractEmployeesService>();
            // services.AddTransient<InspcetMapService>();
            services.AddTransient<PatrolGroupService>();
            services.AddTransient<PatrolGroupPathService>();
            services.AddTransient<PartInfoService>();
            // services.AddTransient<PFMasterService>();
            services.AddTransient<PartLocationInfoService>();
            // services.AddTransient<MWMasterService>();
            // services.AddTransient<CourseStatisticsService>();
            // services.AddTransient<TargetDashboardService>();
            services.AddTransient<FireFightReportSaveService>();
            services.AddTransient<SituationDashboardService>();
            services.AddTransient<WorkScheduleService>();
            services.AddTransient<OldInspectionRecordAllService>();
            services.AddTransient<RepairEquipmentService>();
            services.AddTransient<RepairEquipmentGroupService>();
            services.AddTransient<RepairEquipmentMasterService>();
            services.AddTransient<RepairEquipmentPersonService>();
            services.AddTransient<RepairEquipmentGroupService>();
            services.AddTransient<RepairEquipmentMasterService>();
            services.AddTransient<RepairEquipmentPersonService>();
            services.AddTransient<EquipmentMaintainCycleService>();
            services.AddTransient<RepairRemakerService>();
            services.AddTransient<RepairEquipmentRepairRemakerService>();
            services.AddTransient<RepairMasterService>();
            services.AddTransient<StockChangeHistoryService>();
            #endregion

            #region µù¥U Razor Model
            services.AddTransient<JobTitleRazorModel>();
            services.AddTransient<AuthorityRazorModel>();
            services.AddTransient<PersonRazorModel>();
            services.AddTransient<PersonnelChangeRazorModel>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTUwNzcxQDMxMzkyZTMyMmUzMEdrSDBDTnF5aTBYbW0zMlY5QTBZQThINlRIRG42M1NvTG1LaXIyNTRPdTg9");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
