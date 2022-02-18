using Dapper;
using Database.Models.Models;
using InspectionShare.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class SmtpService
    {
        InspectionDBContext context;

        public SmtpService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<Smtp> GetAsync()
        {
            Smtp result = await context.Smtp
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task AddAsync(Smtp smtp)
        {
            smtp.CreatedDate = DateTime.Now;
            await context.Smtp.AddAsync(smtp);
            await context.SaveChangesAsync();
        }
    }
}
