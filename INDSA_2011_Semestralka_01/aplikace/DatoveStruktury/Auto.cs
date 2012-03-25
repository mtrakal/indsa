using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public class Auto {
        public Auto() {
            HranaPoloha = new CestyGraf.Hrana();
        }
        public CestyGraf.Hrana HranaPoloha { get; set; }
        public double VzdalenostOdV1 { get; set; }
        public double VzdalenostOdV2 { get; set; }
        public CestyGraf.Vrchol DejPolohu() {
            CestyGraf.Vrchol v = new CestyGraf.Vrchol();
            v.Souradnice = new CestyGraf.Bod();
            double vzdalenostX = Math.Abs(HranaPoloha.Vrchol1.Souradnice.X - HranaPoloha.Vrchol2.Souradnice.X);
            double vzdalenostY = Math.Abs(HranaPoloha.Vrchol1.Souradnice.Y - HranaPoloha.Vrchol2.Souradnice.Y);
            double vzdalenostPrepona = VzdalenostOdV1 + VzdalenostOdV2;
            double koeficient1 = VzdalenostOdV1 / vzdalenostPrepona;
            v.Souradnice.X = vzdalenostX * koeficient1;
            v.Souradnice.Y = vzdalenostY * koeficient1;
            if (HranaPoloha.Vrchol1.Souradnice.X > HranaPoloha.Vrchol2.Souradnice.X) {
                v.Souradnice.X *= -1;
            }
            if (HranaPoloha.Vrchol1.Souradnice.Y > HranaPoloha.Vrchol2.Souradnice.Y) {
                v.Souradnice.Y *= -1;
            }
            v.Souradnice.X += HranaPoloha.Vrchol1.Souradnice.X;
            v.Souradnice.Y += HranaPoloha.Vrchol1.Souradnice.Y;
            return v;
        }
    }
}