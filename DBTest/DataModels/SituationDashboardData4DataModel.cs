using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class SituationDashboardData4DataModel
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
        public double 到位率 { get; set; }
        public string 到位率文字
        {
            get
            {
                return $"{到位率} %";
            }
        }
    }
}
