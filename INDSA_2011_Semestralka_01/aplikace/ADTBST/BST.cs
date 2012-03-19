using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace aplikace {
    public class BST<T, TKey> where TKey : IComparable {
        private class DVrchol {
            public DVrchol Pravy { get; set; }
            public DVrchol Levy { get; set; }
            public DVrchol Rodic { get; set; }
            public T Data { get; set; }
            public TKey Klic { get; set; }
            public DVrchol() { }
            public DVrchol(TKey klic) : this() { Klic = klic; }
            public DVrchol(TKey klic, T data) : this(klic) { Data = data; }
            public DVrchol(TKey klic, T data, DVrchol pravy) : this(klic, data) { Pravy = pravy; }
            public DVrchol(TKey klic, T data, DVrchol pravy, DVrchol levy) : this(klic, data, pravy) { Levy = levy; }
        }
        DVrchol koren;
        public BST() { koren = null; }
        private DVrchol vlozR(DVrchol v, TKey klic, T data, DVrchol rodic) { // OK
            if (v == null) {
                return new DVrchol(klic, data) { Rodic = rodic };
            }
            if (klic.CompareTo(v.Klic) == -1) {
                v.Levy = vlozR(v.Levy, klic, data, v);
            } else {
                v.Pravy = vlozR(v.Pravy, klic, data, v);
            }
            return v;
        }

        public void Vloz(TKey klic, T data) {
            koren = vlozR(koren, klic, data, null);
        }

        public bool JePrazdny() {
            if (koren == null) {
                return true;
            }
            return false;
        }
        public T DejMax() { return dej(koren, true); }
        public T DejMin() { return dej(koren, false); }
        private T dej(DVrchol vrchol, bool maximum) {
            if (JePrazdny()) {
                return default(T);
            }
            T data;
            DVrchol aktualni = vrchol;
            if (maximum) {
                // prohledávání k maximálnímu prvku
                while (aktualni.Pravy != null) {
                    aktualni = aktualni.Pravy;
                }
                data = aktualni.Data;
                if (jeKoren(aktualni)) {
                    koren = aktualni.Levy;
                    aktualni.Levy.Rodic = null;
                    return data;
                }
                if (jeVrchol(aktualni)) {
                    aktualni.Rodic.Pravy = null;
                } else {
                    aktualni.Levy.Rodic = aktualni.Rodic;
                    aktualni.Rodic.Pravy = aktualni.Levy;
                }
                return data;
            } else {
                // prohledávání k minimálnímu prvku
                while (aktualni.Levy != null) {
                    aktualni = aktualni.Levy;
                }
                data = aktualni.Data;
                if (jeKoren(aktualni)) {
                    koren = aktualni.Pravy;
                    aktualni.Pravy.Rodic = null;
                    return data;
                }
                if (jeVrchol(aktualni)) {
                    aktualni.Rodic.Levy = null;
                    aktualni = null; //nefajčí, musí se před rodiče :(
                } else {
                    aktualni.Pravy.Rodic = aktualni.Rodic;
                    aktualni.Rodic.Levy = aktualni.Pravy;
                }
                return data;
            }
        }
        private bool jeVrchol(DVrchol vrchol) {
            if (vrchol.Pravy == null && vrchol.Levy == null) {
                return true;
            }
            return false;
        }
        private bool jeKoren(DVrchol vrchol) {
            if (vrchol.Rodic == null) {
                return true;
            }
            return false;
        }
        #region MyRegion
        //void Preorder(DVrchol v) { // Preorder
        //    if (v == null) {
        //        return;
        //    } else {
        //        //v.tiskVrcholu();
        //        Preorder(v.Levy);
        //        Preorder(v.Pravy);
        //    }
        //}

        //public void PruchodPreorder() {
        //    Preorder(koren);
        //}

        //void Inorder(DVrchol v) { // Inorder
        //    if (v == null) {
        //        return;
        //    } else {
        //        Inorder(v.Levy);
        //        //v.tiskVrcholu();
        //        Inorder(v.Pravy);
        //    }
        //}

        //public void PruchodInorder() {
        //    Inorder(koren);
        //}

        //void Postorder(DVrchol v) { //Postorder
        //    if (v == null) {
        //        return;
        //    } else {
        //        Postorder(v.Levy);
        //        Postorder(v.Pravy);
        //        //v.tiskVrcholu();
        //    }
        //}

        //public void PruchodPostorder() {
        //    Postorder(koren);
        //}

        //private T hledejR(DVrchol v, TKey klic) { // Vyhledavani
        //    if (v == null) {
        //        return default(T);
        //    }
        //    if (klic.Equals(v.Klic)) {
        //        return v.Data;
        //    }
        //    if (klic.CompareTo(v.Klic) == 1) { // TODO: zkontrolovat podmínku
        //        return hledejR(v.Levy, klic);
        //    } else {
        //        return hledejR(v.Pravy, klic);
        //    }
        //}

        //public T Hledej(TKey klic) {
        //    return hledejR(koren, klic);
        //} 
        #endregion
    }
}
