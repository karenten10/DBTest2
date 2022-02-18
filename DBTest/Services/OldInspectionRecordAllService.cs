using Dapper;
using Database.Models.Models;
using InspectionBlazor.AdapterModels;
using InspectionBlazor.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.Services
{
    public class OldInspectionRecordAllService
    {
        string ConnectionString = "";

        public ILogger<OldInspectionRecordAllService> Logger { get; }
        public IConfiguration Configuration { get; }

        public OldInspectionRecordAllService(ILogger<OldInspectionRecordAllService> logger,IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;

            string oldInspectionDBContext = Configuration["OldInspectionDBContext"];
            ConnectionString = oldInspectionDBContext;
        }

        public async Task<DateTime> GetLastDate()
        {
            DateTime result = DateTime.Today;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               select top 1 a.a09
                                FROM [a00052] a 
                                order by a.a09 desc
                          ";

                result = conn.Query<DateTime>(strSql).FirstOrDefault();
            }

            await Task.Yield();
            return result;
        }
        public async Task<List<OldInspectionSearchFieldModel>> GetDepartmentAsync()
        {
            List<OldInspectionSearchFieldModel> result = new List<OldInspectionSearchFieldModel>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               SELECT a01 as Id,
                                a02 as Name
                                FROM a00007
                          ";

                result = conn.Query<OldInspectionSearchFieldModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        public async Task<List<OldInspectionSearchFieldModel>> GetPatrolPathListAsync(int departmentId)
        {
            List<OldInspectionSearchFieldModel> result = new List<OldInspectionSearchFieldModel>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               select a01 as Id,
                                a02 as Name
                                from a00016
                                where a07 = {departmentId}
                          ";

                result = conn.Query<OldInspectionSearchFieldModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        public async Task<List<OldInspectionSearchFieldModel>> GetScopeListAsync(int patrolPathId)
        {
            List<OldInspectionSearchFieldModel> result = new List<OldInspectionSearchFieldModel>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               select distinct(範圍.a01)  id,
                                範圍.a02 Name
                                from a00023 a
                                inner join a00013 as 範圍
                                on a.a18 = 範圍.a01
                                where a.a21 = {patrolPathId}
                          ";

                result = conn.Query<OldInspectionSearchFieldModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        public async Task<List<OldInspectionSearchFieldModel>> GetEquipmentListAsync(int scopeId)
        {
            List<OldInspectionSearchFieldModel> result = new List<OldInspectionSearchFieldModel>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string strSql = "";

                strSql = $@"
                               select distinct(設備.a01)  id,
                                設備.a02 Name
                                from a00023 a
                                inner join a00021 as 設備
                                on a.a16 = 設備.a01
                                where a.a18 = {scopeId}
                          ";

                result = conn.Query<OldInspectionSearchFieldModel>(strSql).ToList();
            }

            await Task.Yield();
            return result;
        }
        public async Task<IQueryable<OldInspectionRecordAdapterModel>> GetOutComeByConditionAsync(OldInspectionQueryConditionDataModel conditionDataModel, List<long?> loginDepts)
        {
            try
            {
                IQueryable<OldInspectionRecordAdapterModel> result;
                string monthRange = DateTime.Today.ToString("yyyyMM");

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {

                    conn.Open();
                    string strSql = "";

                    strSql = $@"
                               SELECT b.a02 AS Period,
                               路線.a02 AS Path,
                               部門單位.a02 AS DepartmentName,
                               範圍.a02 AS Scope,
                               巡檢點.a09 AS Place,
                               設備.a02 AS Exam,
                               檢驗項目.a03 AS ExamItem,
                               審核依據.a02 AS Audit,
                               a.a05 AS Value,
                               人員.a04 AS Recorder,
                               a.a09 AS UpdateTime,
	                           a.a16 AS Remark,
                               a.a10 as StatusCode
                        FROM [a00052] a
                            INNER JOIN a00027 b
                                ON b.a01 = a.a03
                            INNER JOIN a00016 AS 路線
                                ON 路線.a01 = a.a13
                            INNER JOIN a00007 AS 部門單位
                                ON 部門單位.a01 = 路線.a07
                            INNER JOIN a00013 AS 範圍
                                ON 範圍.a01 = a.a21
                            INNER JOIN a00014 AS 巡檢點
                                ON 巡檢點.a01 = a.a22
                            INNER JOIN a00021 AS 設備
                                ON 設備.a01 = a.a20
                            INNER JOIN a00023 AS 檢驗項目
                                ON 檢驗項目.a01 = a.a02
                            INNER JOIN a00025 AS 審核依據
                                ON 審核依據.a01 = 檢驗項目.a05
                            INNER JOIN dbo.a00001 AS 人員
                                ON 人員.a01 = a.a04
                            WHERE a.a09 >= '{conditionDataModel.Begin.ToString("yyyy/MM/dd")}' and 
                                  a.a09 < '{conditionDataModel.End.AddDays(1).ToString("yyyy/MM/dd")}'
                                ";

                    #region 部門
                    if (conditionDataModel.DepartmentId != 0)
                    {
                        strSql = strSql + $" and 部門單位.a01 = {conditionDataModel.DepartmentId}";
                    }
                    #endregion

                    #region 路線
                    if (conditionDataModel.PatrolPath != 0)
                    {
                        strSql = strSql + $" and a.a13 = {conditionDataModel.PatrolPath}";
                    }
                    #endregion

                    #region 範圍
                    if (conditionDataModel.Scope != 0)
                    {
                        strSql = strSql + $" and a.a21 = {conditionDataModel.Scope}";
                    }
                    #endregion

                    #region 設備
                    if (conditionDataModel.Equipment != 0)
                    {
                        strSql = strSql + $" and a.a20 = {conditionDataModel.Equipment}";
                    }
                    #endregion

                    #region 異常
                    if(conditionDataModel.SearchType == Helpers.InspectionRecordHelper.SearchType.Abnormal)
                    {
                        strSql = strSql + $" and a.a10 = 1";
                    }
                    #endregion

                    strSql = strSql + " order by a.a09";

                    result = conn.Query<OldInspectionRecordAdapterModel>(strSql).ToList().AsQueryable();
                }

                await Task.Yield();
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetOutComeByConditionAsync error = {ex.Message}");
                return null;
            }
        }
    }
}
