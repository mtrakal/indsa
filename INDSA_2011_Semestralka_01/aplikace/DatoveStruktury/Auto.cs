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
            v = HranaPoloha.Vrchol1 as CestyGraf.Vrchol;
            double uhel = ((Math.Asin(HranaPoloha.Vrchol2.Souradnice.X - HranaPoloha.Vrchol1.Souradnice.X / HranaPoloha.Vrchol2.Souradnice.Y - HranaPoloha.Vrchol1.Souradnice.Y)) / (2 * Math.PI)) * 360;
            //return new Vrchol("automobil",(HranaPoloha.Vrchol1.Souradnice.X + HranaPoloha.Vrchol2.Souradnice.X) / 2, (HranaPoloha.Vrchol1.Souradnice.Y + HranaPoloha.Vrchol2.Souradnice.Y) / 2,);
            return v;
        }
    }
}