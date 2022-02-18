using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspectionBlazor.AdapterModels;

namespace InspectionBlazor.Services
{
    public class RepairMasterService
    {
        private readonly InspectionDBContext context;
        private readonly PersonService personService;

        public RepairMasterService(InspectionDBContext context, PersonService personService)
        {
            this.context = context;
            this.personService = personService;
        }

        public Task<IQueryable<RepairMaster>> GetAsync()
        {
            //RepairMasterAdapterModel repairMasterAdapterModel = new RepairMasterAdapterModel();

            //repairMasterAdapterModel = from a in context.RepairMaster
            //                           join b in context.Person on a.PersonId equals b.Id into CreatePerson
            //                           from c in CreatePerson.DefaultIfEmpty()
            //                           join d in context.Person on a.PersonInCharge equals d.Id into PersonInCharge
            //                           from e in PersonInCharge.DefaultIfEmpty()
            //                           join f in context.Person on a.PersonOfContact equals f.Id into PersonOfContact
            //                           from g in PersonOfContact.DefaultIfEmpty()
            //                           join h in context.Department on a.DepartmentId equals h.Id into Department
            //                           from i in Department.DefaultIfEmpty()
            //                           join j in context.Person on a.ApplicantId equals j.Id into Applicant
            //                           from k in Applicant.DefaultIfEmpty()


            return Task.FromResult(context.RepairMaster
                .AsNoTracking().AsQueryable());
        }

        public async Task<RepairMaster> GetAsync(int id)
        {
            RepairMaster item = await context.RepairMaster.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task AddAsync(RepairMaster paraObject)
        {
            await context.Database.BeginTransactionAsync();
            try
            {
                await context.RepairMaster.AddAsync(paraObject);
                await context.SaveChangesAsync();

                //Step1: 建立Master，Level=0
                Guid g = Guid.NewGuid();

                RepairManagerApproval RepairManager = new RepairManagerApproval();
                RepairManager.ParentId = null;
                RepairManager.SourceId = null;
                RepairManager.RepairMasterId = paraObject.Id;
                RepairManager.Status = "Init";
                RepairManager.PersonId = paraObject.PersonId;
                RepairManager.Level = 0;
                RepairManager.Guid = g.ToString();
                RepairManager.CreatedDate = DateTime.Now;

                await context.RepairManagerApproval.AddAsync(RepairManager);
                await context.SaveChangesAsync();
                context.CleanAllEFCoreTracking<RepairManagerApproval>();

                //Step2: 依照設備負責人派出信
                var result1 = await context.RepairManagerApproval
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Guid == g.ToString()
                                                    && x.Level == 0);

                if (result1 != null)
                {
                    var list設備負責人 = await context.RepairEquipmentNPerson.Where(x => x.RepairEquipmentId == paraObject.RepairEquipmentId).ToListAsync();


                    if (list設備負責人.Any())
                    {
                        for (int i = 0; i < list設備負責人.Count; i++)
                        {
                            RepairManagerApprovalInbox repairManagerApprovalInbox = new RepairManagerApprovalInbox();
                            repairManagerApprovalInbox.RepairManagerApprovalId = result1.Id;
                            repairManagerApprovalInbox.PersonId = list設備負責人[i].PersonId;
                            repairManagerApprovalInbox.IsApproval = "N";
                            repairManagerApprovalInbox.CreatedDate = DateTime.Now;

                            await context.RepairManagerApprovalInbox.AddAsync(repairManagerApprovalInbox);
                            await context.SaveChangesAsync();
                            context.CleanAllEFCoreTracking<RepairManagerApprovalInbox>();
                        }
                    }


                }
                await context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
            }
           
            return;
        }

        public async Task<RepairMaster> UpdateAsync(RepairMaster paraObject)
        {
            RepairMaster item = await context.RepairMaster
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<RepairMaster>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<RepairMaster> DeleteAsync(RepairMaster paraObject)
        {
            await Task.Delay(100);
            RepairMaster item = await context.RepairMaster.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.RepairMaster.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }

        public async Task<List<RepairEquipmentGroup>> GetRepairEquipmentGroupALLAsync()
        {
            List<RepairEquipmentGroup> listRepairEquipmentGroup = new List<RepairEquipmentGroup>();

            listRepairEquipmentGroup = await context.RepairEquipmentGroup.ToListAsync();

            return listRepairEquipmentGroup;

        }

        public async Task<List<RepairEquipmentModel>> GetRepairEquipmentList(int repairEquipmentGroupId)
        {
            context.CleanAllEFCoreTracking<RepairEquipment>();
            List<RepairEquipmentModel> listRepairEquipmentModel = new List<RepairEquipmentModel>();
            List<RepairEquipment> listRepairEquipment = new List<RepairEquipment>();
            //1.取得 RepairEquipment 
            if (repairEquipmentGroupId == 0)
            {
                // 取全部設備
                listRepairEquipment = await context.RepairEquipment.Where(x => x.Status == "N").ToListAsync();
            }
            else
            {
                listRepairEquipment = await context.RepairEquipment.Where(x => x.RepairEquipmentGroupId == repairEquipmentGroupId && x.Status == "N").ToListAsync();
            }

            // 2.取得設備對應人員 && 設備對應備註
            if (listRepairEquipment.Any())
            {
                context.CleanAllEFCoreTracking<RepairEquipmentNPerson>();

                foreach (var item in listRepairEquipment)
                {
                    RepairEquipmentModel repairEquipmentModel = new RepairEquipmentModel();


                    repairEquipmentModel.RepairEqipmentGroupId = item.RepairEquipmentGroupId;
                    repairEquipmentModel.RepairEquipmentName = item.Name;
                    repairEquipmentModel.RepairEquipmentId = item.Id;

                    var repairPersons = await (from a in context.RepairEquipmentNPerson
                                               join b in context.Person on a.PersonId equals b.Id
                                               where a.RepairEquipmentId == item.Id
                                               select b.Name).ToListAsync();

                    if (repairPersons.Any())
                    {
                        repairEquipmentModel.負責人姓名 = String.Join(", ", repairPersons.ToArray());
                    }else
                    {
                        repairEquipmentModel.負責人姓名 = "無負責人";
                    }



                    var repairRemakers = await (from a in context.RepairEquipmentNRepairRemark
                                                join b in context.RepairRemaker on a.RepairRemakerId equals b.Id
                                                where a.RepairEquipmentId == item.Id
                                                select b.RepairRemakerName).ToListAsync();

                    if (repairRemakers.Any())
                    {
                        repairEquipmentModel.維修備註 = String.Join(", ", repairRemakers.ToArray());
                    }else
                    {
                        repairEquipmentModel.維修備註 = "無故障原因備註";
                    }

                    listRepairEquipmentModel.Add(repairEquipmentModel);

                }
            }




            return listRepairEquipmentModel;


        }



        public async Task<int> Get設備負責人Id(int repairEquipmentId)
        {
            int 設備負責人ID = 0;


            var 設備負責人 = await context.RepairEquipmentNPerson.Where(x => x.RepairEquipmentId == repairEquipmentId).FirstOrDefaultAsync();

            
            if (設備負責人!=null)
            {
                設備負責人ID = 設備負責人.PersonId;
            }

            return 設備負責人ID;
        }

    }
}
