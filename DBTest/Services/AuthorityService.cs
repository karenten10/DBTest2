using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.Extensions;
using InspectionShare.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static InspectionBlazor.RazorModels.AuthorityRazorModel;

namespace InspectionBlazor.Services {
    public class AuthorityService {
        private readonly InspectionDBContext context;

        public AuthorityService(InspectionDBContext context) {
            this.context = context;
        }

        public async Task<List<AuthorityAdapterModel>> GetForPersonAsync() {
            var result = await context.Authority
                .AsNoTracking()
                .ToListAsync();

            List<AuthorityAdapterModel> authorityAdapterModels = new List<AuthorityAdapterModel>();
            foreach (var item in result) {
                authorityAdapterModels.Add(new AuthorityAdapterModel {
                    Id = item.Id,
                    Name = item.Name,
                    Guid = item.Guid
                });
            }

            return authorityAdapterModels;
        }

        public Task<IQueryable<Authority>> GetAsync() {
            return Task.FromResult(context.Authority
                .Include(x => x.AuthorityDetail)
                .OrderBy(x => x.Id)
                .AsNoTracking().AsQueryable());
        }

        public async Task<List<AuthorityDetail>> GetDetailAsync(long id) {
            List<AuthorityDetail> item = await context.AuthorityDetail
                                        .AsNoTracking()
                                        .Where(x => x.AuthorityId == id)
                                        .ToListAsync();
            return item;
        }

        public async Task AddAsync(Authority paraObject, List<MyAuthList> myAuthList) {
            string myGuid = Guid.NewGuid().ToString();
            paraObject.Guid = myGuid;
            await context.Authority.AddAsync(paraObject);
            await context.SaveChangesAsync();

            var result = await context.Authority.FirstOrDefaultAsync(x => x.Guid == myGuid);
            if (result != null) {
                foreach (var item in myAuthList) {
                    if (MenuHelper.必要選單.Contains(item.Code) || item.isChecked) {
                        AuthorityDetail authorityDetail = new AuthorityDetail {
                            AuthorityId = result.Id,
                            MenuCode = item.Code
                        };
                        await context.AuthorityDetail.AddAsync(authorityDetail);
                        await context.SaveChangesAsync();
                    }
                }
            }

            return;
        }

        public async Task<Authority> UpdateAsync(Authority paraObject, List<MyAuthList> myAuthList) {
            var result = await context.Authority
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paraObject.Id);

            if (result == null) {
                return null;
            } else {
                context.CleanAllEFCoreTracking<Authority>();
                context.Entry(paraObject).State = EntityState.Modified;
                await context.SaveChangesAsync();

                // delete detail
                var needDelete = context.AuthorityDetail.Where(x => x.AuthorityId == paraObject.Id);
                context.AuthorityDetail.RemoveRange(needDelete);
                await context.SaveChangesAsync();

                // add detail
                foreach (var item in myAuthList) {
                    if (MenuHelper.必要選單.Contains(item.Code) || item.isChecked) {
                        await context.AuthorityDetail.AddAsync(new AuthorityDetail {
                            AuthorityId = paraObject.Id,
                            MenuCode = item.Code
                        });
                        await context.SaveChangesAsync();
                    }
                }

                return paraObject;
            }
        }

        public async Task<Authority> DeleteAsync(Authority paraObject) {
            await Task.Delay(100);
            Authority item = await context.Authority.FirstOrDefaultAsync(x => x.Id == paraObject.Id);
            if (item == null) {
                return null;
            } else {
                //delete detail
                List<AuthorityDetail> authorityDetails =
                    await context.AuthorityDetail.Where(x => x.AuthorityId == paraObject.Id).ToListAsync();
                context.RemoveRange(authorityDetails);
                await context.SaveChangesAsync();

                //delete master
                context.Authority.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
    }
}
