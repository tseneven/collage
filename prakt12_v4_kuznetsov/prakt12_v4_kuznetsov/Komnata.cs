using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt12_v4_kuznetsov
{
    public class Komnata
    {
        public double S_komnati(double length, double shirina)
        {
            double S = length * shirina;

            return S;
        }
        
        public double s3_komnati(double length, double shirina, double visota)
        {
            double S3 = length * shirina * visota;

            return S3;
        }
        public double kol_mat(double length, double shirina, double visota, double kol_okn, double vis_okn, double shi_okn, double raz_oboi)
        {
            double itog = 0;
            double obl_rab = ((2 * (length * visota)) + (2 * (shirina * visota))) - (kol_okn * (vis_okn * shi_okn));

            if(raz_oboi == 10)
            {
                itog = obl_rab / 10;
            }
            else if (raz_oboi == 15)
            {
                itog = obl_rab / 15;
            }

            return itog;
        }

    }
}
