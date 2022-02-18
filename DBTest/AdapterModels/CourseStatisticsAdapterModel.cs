using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.AdapterModels
{
    public class CourseStatisticsAdapterModel
    {
       public string Title { get; set; }
        public List<DepartmentPathMaster> listDepartmentPathMasters = new List<DepartmentPathMaster>();
        public List<OutcomeDetail> listOutcomeDetail = new List<OutcomeDetail>();
        public List<CheckInDetail> listCheckInDetail = new List<CheckInDetail>();
    }

    public class DepartmentPathMaster
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<DepartmentPathDetail> listDepartmentPathDetail = new List<DepartmentPathDetail>();

    }


    public class DepartmentPathDetail
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PathId { get; set; }
        public string PathName { get; set; }
        public int 應到位數量 { get; set; }
        public int 未到位數量 { get; set; }
        public int 總檢查數 { get; set; }
        public int 異常檢查數量 { get; set; }
        public double 簽核率 { get; set; }

    }
    public class CheckInDetail
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int 應到位數量 { get; set; }
        public int 未到位數量 { get; set; }
        public double 到位率 { get; set; }
        public double 簽核率 { get; set; }
    }
    public class OutcomeDetail
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int 總檢查數 { get; set; }
        public int 檢查異常數 { get; set; }
        public double 檢查正常率 { get; set; }
        public double 簽核率 { get; set; }
    }
}
