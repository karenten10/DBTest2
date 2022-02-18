using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels
{
    public class SituationDashboardData7DataModel
    {
        public string 單位 { get; set; }

        public string 單位文字
        {
            get
            {
                Byte[] encodedBytes = Encoding.ASCII.GetBytes(單位);
                int emptyCnt = 0;
                foreach (var item in encodedBytes)
                {
                    if (item == 63)
                    {
                        emptyCnt++;
                    }
                }
                return $"{單位.PadRight(單位.Length + emptyCnt * 3, ' ')}";
            }
        }
        public int 維修通報數 { get; set; }
        public int 等待改善數 { get; set; }
    }
}
