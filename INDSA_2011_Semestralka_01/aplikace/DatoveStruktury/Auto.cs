using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    public class Auto {
        public Auto() {
            HranaPoloha = new Hrana();
        }
        public Hrana HranaPoloha { get; set; }
        public double VzdalenostOdV1 { get; set; }
        public double VzdalenostOdV2 { get; set; }
        public Graf<string, string, double>.IVrchol DejPolohu() {
            Graf<string,string,double>.IVrchol v = new Vrchol();
            v = HranaPoloha.Vrchol1;
            double uhel = ((Math.Asin(HranaPoloha.Vrchol2.Souradnice.X-HranaPoloha.Vrchol1.Souradnice.X/HranaPoloha.Vrchol2.Souradnice.Y-HranaPoloha.Vrchol1.Souradnice.Y))/(2*Math.PI))*360;
            //return new Vrchol("automobil",(HranaPoloha.Vrchol1.Souradnice.X + HranaPoloha.Vrchol2.Souradnice.X) / 2, (HranaPoloha.Vrchol1.Souradnice.Y + HranaPoloha.Vrchol2.Souradnice.Y) / 2,);
            return v;
        }
    }
}