using Database.Models.Models;
using Microsoft.EntityFrameworkCore;
// using SQLiteModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionShare.Helpers
{
    public static class CleanTracking
    {
        //public static void Clean<T>(SQLiteDBContext context) where T : class
        //{
        //    foreach (var fooXItem in context.Set<T>().Local)
        //    {
        //        context.Entry(fooXItem).State = EntityState.Detached;
        //    }
        //}
        public static void Clean<T>(InspectionDBContext context) where T : class
        {
            foreach (var fooXItem in context.Set<T>().Local)
            {
                context.Entry(fooXItem).State = EntityState.Detached;
            }
        }
    }
}
