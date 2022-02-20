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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using xfPatrolDto.Dtos;
using InspectionShare.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

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
            #region 加入使用 Cookie & JWT 認證需要的宣告
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Tokens:ValidIssuer"],
                        ValidAudience = Configuration["Tokens:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:IssuerSigningKey"])),
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = context.Exception.Message;
                            APIResult apiResult = JWTTokenFailHelper.GetFailResult(context.Exception);

                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResult));
                            return;
                        },
                        OnChallenge = context =>
                        {
                            //context.HandleResponse();
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " +
                                context.SecurityToken);
                            return Task.CompletedTask;
                        }

                    };
                });
            #endregion

            #region 多國語言設定
            services.AddControllers();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSyncfusionBlazor();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // define the list of cultures your app will support
                var supportedCultures = new List<CultureInfo>() {
                    new CultureInfo("en-US"),
                    new CultureInfo("zh")
                };

                // set the default culture
                options.DefaultRequestCulture = new RequestCulture("zh-TW");

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SFLocalizer));
            #endregion

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
            #region 註冊服務
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

            #region 註冊 Razor Model
            services.AddTransient<JobTitleRazorModel>();
            services.AddTransient<AuthorityRazorModel>();
            services.AddTransient<PersonRazorModel>();
            services.AddTransient<PersonnelChangeRazorModel>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region 多國語言設定
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);
            #endregion

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

            #region 指定要使用 Cookie & 使用者認證的中介軟體
            app.UseCookiePolicy();
            app.UseAuthentication();
            #endregion

            #region 指定使用授權檢查的中介軟體
            app.UseAuthorization();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
