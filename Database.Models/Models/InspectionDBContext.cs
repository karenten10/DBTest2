
using Database.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Models.Models
{
    public partial class InspectionDBContext : DbContext
    {
        public InspectionDBContext()
        {
        }

        public InspectionDBContext(DbContextOptions<InspectionDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignedMatters> AssignedMatters { get; set; }
        public virtual DbSet<AttendanceRegister> AttendanceRegister { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Authority> Authority { get; set; }
        public virtual DbSet<AuthorityDetail> AuthorityDetail { get; set; }
        public virtual DbSet<Bulletin> Bulletin { get; set; }
        public virtual DbSet<CanMessage> CanMessage { get; set; }
        public virtual DbSet<Checkin> Checkin { get; set; }
        public virtual DbSet<CheckinConflictn> CheckinConflictn { get; set; }
        public virtual DbSet<ContractEmployees> ContractEmployees { get; set; }
        public virtual DbSet<ContractorShift> ContractorShift { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DocRepository> DocRepository { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentBasic> EquipmentBasic { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<EquipmentCategoryParts> EquipmentCategoryParts { get; set; }
        public virtual DbSet<EquipmentComponent> EquipmentComponent { get; set; }
        public virtual DbSet<EquipmentExam> EquipmentExam { get; set; }
        public virtual DbSet<EquipmentExamItem> EquipmentExamItem { get; set; }
        public virtual DbSet<EquipmentExamItemTemplate> EquipmentExamItemTemplate { get; set; }
        public virtual DbSet<EquipmentFile> EquipmentFile { get; set; }
        public virtual DbSet<EquipmentFix> EquipmentFix { get; set; }
        public virtual DbSet<EquipmentMainten> EquipmentMainten { get; set; }
        public virtual DbSet<EquipmentPart> EquipmentPart { get; set; }
        public virtual DbSet<EquipmentTemplate> EquipmentTemplate { get; set; }
        public virtual DbSet<ExceptionMessage> ExceptionMessage { get; set; }
        public virtual DbSet<Expand> Expand { get; set; }
        public virtual DbSet<ExpandAllowDay> ExpandAllowDay { get; set; }
        public virtual DbSet<ExpandExamItem> ExpandExamItem { get; set; }
        public virtual DbSet<ExpandPlace> ExpandPlace { get; set; }
        public virtual DbSet<FaultImprove> FaultImprove { get; set; }
        public virtual DbSet<FaultImproveDetail> FaultImproveDetail { get; set; }
        public virtual DbSet<FaultImproveInbox> FaultImproveInbox { get; set; }
        public virtual DbSet<FormMapping> FormMapping { get; set; }
        public virtual DbSet<FormPath> FormPath { get; set; }
        public virtual DbSet<FormReport> FormReport { get; set; }
        public virtual DbSet<FormTypeInfo> FormTypeInfo { get; set; }
        public virtual DbSet<HandyDetail> HandyDetail { get; set; }
        public virtual DbSet<HandyMaster> HandyMaster { get; set; }
        public virtual DbSet<ImageRepository> ImageRepository { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<LeaveType> LeaveType { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<MailQueue> MailQueue { get; set; }
        public virtual DbSet<MailQueueHistory> MailQueueHistory { get; set; }
        public virtual DbSet<ManagerApproval> ManagerApproval { get; set; }
        public virtual DbSet<ManagerApprovalDetail> ManagerApprovalDetail { get; set; }
        public virtual DbSet<ManagerApprovalInbox> ManagerApprovalInbox { get; set; }
        public virtual DbSet<MenuList> MenuList { get; set; }
        public virtual DbSet<MobileDevice> MobileDevice { get; set; }
        public virtual DbSet<Mwdetail> Mwdetail { get; set; }
        public virtual DbSet<Mwmaster> Mwmaster { get; set; }
        public virtual DbSet<NoInspectCalendar> NoInspectCalendar { get; set; }
        public virtual DbSet<NoInspectCalendarNpatrolPlace> NoInspectCalendarNpatrolPlace { get; set; }
        public virtual DbSet<OutCome> OutCome { get; set; }
        public virtual DbSet<OutComeHistory> OutComeHistory { get; set; }
        public virtual DbSet<OutcomeConflict> OutcomeConflict { get; set; }
        public virtual DbSet<PadMessage> PadMessage { get; set; }
        public virtual DbSet<PadSettings> PadSettings { get; set; }
        public virtual DbSet<PartInfo> PartInfo { get; set; }
        public virtual DbSet<PartLocationInfo> PartLocationInfo { get; set; }
        public virtual DbSet<PartQtyInit> PartQtyInit { get; set; }
        public virtual DbSet<PatrolGroup> PatrolGroup { get; set; }
        public virtual DbSet<PatrolGroupNpath> PatrolGroupNpath { get; set; }
        public virtual DbSet<PatrolPath> PatrolPath { get; set; }
        public virtual DbSet<PatrolPathNplace> PatrolPathNplace { get; set; }
        public virtual DbSet<PatrolPathPeriod> PatrolPathPeriod { get; set; }
        public virtual DbSet<PatrolPathPeriodNexamItem> PatrolPathPeriodNexamItem { get; set; }
        public virtual DbSet<PatrolPathPeriodNplace> PatrolPathPeriodNplace { get; set; }
        public virtual DbSet<PatrolPathScope> PatrolPathScope { get; set; }
        public virtual DbSet<PatrolPlace> PatrolPlace { get; set; }
        public virtual DbSet<PatrolScope> PatrolScope { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonDepartment> PersonDepartment { get; set; }
        public virtual DbSet<PersonManager> PersonManager { get; set; }
        public virtual DbSet<PersonnelChange> PersonnelChange { get; set; }
        public virtual DbSet<PersonnelChangePeople> PersonnelChangePeople { get; set; }
        public virtual DbSet<Pfdetail> Pfdetail { get; set; }
        public virtual DbSet<Pfmaster> Pfmaster { get; set; }
        public virtual DbSet<PlaceStayTime> PlaceStayTime { get; set; }
        public virtual DbSet<Remaker> Remaker { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<ShiftSchedulingRules> ShiftSchedulingRules { get; set; }
        public virtual DbSet<ShiftSchedulingRulesPeople> ShiftSchedulingRulesPeople { get; set; }
        public virtual DbSet<Smtp> Smtp { get; set; }
        public virtual DbSet<StrengthenInspection> StrengthenInspection { get; set; }
        public virtual DbSet<SystemEnvironment> SystemEnvironment { get; set; }
        public virtual DbSet<UpdateFile> UpdateFile { get; set; }
        public virtual DbSet<VirtualEquipmentExamItem> VirtualEquipmentExamItem { get; set; }
        public virtual DbSet<WalkthroughShiftSchedule> WalkthroughShiftSchedule { get; set; }
        public virtual DbSet<WorkLog> WorkLog { get; set; }
        public virtual DbSet<WorkType> WorkType { get; set; }
        public virtual DbSet<WorkTypePeople> WorkTypePeople { get; set; }
        public virtual DbSet<WorkingPlanCollection> WorkingPlanCollection { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedule { get; set; }
        public virtual DbSet<FireFightReportSave> FireFightReportSave { get; set; }
        public virtual DbSet<PatrolPathNequipmentTemp> PatrolPathNequipmentTemp { get; set; }
        public virtual DbSet<RepairEquipmentGroup> RepairEquipmentGroup { get; set; }
        public virtual DbSet<RepairEquipment> RepairEquipment { get; set; }
        public virtual DbSet<RepairEquipmentNPerson> RepairEquipmentNPerson { get; set; }
        public virtual DbSet<RepairRemaker> RepairRemaker { get; set; }
        public virtual DbSet<RepairEquipmentNRepairRemark> RepairEquipmentNRepairRemark { get; set; }
        public virtual DbSet<RepairMaster> RepairMaster { get; set; }
        public virtual DbSet<RepairManagerApproval> RepairManagerApproval { get; set; }
        public virtual DbSet<RepairManagerApprovalInbox> RepairManagerApprovalInbox { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedMatters>(entity =>
            {
                entity.Property(e => e.AnnounceTime).HasColumnType("datetime");

                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<AttendanceRegister>(entity =>
            {
                entity.Property(e => e.AttendanceDate).HasColumnType("datetime");

                entity.Property(e => e.ContractorShiftSegment).HasMaxLength(10);

                entity.Property(e => e.LeaveTypeSegment).HasMaxLength(10);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.HasOne(d => d.ContractorShift)
                    .WithMany(p => p.AttendanceRegister)
                    .HasForeignKey(d => d.ContractorShiftId)
                    .HasConstraintName("FK_AttendanceRegister_REF_ContractorShift");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.AttendanceRegister)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .HasConstraintName("FK_AttendanceRegister_REF_LeaveType");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.AttendanceRegister)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceRegister_REF_Person");

                entity.HasOne(d => d.WorkType)
                    .WithMany(p => p.AttendanceRegister)
                    .HasForeignKey(d => d.WorkTypeId)
                    .HasConstraintName("FK_AttendanceRegister_REF_WorkType");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditName).IsRequired();

                entity.Property(e => e.ForPaths).IsRequired();
            });

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AuthorityDetail>(entity =>
            {
                entity.Property(e => e.MenuCode).IsRequired();

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.AuthorityDetail)
                    .HasForeignKey(d => d.AuthorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthorityDetail_REF_Authority");
            });

            modelBuilder.Entity<Bulletin>(entity =>
            {
                entity.Property(e => e.AnnounceTime).HasColumnType("datetime");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<CanMessage>(entity =>
            {
                entity.Property(e => e.Message).HasMaxLength(512);

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('F')");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.CanMessage)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CanMessage_Create_REF_Person");
            });

            modelBuilder.Entity<Checkin>(entity =>
            {
                entity.HasKey(e => e.ExpandPlaceId);

                entity.Property(e => e.ExpandPlaceId).ValueGeneratedNever();

                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否已完成");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("輸入時間");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.Checkin)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkin_REF_Expand");

                entity.HasOne(d => d.ExpandPlace)
                    .WithOne(p => p.Checkin)
                    .HasForeignKey<Checkin>(d => d.ExpandPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkin_REF_ExpandPlace");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.Checkin)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkin_REF_PatrolPath");
            });

            modelBuilder.Entity<CheckinConflictn>(entity =>
            {
                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否已完成");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("輸入時間");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.CheckinConflictn)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckinConflictn_REF_Expand");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.CheckinConflictn)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckinConflictn_REF_PatrolPath");
            });

            modelBuilder.Entity<ContractEmployees>(entity =>
            {
                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ContractEmployees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractEmployees_REF_Department");
            });

            modelBuilder.Entity<ContractorShift>(entity =>
            {
                entity.Property(e => e.CrossDay)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Hours).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.ShiftName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.UserDeptId)
                    .HasName("idx1_Department");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(48);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.UserDeptId).HasMaxLength(50);
            });

            modelBuilder.Entity<DocRepository>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.FileExtension).HasMaxLength(4);

                entity.Property(e => e.Filename).HasMaxLength(50);

                entity.Property(e => e.FilenameOriginal).HasMaxLength(50);

                entity.Property(e => e.Folder).HasMaxLength(4);

                entity.Property(e => e.Title).HasMaxLength(256);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentIdExt)
                    .IsRequired()
                    .HasColumnName("EquipmentId_Ext")
                    .HasComment("設備代碼(外)");

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasComment("設備名稱");

                entity.Property(e => e.EquipmentTemplateId).HasComment("參考設備範本");

                entity.Property(e => e.SectionId).HasComment("區段代碼");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");

                entity.HasOne(d => d.EquipmentTemplate)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentTemplateId)
                    .HasConstraintName("FK_Equipment_REF_EquipmentTemplate");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.PatrolPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipment_REF_PatrolPlace");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipment_REF_Section");
            });

            modelBuilder.Entity<EquipmentBasic>(entity =>
            {
                entity.Property(e => e.Capacity).HasMaxLength(256);

                entity.Property(e => e.CheckDate).HasColumnType("datetime");

                entity.Property(e => e.EnglishName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.EquipmentStatus).HasMaxLength(50);

                entity.Property(e => e.EquipmentValue).HasMaxLength(256);

                entity.Property(e => e.MaintenRemark).HasMaxLength(256);

                entity.Property(e => e.Manufacturer).HasMaxLength(256);

                entity.Property(e => e.Remark).HasMaxLength(512);

                entity.Property(e => e.SerialNo).HasMaxLength(256);

                entity.Property(e => e.Spec).HasMaxLength(256);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Tag).HasMaxLength(256);

                entity.Property(e => e.WarrantyPeriod).HasColumnType("datetime");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentBasic)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentBasic_REF_Equipment");

                entity.HasOne(d => d.MaintenDepartmentNavigation)
                    .WithMany(p => p.EquipmentBasicMaintenDepartmentNavigation)
                    .HasForeignKey(d => d.MaintenDepartment)
                    .HasConstraintName("FK_EquipmentBasic_Mainten_REF_Department");

                entity.HasOne(d => d.MaintenPersonNavigation)
                    .WithMany(p => p.EquipmentBasicMaintenPersonNavigation)
                    .HasForeignKey(d => d.MaintenPerson)
                    .HasConstraintName("FK_EquipmentBasic_Mainten_REF_Person");

                entity.HasOne(d => d.OwnerDepartmentNavigation)
                    .WithMany(p => p.EquipmentBasicOwnerDepartmentNavigation)
                    .HasForeignKey(d => d.OwnerDepartment)
                    .HasConstraintName("FK_EquipmentBasic_Owner_REF_Department");

                entity.HasOne(d => d.OwnerPersonNavigation)
                    .WithMany(p => p.EquipmentBasicOwnerPersonNavigation)
                    .HasForeignKey(d => d.OwnerPerson)
                    .HasConstraintName("FK_EquipmentBasic_Owner_REF_Person");
            });

            modelBuilder.Entity<EquipmentCategory>(entity =>
            {
                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("廠牌");

                entity.Property(e => e.ChineseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("設備類別名稱(中文)");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(50)
                    .HasComment("設備類別名稱(英文)");

                entity.Property(e => e.EquipmentCategoryExtId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("設備類別外部編號");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("型號");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasComment("備註");
            });

            modelBuilder.Entity<EquipmentCategoryParts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EquipmentCategoryId).HasComment("設備類別內部編號");

                entity.Property(e => e.MaterialNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("材料編號");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("組件名稱");

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasColumnName("specification")
                    .HasMaxLength(100)
                    .HasComment("規格");

                entity.HasOne(d => d.EquipmentCategory)
                    .WithMany(p => p.EquipmentCategoryParts)
                    .HasForeignKey(d => d.EquipmentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentCategoryParts_REF_EquipmentCategory");
            });

            modelBuilder.Entity<EquipmentComponent>(entity =>
            {
                entity.Property(e => e.ChangeRecord).HasMaxLength(512);

                entity.Property(e => e.Component).HasMaxLength(512);

                entity.Property(e => e.ImageNo).HasMaxLength(512);

                entity.Property(e => e.MaterialNo).HasMaxLength(512);

                entity.Property(e => e.PartName).HasMaxLength(512);

                entity.Property(e => e.Specification).HasMaxLength(512);

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentComponent)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentComponent_REF_Equipment");
            });

            modelBuilder.Entity<EquipmentExam>(entity =>
            {
                entity.Property(e => e.PatrolPathId).HasComment("巡檢路線代碼");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentExam)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExam_REF_Equipment");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.EquipmentExam)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExam_REF_PatrolPath");
            });

            modelBuilder.Entity<EquipmentExamItem>(entity =>
            {
                entity.HasIndex(e => e.Guid);

                entity.Property(e => e.DiffJudgeStardand)
                    .HasMaxLength(20)
                    .HasComment("差距範圍判斷標準*等於、大於、小於");

                entity.Property(e => e.EquipmentId).HasComment("檢查設備代碼");

                entity.Property(e => e.ExamConditionName)
                    .HasMaxLength(50)
                    .HasComment("紀錄審核*0.無審核依據,1.等於 2.小於 3.大於 4.範圍 5.差距 6.標準 7.小於等於 8.大於等於");

                entity.Property(e => e.ExamMethod)
                    .HasMaxLength(50)
                    .HasComment("檢查方法");

                entity.Property(e => e.LowerLimit)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("輸入範圍下限");

                entity.Property(e => e.MustPhoto)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasComment("設備檢查項目名稱");

                entity.Property(e => e.Name1)
                    .HasMaxLength(20)
                    .HasComment("名稱1");

                entity.Property(e => e.Name2)
                    .HasMaxLength(20)
                    .HasComment("名稱2");

                entity.Property(e => e.Name3)
                    .HasMaxLength(20)
                    .HasComment("名稱3");

                entity.Property(e => e.Name4)
                    .HasMaxLength(20)
                    .HasComment("名稱4");

                entity.Property(e => e.Name5)
                    .HasMaxLength(20)
                    .HasComment("名稱5");

                entity.Property(e => e.OrderId).HasComment("順序");

                entity.Property(e => e.PatrolPlaceId).HasComment("所屬巡檢點");

                entity.Property(e => e.Score).HasComment("評分");

                entity.Property(e => e.Sinspection)
                    .HasColumnName("SInspection")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("加強巡檢*Y/N");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("啟用/停用*YN");

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .HasComment("單位");

                entity.Property(e => e.UpperLimit)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("輸入範圍上限");

                entity.Property(e => e.Value1)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("數值1");

                entity.Property(e => e.Value2)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("數值1");

                entity.Property(e => e.WarningMessage)
                    .HasMaxLength(200)
                    .HasComment("異常警示訊息");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentExamItem)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExamItem_REF_Equipment");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.EquipmentExamItem)
                    .HasForeignKey(d => d.PatrolPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExamItem_REF_PatrolPlace");

                entity.HasOne(d => d.WorkType)
                    .WithMany(p => p.EquipmentExamItem)
                    .HasForeignKey(d => d.WorkTypeId)
                    .HasConstraintName("FK_EquipmentExamItem_REF_WorkType");
            });

            modelBuilder.Entity<EquipmentExamItemTemplate>(entity =>
            {
                entity.HasIndex(e => e.Guid);

                entity.Property(e => e.DiffJudgeStardand).HasMaxLength(20);

                entity.Property(e => e.ExamConditionName).HasMaxLength(50);

                entity.Property(e => e.ExamMethod).HasMaxLength(50);

                entity.Property(e => e.LowerLimit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Name1).HasMaxLength(20);

                entity.Property(e => e.Name2).HasMaxLength(20);

                entity.Property(e => e.Name3).HasMaxLength(20);

                entity.Property(e => e.Name4).HasMaxLength(20);

                entity.Property(e => e.Name5).HasMaxLength(20);

                entity.Property(e => e.Sinspection)
                    .HasColumnName("SInspection")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Unit).HasMaxLength(20);

                entity.Property(e => e.UpperLimit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Value1).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Value2).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.WarningMessage).HasMaxLength(200);

                entity.HasOne(d => d.EquipmentTemplate)
                    .WithMany(p => p.EquipmentExamItemTemplate)
                    .HasForeignKey(d => d.EquipmentTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExamItemTemplate_REF_EquipmentTemplate");
            });

            modelBuilder.Entity<EquipmentFile>(entity =>
            {
                entity.Property(e => e.FileKind).HasMaxLength(256);

                entity.Property(e => e.FileName).HasMaxLength(256);

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentFile)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentFile_REF_Equipment");
            });

            modelBuilder.Entity<EquipmentFix>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EquipmentItem).HasMaxLength(256);

                entity.Property(e => e.EquipmentName).HasMaxLength(256);

                entity.Property(e => e.EstimateDate).HasColumnType("datetime");

                entity.Property(e => e.FixRemark).HasMaxLength(512);

                entity.Property(e => e.FixStatus).HasMaxLength(256);

                entity.HasOne(d => d.CreatePersonNavigation)
                    .WithMany(p => p.EquipmentFixCreatePersonNavigation)
                    .HasForeignKey(d => d.CreatePerson)
                    .HasConstraintName("FK_EquipmentFix_Create_REF_Person");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentFix)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentFix_REF_Equipment");

                entity.HasOne(d => d.ToDepartmentNavigation)
                    .WithMany(p => p.EquipmentFix)
                    .HasForeignKey(d => d.ToDepartment)
                    .HasConstraintName("FK_EquipmentFix_To_REF_Department");

                entity.HasOne(d => d.ToPersonNavigation)
                    .WithMany(p => p.EquipmentFixToPersonNavigation)
                    .HasForeignKey(d => d.ToPerson)
                    .HasConstraintName("FK_EquipmentFix_To_REF_Person");
            });

            modelBuilder.Entity<EquipmentMainten>(entity =>
            {
                entity.Property(e => e.MaintenDate).HasColumnType("datetime");

                entity.Property(e => e.MaintenEquipment).HasMaxLength(256);

                entity.Property(e => e.MaintenItem).HasMaxLength(256);

                entity.Property(e => e.MaintenPath).HasMaxLength(256);

                entity.Property(e => e.MaintenPeriod).HasMaxLength(256);

                entity.Property(e => e.MaintenPicture).HasMaxLength(512);

                entity.Property(e => e.MaintenScope).HasMaxLength(256);

                entity.Property(e => e.MaintenSpot).HasMaxLength(256);

                entity.Property(e => e.MaintenStandard).HasMaxLength(256);

                entity.Property(e => e.MaintenValue).HasMaxLength(256);

                entity.Property(e => e.RecordAudit).HasMaxLength(512);

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentMainten)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentMainten_REF_Equipment");

                entity.HasOne(d => d.MaintenPersonNavigation)
                    .WithMany(p => p.EquipmentMainten)
                    .HasForeignKey(d => d.MaintenPerson)
                    .HasConstraintName("FK_EquipmentMainten_Mainten_REF_Person");
            });

            modelBuilder.Entity<EquipmentPart>(entity =>
            {
                entity.HasIndex(e => new { e.EquipmentId, e.PartId })
                    .HasName("AK1_EquipmentPart")
                    .IsUnique();

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.EquipmentPart)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentPart_REF_PartInfo");
            });

            modelBuilder.Entity<EquipmentTemplate>(entity =>
            {
                entity.Property(e => e.EquipmentIdExt)
                    .IsRequired()
                    .HasColumnName("EquipmentId_Ext");

                entity.Property(e => e.EquipmentName).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<ExceptionMessage>(entity =>
            {
                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DeviceName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DeviceType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DeviceVersion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ExceptionDetail)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.ExceptionTime).HasColumnType("datetime");

                entity.Property(e => e.ExceptionType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Function)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Expand>(entity =>
            {
                entity.HasIndex(e => e.BeginTime);

                entity.HasIndex(e => e.EndTime);

                entity.Property(e => e.BeginTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.Expand)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expand_REF_PatrolPath");

                entity.HasOne(d => d.PatrolPathPeriod)
                    .WithMany(p => p.Expand)
                    .HasForeignKey(d => d.PatrolPathPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Expand_REF_PatrolPathPeriod");
            });

            modelBuilder.Entity<ExpandAllowDay>(entity =>
            {
                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.ExpandAllowDay)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpandAllowDay_REF_Expand");
            });

            modelBuilder.Entity<ExpandExamItem>(entity =>
            {
                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("該項目是否巡檢完成");

                entity.HasOne(d => d.EquipmentExamItem)
                    .WithMany(p => p.ExpandExamItem)
                    .HasForeignKey(d => d.EquipmentExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpandExamItem_REF_EquipmentExamItem");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.ExpandExamItem)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpandExamItem_REF_Expand");
            });

            modelBuilder.Entity<ExpandPlace>(entity =>
            {
                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ServerUpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.ExpandPlace)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpandPlace_REF_Expand");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.ExpandPlace)
                    .HasForeignKey(d => d.PatrolPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExpandPlace_REF_PatrolPlace");
            });

            modelBuilder.Entity<FaultImprove>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.ManagerApproval)
                    .WithMany(p => p.FaultImprove)
                    .HasForeignKey(d => d.ManagerApprovalId)
                    .HasConstraintName("FK_FaultImprove_REF_ManagerApproval");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_FaultImprove_REF_FaultImprove");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.FaultImprove)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImprove_REF_Person");
            });

            modelBuilder.Entity<FaultImproveDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpectedFinishDate).HasColumnType("datetime");

                entity.Property(e => e.FaultImproveGuid)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.FaultImproveDetail)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImproveDetail_REF_Department");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.FaultImproveDetail)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImproveDetail_REF_Equipment");

                entity.HasOne(d => d.ExamItem)
                    .WithMany(p => p.FaultImproveDetail)
                    .HasForeignKey(d => d.ExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImproveDetail_REF_EquipmentExamItem");
            });

            modelBuilder.Entity<FaultImproveInbox>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsApproval)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.HasOne(d => d.FaultImprove)
                    .WithMany(p => p.FaultImproveInbox)
                    .HasForeignKey(d => d.FaultImproveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImproveInbox_REF_FaultImprove");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.FaultImproveInbox)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FaultImproveInbox_REF_Person");
            });

            modelBuilder.Entity<FormMapping>(entity =>
            {
                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.FormMapping)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormMapping_REF_Equipment");

                entity.HasOne(d => d.FormReport)
                    .WithMany(p => p.FormMapping)
                    .HasForeignKey(d => d.FormReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentExam_REF_FormReport");
            });

            modelBuilder.Entity<FormPath>(entity =>
            {
                entity.HasOne(d => d.FormReport)
                    .WithMany(p => p.FormPath)
                    .HasForeignKey(d => d.FormReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormPath_REF_FormReport");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.FormPath)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormPath_REF_PatrolPath");
            });

            modelBuilder.Entity<FormReport>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Iso).HasColumnName("ISO");

                entity.Property(e => e.No).HasMaxLength(50);

                entity.Property(e => e.Period)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('D')");
            });

            modelBuilder.Entity<FormTypeInfo>(entity =>
            {
                entity.HasKey(e => e.FormType)
                    .HasName("PK_FORMTYPEINFO");

                entity.Property(e => e.FormType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FormName).HasMaxLength(50);
            });

            modelBuilder.Entity<HandyDetail>(entity =>
            {
                entity.Property(e => e.Memo).HasMaxLength(512);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.HandyMaster)
                    .WithMany(p => p.HandyDetail)
                    .HasForeignKey(d => d.HandyMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HandyDetail_Master_REF_HandyMaster");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.HandyDetail)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HandyDetail_Manager_REF_Person");
            });

            modelBuilder.Entity<HandyMaster>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Location).HasMaxLength(256);

                entity.Property(e => e.Memo).HasMaxLength(512);

                entity.Property(e => e.Photo1).HasMaxLength(256);

                entity.Property(e => e.ServerUpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Create)
                    .WithMany(p => p.HandyMaster)
                    .HasForeignKey(d => d.CreateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HandyMaster_Create_REF_Person");
            });

            modelBuilder.Entity<ImageRepository>(entity =>
            {
                entity.Property(e => e.FileExtension).HasMaxLength(4);

                entity.Property(e => e.Filename).HasMaxLength(50);

                entity.Property(e => e.Folder).HasMaxLength(4);
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.LeaveName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.Property(e => e.LoginTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MailQueue>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.MailAddress).IsRequired();

                entity.Property(e => e.SentStatus).HasMaxLength(1);

                entity.Property(e => e.Subject).IsRequired();
            });

            modelBuilder.Entity<MailQueueHistory>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.MailAddress).IsRequired();

                entity.Property(e => e.SentStatus).HasMaxLength(1);

                entity.Property(e => e.Subject).IsRequired();
            });

            modelBuilder.Entity<ManagerApproval>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Memo).HasMaxLength(1024);

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.ManagerApproval)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApproval_REF_Expand");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ManagerApproval_REF_ManagerApproval");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ManagerApproval)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApproval_REF_Person");
            });

            modelBuilder.Entity<ManagerApprovalDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DoubleCheck)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.ManagerApprovalGuid)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.StrengthenInspection)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.HasOne(d => d.ExpandExamItem)
                    .WithMany(p => p.ManagerApprovalDetail)
                    .HasForeignKey(d => d.ExpandExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApprovalDetail_REF_Outcome");

                entity.HasOne(d => d.ExpandExamItemNavigation)
                    .WithMany(p => p.ManagerApprovalDetail)
                    .HasForeignKey(d => d.ExpandExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApprovalDetail_REF_ExpandExamItem");
            });

            modelBuilder.Entity<ManagerApprovalInbox>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsApproval)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.HasOne(d => d.ManagerApproval)
                    .WithMany(p => p.ManagerApprovalInbox)
                    .HasForeignKey(d => d.ManagerApprovalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApprovalInbox_REF_ManagerApproval");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ManagerApprovalInbox)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerApprovalInbox_REF_Person");
            });

            modelBuilder.Entity<MenuList>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ParentCode).HasMaxLength(200);
            });

            modelBuilder.Entity<MobileDevice>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(256);

                entity.Property(e => e.Mac).HasMaxLength(256);

                entity.Property(e => e.Remark).HasMaxLength(512);
            });

            modelBuilder.Entity<Mwdetail>(entity =>
            {
                entity.ToTable("MWDetail");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.Property(e => e.QtyChange).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Mwdetail)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MWDetail_REF_MWMaster");
            });

            modelBuilder.Entity<Mwmaster>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK_MWMASTER");

                entity.ToTable("MWMaster");

                entity.Property(e => e.ApplicationDate).HasColumnType("datetime");

                entity.Property(e => e.CauseOfFailure).HasMaxLength(100);

                entity.Property(e => e.ContactTelNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasComment("逾期通知要用的");

                entity.Property(e => e.FormNumber).HasMaxLength(25);

                entity.Property(e => e.FormType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasComment("不使用代碼,使用彈跳視窗進行選擇");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.PropertyNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QtyChangeTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment(@"立單->送審給倉管->倉管確認->倉管送審到業主->業主結案
   1.立單送審給倉管,倉管可以退單
   2.若倉管確認怎扣庫存
   3.倉管確認後就只能往上送審
   4.若單子有誤可以打反向沖銷單,例如領料單和領料退回單,入料單或入料退出單
   ");

                entity.Property(e => e.VendorVatno)
                    .HasColumnName("VendorVATNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NoInspectCalendar>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<NoInspectCalendarNpatrolPlace>(entity =>
            {
                entity.ToTable("NoInspectCalendarNPatrolPlace");

                entity.HasOne(d => d.NoInspectCalendar)
                    .WithMany(p => p.NoInspectCalendarNpatrolPlace)
                    .HasForeignKey(d => d.NoInspectCalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoInspectCalendarNPatrolPlace_REF_NoInspectCalendar");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.NoInspectCalendarNpatrolPlace)
                    .HasForeignKey(d => d.PatrolPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoInspectCalendarNPatrolPlace_REF_PatrolPlace");
            });

            modelBuilder.Entity<OutCome>(entity =>
            {
                entity.HasKey(e => e.ExpandExamItemId);

                entity.Property(e => e.ExpandExamItemId).ValueGeneratedNever();

                entity.Property(e => e.EquipmentShutdown)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsAbnormal)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否已完成");

                entity.Property(e => e.IsEdit)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.MultiChoice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標準輸入結果");

                entity.Property(e => e.PersonId).HasComment("記錄人ID");

                entity.Property(e => e.ServerUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("輸入時間");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("數值輸入結果");

                entity.Property(e => e.YetMemo).HasMaxLength(512);

                entity.HasOne(d => d.ExpandExamItem)
                    .WithOne(p => p.OutCome)
                    .HasForeignKey<OutCome>(d => d.ExpandExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutCome_REF_ExpandExamItem");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.OutCome)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutCome_REF_Expand");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.OutCome)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutCome_REF_PatrolPath");
            });

            modelBuilder.Entity<OutComeHistory>(entity =>
            {
                entity.Property(e => e.EditTime).HasColumnType("datetime");

                entity.Property(e => e.EquipmentShutdown)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsAbnormal)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否已完成");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.MultiChoice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標準輸入結果");

                entity.Property(e => e.PersonId).HasComment("記錄人ID");

                entity.Property(e => e.ServerUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("輸入時間");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("數值輸入結果");

                entity.HasOne(d => d.ExpandExamItem)
                    .WithMany(p => p.OutComeHistory)
                    .HasForeignKey(d => d.ExpandExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutComeHistory_REF_ExpandExamItem");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.OutComeHistory)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutComeHistory_REF_Expand");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.OutComeHistory)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutComeHistory_REF_PatrolPath");
            });

            modelBuilder.Entity<OutcomeConflict>(entity =>
            {
                entity.Property(e => e.EquipmentShutdown)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsAbnormal)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.IsCompleted)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否已完成");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.MultiChoice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標準輸入結果");

                entity.Property(e => e.ServerUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasComment("輸入時間");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 4)")
                    .HasComment("數值輸入結果");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.OutcomeConflict)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutcomeConflict_REF_Expand");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.OutcomeConflict)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutcomeConflict_REF_PatrolPath");
            });

            modelBuilder.Entity<PadMessage>(entity =>
            {
                entity.Property(e => e.Message).HasMaxLength(512);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.PadMessage)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_PadMessage_Belong_REF_Department");
            });

            modelBuilder.Entity<PadSettings>(entity =>
            {
                entity.Property(e => e.Key).IsRequired();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<PartInfo>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK_PARTINFO");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CycleTime).HasComment("以天為單位");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PartIdNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.Specification).HasMaxLength(500);

                entity.Property(e => e.StockQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StockUnit)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<PartLocationInfo>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK_PARTLOCATIONINFO");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.LocationIdNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PartQtyInit>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK_PARTQTYINIT");

                entity.Property(e => e.PartId).ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTime).HasColumnType("datetime");

                entity.Property(e => e.QtyInit).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Part)
                    .WithOne(p => p.PartQtyInit)
                    .HasForeignKey<PartQtyInit>(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PartQtyInit_REF_PartInfo");
            });

            modelBuilder.Entity<PatrolGroup>(entity =>
            {
                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PatrolGroupNpath>(entity =>
            {
                entity.ToTable("PatrolGroupNPath");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.PatrolGroupNpath)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolGroupNPath_REF_PatrolGroup");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.PatrolGroupNpath)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolGroupNPath_REF_PatrolPath");
            });

            modelBuilder.Entity<PatrolPath>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasComment("部門ID");

                entity.Property(e => e.InspectByOrder)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("按順序檢(Y, N)");

                entity.Property(e => e.InspectByPda)
                    .IsRequired()
                    .HasColumnName("InspectByPDA")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("PDA能否檢(Y,N)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasComment("名稱");

                entity.Property(e => e.RealTimeTrans)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("即時傳輸(Y,N)");

                entity.Property(e => e.ShowRemark)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("到位後不出現備註(Y,N)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");

                entity.Property(e => e.TurnOnGps)
                    .IsRequired()
                    .HasColumnName("TurnOnGPS")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("開啟GPS(Y,N)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.PatrolPath)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_PatrolPath_REF_Department");
            });

            modelBuilder.Entity<PatrolPathNplace>(entity =>
            {
                entity.ToTable("PatrolPathNPlace");

                entity.Property(e => e.PatrolPathId).HasComment("巡檢路線ID");

                entity.Property(e => e.PatrolPlaceId).HasComment("巡檢點ID");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.PatrolPathNplace)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathNPlace_REF_PatrolPath");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.PatrolPathNplace)
                    .HasForeignKey(d => d.PatrolPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathNPlace_REF_PatrolPlace");
            });

            modelBuilder.Entity<PatrolPathPeriod>(entity =>
            {
                entity.Property(e => e.BeginDay).HasComment("該時段生效的開始時間點");

                entity.Property(e => e.BeginTime).HasComment("開始時間");

                entity.Property(e => e.Crontab)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("Crontab 的設定參數");

                entity.Property(e => e.Cycle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("週期*->每一日,每週一~每周日,每月,指定時間,週區間");

                entity.Property(e => e.DayOfMonth).HasComment("每月第幾日*->週期選擇欄位如果是每月時用時,會跟這個欄位有關");

                entity.Property(e => e.DaysOfMonth).HasComment("指定每月特定幾天才要觸發");

                entity.Property(e => e.DaysOfWeek).HasComment("一週內星期幾要觸發此展開");

                entity.Property(e => e.EndTime).HasComment("結束時間");

                entity.Property(e => e.FillUnKeyin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("是否補未到位數值*->Y, N");

                entity.Property(e => e.LastDay).HasComment("該時段生效的最後時間點");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasComment("名稱");

                entity.Property(e => e.PatrolPathId).HasComment("巡檢路線代碼");

                entity.Property(e => e.RangeBeginDay).HasComment("指定時間開始日");

                entity.Property(e => e.RangeBeginMonth).HasComment("指定時間開始月");

                entity.Property(e => e.RangeEndDay).HasComment("指定時間結束日");

                entity.Property(e => e.RangeEndMonth).HasComment("指定時間結束月");

                entity.Property(e => e.Rule1)
                    .HasMaxLength(10)
                    .HasComment("到位紀錄時段依據*->Y/N");

                entity.Property(e => e.Rule2)
                    .HasMaxLength(10)
                    .HasComment("報表紀錄時間依據*->0.當日, 1隔日");

                entity.Property(e => e.Shift).HasComment("指定多班別清單");

                entity.Property(e => e.SortId).HasComment("排序");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");

                entity.Property(e => e.TimeLimit)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasComment("是否限時*->Y,N");

                entity.Property(e => e.TipNotify).HasComment("提示通報*->0=不通報");

                entity.Property(e => e.UnTransNotify).HasComment("未回傳通報*->0=不通報");

                entity.Property(e => e.WeekRangeBegin).HasComment("週區間開始");

                entity.Property(e => e.WeekRangeEnd).HasComment("週區間結束");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.PatrolPathPeriod)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathPeriod_REF_PatrolPath");
            });

            modelBuilder.Entity<PatrolPathPeriodNexamItem>(entity =>
            {
                entity.ToTable("PatrolPathPeriodNExamItem");

                entity.HasIndex(e => new { e.PatrolPathPeriodId, e.EquipmentExamItemId })
                    .HasName("AK1_PatrolPathPeriodNExamItem")
                    .IsUnique();

                entity.HasOne(d => d.EquipmentExamItem)
                    .WithMany(p => p.PatrolPathPeriodNexamItem)
                    .HasForeignKey(d => d.EquipmentExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathPeriodNExamItem_REF_EquipmentExamItem");

                entity.HasOne(d => d.PatrolPathPeriod)
                    .WithMany(p => p.PatrolPathPeriodNexamItem)
                    .HasForeignKey(d => d.PatrolPathPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathPeriodNExamItem_REF_PatrolPathPeriod");
            });

            modelBuilder.Entity<PatrolPathPeriodNplace>(entity =>
            {
                entity.ToTable("PatrolPathPeriodNPlace");

                entity.HasIndex(e => new { e.PatrolPathPeriodId, e.PatroPlaceId })
                    .HasName("AK1_PatrolPathPeriodNPlace")
                    .IsUnique();

                entity.Property(e => e.PatroPlaceId).HasComment("巡檢點ID");

                entity.Property(e => e.PatrolPathPeriodId).HasComment("巡檢時段ID");

                entity.HasOne(d => d.PatroPlace)
                    .WithMany(p => p.PatrolPathPeriodNplace)
                    .HasForeignKey(d => d.PatroPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathPeriodNPlace_REF_PatrolPlace");

                entity.HasOne(d => d.PatrolPathPeriod)
                    .WithMany(p => p.PatrolPathPeriodNplace)
                    .HasForeignKey(d => d.PatrolPathPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathPeriodNPlace_REF_PatrolPathPeriod");
            });

            modelBuilder.Entity<PatrolPathScope>(entity =>
            {
                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.PatrolPathScope)
                    .HasForeignKey(d => d.PatrolPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathScope_REF_PatrolPath");

                entity.HasOne(d => d.PatrolScope)
                    .WithMany(p => p.PatrolPathScope)
                    .HasForeignKey(d => d.PatrolScopeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPathScope_REF_PatrolScope");
            });

            modelBuilder.Entity<PatrolPlace>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasComment("編碼");

                entity.Property(e => e.Latitude)
                    .HasColumnType("decimal(12, 9)")
                    .HasComment("緯度");

                entity.Property(e => e.Longitude)
                    .HasColumnType("decimal(11, 8)")
                    .HasComment("經度");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.PatrolScopeId).HasComment("巡檢範圍Id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasComment("種類(HF,UHF,一維,二維)");

                entity.HasOne(d => d.PatrolScope)
                    .WithMany(p => p.PatrolPlace)
                    .HasForeignKey(d => d.PatrolScopeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatrolPlace_REF_PatrolScope");
            });

            modelBuilder.Entity<PatrolScope>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Account)
                    .HasName("idx1_Person");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("帳號");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasComment("地址");

                entity.Property(e => e.AuthorityId).HasComment("權限");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasComment("出生年月日");

                entity.Property(e => e.ContractorName)
                    .HasMaxLength(20)
                    .HasComment("承攬商(用以識別正興)");

                entity.Property(e => e.Education)
                    .HasMaxLength(50)
                    .HasComment("學歷");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasComment("信箱");

                entity.Property(e => e.Experience)
                    .HasMaxLength(50)
                    .HasComment("經歷");

                entity.Property(e => e.IdentityCardNumber)
                    .HasMaxLength(20)
                    .HasComment("身分證字號");

                entity.Property(e => e.JobTitleId).HasComment("職稱");

                entity.Property(e => e.LastUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.License)
                    .HasMaxLength(100)
                    .HasComment("證照");

                entity.Property(e => e.Memo)
                    .HasMaxLength(500)
                    .HasComment("備註");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .HasComment("手機");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("姓名");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("密碼");

                entity.Property(e => e.PasswordUpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Qualifications)
                    .HasMaxLength(500)
                    .HasComment("資格");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("密碼鹽");

                entity.Property(e => e.SchoolDepartment)
                    .HasMaxLength(50)
                    .HasComment("科⽬");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("狀態");

                entity.Property(e => e.WorkingType)
                    .HasMaxLength(100)
                    .HasComment("工作性質");

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.AuthorityId)
                    .HasConstraintName("FK_Person_REF_Authority");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_Person_REF_JobTitle");
            });

            modelBuilder.Entity<PersonDepartment>(entity =>
            {
                entity.HasIndex(e => new { e.PersonId, e.DepartmentId })
                    .HasName("UX1_PersonDepartment")
                    .IsUnique();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.PersonDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_PersonDepartment_REF_Department");
            });

            modelBuilder.Entity<PersonManager>(entity =>
            {
                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.PersonManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_PersonManager_REF_Person");
            });

            modelBuilder.Entity<PersonnelChange>(entity =>
            {
                entity.Property(e => e.ChangedCount).HasComment("異動後人數");

                entity.Property(e => e.IsChanged)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否異動");

                entity.Property(e => e.Memo)
                    .HasMaxLength(500)
                    .HasComment("備註");

                entity.Property(e => e.MustQualify)
                    .HasMaxLength(500)
                    .HasComment("必須資格");

                entity.Property(e => e.OptionalQualify)
                    .HasMaxLength(500)
                    .HasComment("選項資格");

                entity.Property(e => e.OriginalCount).HasComment("原有人數");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("狀態");

                entity.Property(e => e.VacancyCount).HasComment("需求人數");
            });

            modelBuilder.Entity<Pfdetail>(entity =>
            {
                entity.ToTable("PFDetail");

                entity.HasIndex(e => new { e.ParentId, e.ItemId })
                    .HasName("AK1_PFDetail")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                   ;

                entity.Property(e => e.QtyChange).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Pfdetail)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PFDetail_REF_PFMaster");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.Pfdetail)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PFDetail_REF_PartInfo");
            });

            modelBuilder.Entity<Pfmaster>(entity =>
            {
                entity.HasKey(e => e.FormId)
                    .HasName("PK_PFMASTER");

                entity.ToTable("PFMaster");

                entity.HasComment("Parts Form");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FormNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.FormType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.QtyChangeTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasComment(@"立單->送審給倉管->倉管確認->倉管送審到業主->業主結案
   1.立單送審給倉管,倉管可以退單
   2.若倉管確認怎扣庫存
   3.倉管確認後就只能往上送審
   4.若單子有誤可以打反向沖銷單,例如領料單和領料退回單,入料單或入料退出單
   ");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Pfmaster)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PFMaster_REF_PartLocationInfo");
            });

            modelBuilder.Entity<PlaceStayTime>(entity =>
            {
                entity.HasIndex(e => new { e.PatrolPathPeriodId, e.PatroPlaceId })
                    .HasName("AK1_PlaceStayTime")
                    .IsUnique();

                entity.HasOne(d => d.PatroPlace)
                    .WithMany(p => p.PlaceStayTime)
                    .HasForeignKey(d => d.PatroPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceStayTime_REF_PatrolPlace");

                entity.HasOne(d => d.PatrolPathPeriod)
                    .WithMany(p => p.PlaceStayTime)
                    .HasForeignKey(d => d.PatrolPathPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceStayTime_REF_PatrolPathPeriod");
            });

            modelBuilder.Entity<Remaker>(entity =>
            {
                entity.Property(e => e.RemakerName).IsRequired();

                entity.Property(e => e.Type).IsRequired();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名稱");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("是否停用");
            });

            modelBuilder.Entity<ShiftSchedulingRules>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(255);

                entity.Property(e => e.RuleType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.ContractorShiftId1Navigation)
                    .WithMany(p => p.ShiftSchedulingRulesContractorShiftId1Navigation)
                    .HasForeignKey(d => d.ContractorShiftId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShiftSchedulingRules_REF_ContractorShift1");

                entity.HasOne(d => d.ContractorShiftId2Navigation)
                    .WithMany(p => p.ShiftSchedulingRulesContractorShiftId2Navigation)
                    .HasForeignKey(d => d.ContractorShiftId2)
                    .HasConstraintName("FK_ShiftSchedulingRules_REF_ContractorShift2");
            });

            modelBuilder.Entity<ShiftSchedulingRulesPeople>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.ShiftSchedulingRulesPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShiftSchedulingRulesPeople_REF_Person");

                entity.HasOne(d => d.ShiftSchedulingRules)
                    .WithMany(p => p.ShiftSchedulingRulesPeople)
                    .HasForeignKey(d => d.ShiftSchedulingRulesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShiftSchedulingRulesPeople_REF_ShiftSchedulingRules");
            });

            modelBuilder.Entity<Smtp>(entity =>
            {
                entity.ToTable("SMTP");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EnableSsl)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StrengthenInspection>(entity =>
            {
                entity.Property(e => e.CreaetTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<SystemEnvironment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrentSqliteFileIndex).HasColumnName("CurrentSQLiteFileIndex");

                entity.Property(e => e.File1CreateTime).HasColumnType("datetime");

                entity.Property(e => e.File1Md5)
                    .IsRequired()
                    .HasColumnName("File1MD5");

                entity.Property(e => e.File2CreateTime).HasColumnType("datetime");

                entity.Property(e => e.File2Md5)
                    .IsRequired()
                    .HasColumnName("File2MD5");

                entity.Property(e => e.File3CreateTime).HasColumnType("datetime");

                entity.Property(e => e.File3Md5)
                    .IsRequired()
                    .HasColumnName("File3MD5");

                entity.Property(e => e.SqliteFilename1)
                    .IsRequired()
                    .HasColumnName("SQLiteFilename1");

                entity.Property(e => e.SqliteFilename2)
                    .IsRequired()
                    .HasColumnName("SQLiteFilename2");

                entity.Property(e => e.SqliteFilename3)
                    .IsRequired()
                    .HasColumnName("SQLiteFilename3");
            });

            modelBuilder.Entity<UpdateFile>(entity =>
            {
                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<VirtualEquipmentExamItem>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.EquipmentExam)
                    .WithMany(p => p.VirtualEquipmentExamItem)
                    .HasForeignKey(d => d.EquipmentExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualEquipmentExamItem_REF_EquipmentExam");

                entity.HasOne(d => d.EquipmentExamItem)
                    .WithMany(p => p.VirtualEquipmentExamItem)
                    .HasForeignKey(d => d.EquipmentExamItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualEquipmentExamItem_REF_EquipmentExamItem");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.VirtualEquipmentExamItem)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualEquipmentExamItem_REF_Equipment");
            });

            modelBuilder.Entity<WalkthroughShiftSchedule>(entity =>
            {
                entity.Property(e => e.AttendanceDate).HasColumnType("datetime");

                entity.Property(e => e.DayOfWeek)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RuleType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShiftName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.WeekOfYear)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<WorkLog>(entity =>
            {
                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.WorkLogDate).HasColumnType("datetime");

                entity.Property(e => e.WorkingArea).HasMaxLength(200);

                entity.Property(e => e.WorkingContent).HasMaxLength(500);

                entity.HasOne(d => d.ContractorShift)
                    .WithMany(p => p.WorkLog)
                    .HasForeignKey(d => d.ContractorShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkLog_REF_ContractorShift");

                entity.HasOne(d => d.WorkType)
                    .WithMany(p => p.WorkLog)
                    .HasForeignKey(d => d.WorkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkLog_REF_WorkType");
            });

            modelBuilder.Entity<WorkType>(entity =>
            {
                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Remark).HasMaxLength(255);

                entity.Property(e => e.WorkingDay).HasMaxLength(50);

                entity.Property(e => e.WorkingHours).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<WorkTypePeople>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.WorkTypePeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkTypePeople_REF_Person");

                entity.HasOne(d => d.WorkType)
                    .WithMany(p => p.WorkTypePeople)
                    .HasForeignKey(d => d.WorkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkTypePeople_REF_WorkType");
            });

            modelBuilder.Entity<WorkingPlanCollection>(entity =>
            {
                entity.Property(e => e.Remark).HasMaxLength(255);

                entity.Property(e => e.WorkDate).HasColumnType("datetime");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.WorkingPlanCollection)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingPlanCollection_REF_Equipment");

                entity.HasOne(d => d.Expand)
                    .WithMany(p => p.WorkingPlanCollection)
                    .HasForeignKey(d => d.ExpandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkingPlanCollection_REF_Expand");
            });

            modelBuilder.Entity<WorkSchedule>(entity =>
            {

                entity.Property(e => e.WorkDate).HasColumnType("datetime");

                entity.HasOne(d => d.PatrolGroup)
                    .WithMany(p => p.WorkSchedule)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkSchedule_REF_Group");

                entity.HasOne(d => d.PatrolPath)
                    .WithMany(p => p.WorkSchedule)
                    .HasForeignKey(d => d.PathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkSchedule_REF_Path");

                entity.HasOne(d => d.PatrolPlace)
                    .WithMany(p => p.WorkSchedule)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkSchedule_REF_Place");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.WorkSchedule)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkSchedule_REF_Equipment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}