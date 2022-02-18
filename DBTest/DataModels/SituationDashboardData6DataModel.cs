using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class SituationDashboardData6DataModel
    {
        public string 範圍 { get; set; }

        public string 範圍文字
        {
            get
            {
                Byte[] encodedBytes = Encoding.ASCII.GetBytes(範圍);
                int emptyCnt = 0;
                foreach (var item in encodedBytes)
                {
                    if (item == 63)
                    {
                        emptyCnt++;
                    }
                }
                return $"{範圍.PadRight(範圍.Length + emptyCnt * 4, ' ')}";
            }
        }
        public double 異常數比例 { get; set; }

        public string 異常數比例文字
        {
            get
            {
                return $"{異常數比例} %";
            }
        }
    }
}
