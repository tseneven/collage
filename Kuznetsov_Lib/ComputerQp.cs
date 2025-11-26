using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuznetsov_Lib
{
    public class ComputerQp : ComputerQ
    {
        string P { get; set; }

        public ComputerQp(string cpuName, string cpuValue, string ozu, string P)
            : base(cpuName, cpuValue, ozu)
        {
            this.P = P;
        }

        public override string Q()
        {
            int pInt;

            if (!int.TryParse(P, out pInt))
            {
                return "Данные должны быть числовыми";
            }

            if (pInt < 0)
            {
                return "Число не может быть меньше 0";
            }

            string baseResult = base.Q();

            if (!double.TryParse(baseResult, out double baseValue))
            {
                return baseResult;
            }

            if (pInt > 1500)
            {
                return (baseValue - 0.5 * pInt).ToString();
            }
            else
            {
                return (baseValue + 0.8 * pInt).ToString(); 
            }
        }
    }

}
