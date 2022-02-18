using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Dapper;
using InspectionBlazor.Helpers;

namespace InspectionBlazor.Services
{
    
    public class EquipmentTrendChartService
    {
        private readonly InspectionDBContext context;
        private readonly ILogger<EquipmentTrendChartService> logger;

        public EquipmentTrendChartService(InspectionDBContext context, ILogger<EquipmentTrendChartService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<EquipmentTrendChartDataModel>> GetOutComeValuesAsync(EquipmentTrendChartQueryConditionDataModel conditionDataModel)
        {
            var dbResult = await context.OutCome
               .AsNoTracking()
               .Include(x => x.ExpandExamItem)
               .ThenInclude(x => x.EquipmentExamItem)
               .ThenInclude(x => x.Equipment)
               .Where(x =>
               x.Expand.EndTime >= conditionDataModel.Begin &&
               x.Expand.EndTime <= conditionDataModel.End &&
               x.ExpandExamItem.EquipmentExamItemId == conditionDataModel.EquipmentExamItemId &&
               x.IsCompleted == MagicHelper.StatusYesCode)
               .OrderBy(x=>x.Expand.EndTime)
               .Select(x=> new EquipmentTrendChartDataModel
               {
                    EquipmenUnit = x.ExpandExamItem.EquipmentExamItem.Unit,
                    EquipmentName = $"{x.Expand.BeginTime.ToString("yyyy/MM/dd")} - {x.Expand.EndTime.ToString("yyyy/MM/dd")}",
                    EquipmentValue = x.Value
               })
               .ToListAsync();

            return dbResult;

        }
    }
}
