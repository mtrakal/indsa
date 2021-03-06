﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace aplikace {
    class TextSouborDV : IDisposable, IDatovaVrstva {

        /// <summary>
        /// Podkladový stream.
        /// </summary>
        FileStream fs = null;
        /// <summary>
        /// Stream pro zápis do txt souboru.
        /// </summary>
        StreamWriter sw = null;
        /// <summary>
        /// Stream pro čtení z txt souboru.
        /// </summary>
        StreamReader sr = null;
        /// <summary>
        /// Kontruktor s parametrem <see cref="string"/>, který obsahuje název souboru.
        /// </summary>
        /// <param name="nazev">Parametr datového typu <see cref="string"/> obsahující název souboru.</param>
        public TextSouborDV(string nazev) {
            fs = new FileStream(nazev, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            sw = new StreamWriter(fs, Encoding.UTF8);
            sr = new StreamReader(fs, Encoding.UTF8);
        }

        public void Dispose() {
            sw.Close();
            sr.Close();
            fs.Close();
        }

        public void UlozVrcholy(List<CestyGraf.Vrchol> vrcholy) {
            sw.BaseStream.SetLength(0);

            sw.WriteLine(vrcholy.Count);
            foreach (CestyGraf.Vrchol item in vrcholy) {
                string s = string.Format("{0};{1};{2}",
                    item.Data,
                    item.Souradnice.X,
                    item.Souradnice.Y
                    );
                sw.WriteLine(s);
            }
            sw.Flush();
        }
        public void UlozHrany(List<CestyGraf.Hrana> hrany) {
            sw.BaseStream.SetLength(0);

            sw.WriteLine(hrany.Count);
            foreach (CestyGraf.Hrana item in hrany) {
                string s = string.Format(Konstanty.FORMAT,
                    item.Data,
                    item.Vrchol1.Souradnice.X.ToString(Konstanty.CULTUREINFO),
                    item.Vrchol1.Souradnice.Y.ToString(Konstanty.CULTUREINFO),
                    item.Vrchol2.Souradnice.X.ToString(Konstanty.CULTUREINFO),
                    item.Vrchol2.Souradnice.Y.ToString(Konstanty.CULTUREINFO),
                    item.Metrika,
                    item.Sjizdna
                    );
                sw.WriteLine(s);
            }
            sw.Flush();
        }
        public List<CestyGraf.Vrchol> NactiVrcholy() {
            List<CestyGraf.Vrchol> vrcholy = new List<CestyGraf.Vrchol>();
            if (sr.BaseStream.Length == 0) {
                return vrcholy;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            int pocet = int.Parse(sr.ReadLine());
            string[] pom = null;
            for (int i = 0; i < pocet; i++) {
                pom = null;
                pom = sr.ReadLine().Split(';');
                CestyGraf.Vrchol c = new CestyGraf.Vrchol(pom[0], double.Parse(pom[1]), double.Parse(pom[2]));
                vrcholy.Add(c);
            }
            return vrcholy;
        }
        public List<CestyGraf.Hrana> NactiHrany(ref CestyGraf graf) {
            List<CestyGraf.Hrana> cesty = new List<CestyGraf.Hrana>();
            if (sr.BaseStream.Length == 0) {
                return cesty;
            }
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            int pocet = int.Parse(sr.ReadLine());
            string[] pom = null;
            for (int i = 0; i < pocet; i++) {
                pom = null;
                pom = sr.ReadLine().Split(';');
                CestyGraf.Hrana c = new CestyGraf.Hrana() {
                    Data = pom[0],
                    //Data = string.Format("{0}{1}", Konstanty.ABECEDA[i / Konstanty.ABECEDA.Length], Konstanty.ABECEDA[i % Konstanty.ABECEDA.Length]),
                    //Vrchol1 = graf.DejVrchol(pom[0]),
                    //Vrchol2 = graf.DejVrchol(pom[1]),

                    Vrchol1 = graf.DejVrchol(double.Parse(pom[1], Konstanty.CULTUREINFO), double.Parse(pom[2], Konstanty.CULTUREINFO)),
                    Vrchol2 = graf.DejVrchol(double.Parse(pom[3], Konstanty.CULTUREINFO), double.Parse(pom[4], Konstanty.CULTUREINFO)),
                    Metrika = double.Parse(pom[5], Konstanty.CULTUREINFO),
                    Sjizdna = bool.Parse(pom[6])

                    /*Data = string.Format("{0}{1}", Konstanty.ABECEDA[i / Konstanty.ABECEDA.Length], Konstanty.ABECEDA[i % Konstanty.ABECEDA.Length]),
                    Vrchol1 = new Point(int.Parse(pom[0]), int.Parse(pom[1])),
                    Vrchol2 = new Point(int.Parse(pom[2]), int.Parse(pom[3])),
                    Metrika = int.Parse(pom[4]),
                    Sjizdna = true
                     */
                };
                cesty.Add(c);
            }
            return cesty;
        }
    }
}
