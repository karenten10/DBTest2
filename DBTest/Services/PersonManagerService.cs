using AutoMapper;
using Dapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.Extensions;
using InspectionBlazor.Helpers;
using InspectionShare.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class PersonManagerService
    {
        private readonly InspectionDBContext context;

        public PersonManagerService(InspectionDBContext context)
        {
            this.context = context;
        }

        public async Task<int?[]> GetPersonManagerAsync(int personId)
        {
            try
            {
                var result = await context.PersonManager
                    .Where(x => x.PersonId == personId)
                    .ToListAsync();

                if (result.Count > 0)
                {
                    int?[] r = new int?[result.Count];
                    for (int i = 0; i < result.Count(); i++)
                    {
                        r[i] = result[i].ManagerId;
                    }
                    return r;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
