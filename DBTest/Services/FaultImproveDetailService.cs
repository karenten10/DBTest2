using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class FaultImproveDetailService
    {
        private readonly InspectionDBContext context;

        public FaultImproveDetailService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<FaultImproveDetail> GetByGuidAsync(string guid)
        {
            FaultImproveDetail item = await context.FaultImproveDetail.AsNoTracking()
                .Include(x => x.Equipment)
                .ThenInclude(x => x.PatrolPlace)
                .ThenInclude(x => x.PatrolScope)
                .FirstOrDefaultAsync(x => x.FaultImproveGuid == guid);
            return item;
        }
    }
}
