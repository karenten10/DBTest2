using Database.Models.Models;
using InspectionBlazor.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class ExceptionMessageService
    {
        private readonly InspectionDBContext context;

        public ExceptionMessageService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<ExceptionMessage>> GetAsync()
        {
            return context.ExceptionMessage
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<bool> AddAsync(ExceptionMessage paraObject)
        {
            try
            {
                await context.ExceptionMessage.AddAsync(paraObject);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
