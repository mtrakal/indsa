using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace aplikace {
    public class Graf<TBod, TVrchol, THrana, TMetrika> {
        public interface IBod : IEqualityComparer<IBod> {
            TBod X { get; set; }
            TBod Y { get; set; }
        }
        public interface IVrchol {
            TVrchol Data { get; set; }
            IBod Souradnice { get; set; }
            void PridejHranu(IHrana hrana);
            IHrana DejHranu(THrana data);
            List<IHrana> DejHrany();
            bool OdeberHranu(THrana data);
        }
        public interface IHrana {
            THrana Data { get; set; }
            IVrchol Vrchol1 { get; set; }
            IVrchol Vrchol2 { get; set; }
            TMetrika Metrika { get; set; }
        }

        public class GBod : IBod {
            public TBod X { get; set; }
            public TBod Y { get; set; }
            public GBod() { }
            public GBod(TBod x, TBod y) { X = x; Y = y; }
            public override bool Equals(object obj) {
                return Equals(this, obj as IBod);
            }
            public bool Equals(IBod x, IBod y) {
                if (ReferenceEquals(x, y)) {
                    return true;
                }
                if ((x == null) || (y == null)) {
                    return false;
                }
                if (x.X.Equals(y.X) && x.Y.Equals(y.Y)) {
                    return true;
                }
                return false;
            }
            public int GetHashCode(IBod obj) {
                throw new NotImplementedException("not impl");
            }
        }
        public class GVrchol : IVrchol {
            List<IHrana> seznamHran = new List<IHrana>();
            public TVrchol Data { get; set; }
            public IBod Souradnice { get; set; }
            public void PridejHranu(IHrana hrana) {
                seznamHran.Add(hrana);
            }
            public IHrana DejHranu(THrana data) {
                foreach (IHrana item in seznamHran) {
                    if (item.Data.Equals(data)) {
                        return item;
                    }
                }
                return null;
            }
            public List<IHrana> DejHrany() {
                return seznamHran;
            }
            public bool OdeberHranu(THrana data) {
                foreach (GHrana item in seznamHran) {
                    if (item.Data.Equals(data)) {
                        return seznamHran.Remove(item as IHrana);
                    }
                }
                return false;
            }
        }
        public class GHrana : IHrana {
            public THrana Data { get; set; }
            public IVrchol Vrchol1 { get; set; }
            public IVrchol Vrchol2 { get; set; }
            public TMetrika Metrika { get; set; }
        }
        protected Dictionary<IBod, IVrchol> vrcholy = new Dictionary<IBod, IVrchol>();
        protected Dictionary<THrana, IHrana> hrany = new Dictionary<THrana, IHrana>();
        #region Vrcholy
        public void Pridej(IVrchol vrchol) {
            if (vrcholy.ContainsKey(vrchol.Souradnice)) {
                throw new Exception("Klíč již existuje Vrcholy-Pridej");
            }
            vrcholy.Add(vrchol.Souradnice, vrchol);
        }
        public void Pridej(List<IVrchol> vrcholy) {
            foreach (IVrchol item in vrcholy) {
                Pridej(item);
            }
        }
        public bool Odeber(IBod souradnice) {
            if (vrcholy.ContainsKey(souradnice)) {
                return vrcholy.Remove(souradnice);
            } else {
                throw new ArgumentException("Neznámý parametr klíče Vrcholy-Odeber!");
            }
        }
        public IVrchol DejVrchol(IBod souradnice) {
            if (vrcholy.ContainsKey(souradnice as IBod)) {
                return vrcholy[souradnice];
            } else {
                throw new Exception("Vrchol cesty se nenachází v seznamu vrcholů!");
                //return null;
            }
        }
        public IVrchol DejVrchol(TBod x, TBod y) { // does not work due to: IEqualityComparer is not implemented on abstract layer
            return DejVrchol(new GBod(x, y));
        }
        public List<IVrchol> DejVrcholy() {
            List<IVrchol> vystup = new List<IVrchol>();
            foreach (KeyValuePair<IBod, IVrchol> item in vrcholy) {
                vystup.Add(item.Value);
            }
            return vystup;
        }
        public int CountVrcholy() {
            return vrcholy.Count;
        }
        #endregion
        #region Hrany
        public void Pridej(IHrana hrana) {
            if (hrany.ContainsKey(hrana.Data)) {
                throw new Exception("Klíč již existuje Hrany-Pridej");
            }
            hrany.Add(hrana.Data, hrana);
        }
        public void Pridej(List<IHrana> hrany) {
            foreach (IHrana item in hrany) {
                Pridej(item);
            }
        }
        public bool Odeber(THrana data) {
            if (hrany.ContainsKey(data)) {
                return hrany.Remove(data);
            } else {
                throw new ArgumentException("Neznámý parametr klíče v Hrany-Odeber!");
            }
        }

        public IHrana DejHranu(THrana data) {
            if (hrany.ContainsKey(data)) {
                return hrany[data];
            } else {
                return null;
            }
        }
        public List<IHrana> DejHrany() {
            List<IHrana> vystup = new List<IHrana>();
            foreach (KeyValuePair<THrana, IHrana> item in hrany) {
                vystup.Add(item.Value);
            }
            return vystup;
        }
        public int CountHrany() {
            return hrany.Count;
        }
        #endregion
    }
}
