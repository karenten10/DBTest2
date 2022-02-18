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
    public class StockChangeHistoryService
    {
        private readonly InspectionDBContext context;

        public StockChangeHistoryService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<StockChangeHistoryAdapterModel>> GetAsync()
        {
            List<StockChangeHistoryAdapterModel> result = new List<StockChangeHistoryAdapterModel>();
            var parts = await context.PartInfo
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in parts)
            {
                result.Add(new StockChangeHistoryAdapterModel
                {
                    Id = item.PartId,
                    PartName = item.Name
                });
            }

            return result.AsQueryable();
        }

        public List<StockChangeHistoryDetail> GetHistoryByPartId(int id)
        {
            return (from a in context.Pfdetail
                 join b in context.Pfmaster on a.ParentId equals b.FormId
                 join c in context.Person on b.CreatePersonnelId equals c.Id
                 where a.PartId == id && b.Status == "確認"
                 select new StockChangeHistoryDetail
                 {
                     CreatePerson = c.Name,
                     FormNumber = b.FormNumber,
                     FormType = b.FormType,
                     Qty = a.QtyChange,
                     QtyChangeTime = b.QtyChangeTime,
                     Ramark = a.Remark
                 })
                 .OrderByDescending(x => x.QtyChangeTime)
                 .ToList();
        }
    }
}
