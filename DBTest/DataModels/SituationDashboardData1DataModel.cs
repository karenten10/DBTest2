using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class SituationDashboardData1DataModel
    {
        public string 承攬商 { get; set; }
        public string 承攬商文字
        {
            get
            {
                Byte[] encodedBytes = Encoding.ASCII.GetBytes(承攬商);
                int emptyCnt = 0;
                foreach (var item in encodedBytes)
                {
                    if (item == 63)
                    {
                        emptyCnt++;
                    }
                }
                return $"{承攬商.PadRight(承攬商.Length + emptyCnt * 4, ' ')}";
            }
        }
        public int 總巡檢數 { get; set; }
        public int 異常通報數 { get; set; }
        public int 未到位數 { get; set; }
        public double 異常通報比例 { get; set; }
        public string 異常通報比例文字
        {
            get
            {
                return $"{異常通報比例} %";
            }
        }
    }
}
