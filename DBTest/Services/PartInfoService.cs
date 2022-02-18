using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PartInfoService
    {
        private readonly InspectionDBContext context;

        public PartInfoService(InspectionDBContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<PartInfo>> GetAsync()
        {
            return Task.FromResult(context.PartInfo
                .AsNoTracking().AsQueryable());
        }

        public async Task<List<MyPFMasterMonthly>> GetAllAsync()
        {
            var result = await
                (from a in context.PartInfo
                select new MyPFMasterMonthly
                {
                    PartId = a.PartId,
                    PartName = a.Name
                })
                .OrderBy(x => x.PartName)
                .AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<PartInfo> GetAsync(int id)
        {
            PartInfo item = await context.PartInfo.AsNoTracking()
                .FirstOrDefaultAsync(x => x.PartId == id);
            return item;
        }

        public async Task AddAsync(PartInfo paraObject)
        {
            paraObject.CreateTime = DateTime.Now;
            paraObject.PartIdNumber = paraObject.Name;
            paraObject.StockQty = 0;

            await context.PartInfo.AddAsync(paraObject);
            await context.SaveChangesAsync();
            return;
        }

        public async Task<PartInfo> UpdateAsync(PartInfo paraObject)
        {
            PartInfo item = await context.PartInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PartId == paraObject.PartId);
            if (item == null)
            {
                return null;
            }
            else
            {
                #region 在這裡需要設定需要解除快取紀錄
                context.CleanAllEFCoreTracking<PartInfo>();
                #endregion
                // set Modified flag in your entry
                context.Entry(paraObject).State = EntityState.Modified;

                // save 
                await context.SaveChangesAsync();
                return paraObject;
            }
        }

        public async Task<PartInfo> DeleteAsync(PartInfo paraObject)
        {
            await Task.Delay(100);
            PartInfo item = await context.PartInfo.FirstOrDefaultAsync(x => x.PartId == paraObject.PartId);
            if (item == null)
            {
                return null;
            }
            else
            {
                context.PartInfo.Remove(item);
                await context.SaveChangesAsync();
                return item;
            }
        }
        public async Task<string> InitStockQty(PartInfoAdapterModel paraObject)
        {
            string result = "";
            context.Database.BeginTransaction();
            try
            {
                PartInfo item = await context.PartInfo
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.PartId == paraObject.PartId);
                if (item == null)
                {
                    return null;
                }
                else
                {
                    #region 在這裡需要設定需要解除快取紀錄
                    context.CleanAllEFCoreTracking<PartInfo>();
                    context.CleanAllEFCoreTracking<PartQtyInit>();
                    #endregion

                    //新增PartQtyInit
                    PartQtyInit QtyItme = await context.PartQtyInit.AsNoTracking().FirstOrDefaultAsync(x => x.PartId == paraObject.PartId);

                    if (QtyItme == null)
                    {
                        PartQtyInit partQtyInit = new PartQtyInit();
                        partQtyInit.CreateTime = DateTime.Now;
                        partQtyInit.ModifiedTime = DateTime.Now;
                        partQtyInit.PartId = paraObject.PartId;
                        partQtyInit.QtyInit = paraObject.StockQty;
                        await context.PartQtyInit.AddAsync(partQtyInit);
                    }
                    else
                    {
                        QtyItme.ModifiedTime = DateTime.Now;
                        QtyItme.QtyInit = paraObject.StockQty;
                        context.Entry(QtyItme).State = EntityState.Modified;
                    }
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PartQtyInit ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PartQtyInit OFF;");

                    context.CleanAllEFCoreTracking<PartQtyInit>();

                    //修改PartInfo
                    item.StockQty = paraObject.StockQty;
                    context.Entry(item).State = EntityState.Modified;
                    // save 
                    await context.SaveChangesAsync();
                    context.Database.CommitTransaction();
                    result = "庫存設定成功!";

                }
            }
            catch (Exception ex)
            {
                var aa = ex;
                context.Database.RollbackTransaction();
                result = "庫存設定發生錯誤!";
            }
            return result;
        }

        public Task<IQueryable<PartInfo>> GetByHeaderIDAsync(int equipmentId)
        {
            var PartInfoList = (from a in context.EquipmentPart
                                join b in context.PartInfo on a.PartId equals b.PartId
                                where a.EquipmentId == equipmentId
                                select b).AsQueryable();

            return Task.FromResult(PartInfoList);
        }

        public Task<List<PartInfo>> GetByEquipmentIdAsync(int? equipmentId)
        {
            if (equipmentId == null)
            {
                return Task.FromResult(new List<PartInfo>());
            }
            else if (equipmentId == 0)
            {
                return context.PartInfo
                .AsNoTracking().ToListAsync();
            }
            else
            {
                var PartInfoList = (from a in context.EquipmentPart
                                    join b in context.PartInfo on a.PartId equals b.PartId
                                    where a.EquipmentId == equipmentId
                                    select b).ToListAsync();

                return PartInfoList;
            }
        }

        public async Task<List<PartInfo>> GetPartInfoAsync()
        {
            var result = await context.PartInfo
                        .AsNoTracking()
                        .ToListAsync();

            return result;
        }

        public async Task<int> GetEquipmentPartQTYAsync(int equipmentId, int partId)
        {
            int QTY = 0;

            var result = await (from a in context.EquipmentPart
                                join b in context.PartInfo on a.PartId equals b.PartId
                                where a.EquipmentId == equipmentId && a.PartId == partId
                                select new { QTY = a.Qty }).FirstOrDefaultAsync();

            if (result != null)
            {
                QTY = result.QTY;
            }

            return QTY;
        }
    }
}
