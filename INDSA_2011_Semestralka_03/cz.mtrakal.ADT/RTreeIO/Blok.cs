using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace cz.mtrakal.ADT {
    class Blok<T> : IEnumerable {
        public int KapacitaBloku { get; private set; }
        List<Zaznam<T>> zaznamy;

        public Blok(int kapacitaBloku) {
            KapacitaBloku = kapacitaBloku;
            zaznamy = new List<Zaznam<T>>(KapacitaBloku);
        }

        /// <summary>
        /// Pokud je místo v bloku, přidá se záznam do bloku na první volnou pozici
        /// </summary>
        /// <param name="zaznam"></param>
        public void Pridej(Zaznam<T> zaznam) {
            if (!JePlny()) {
                for (int i = 0; i < zaznamy.Capacity; i++) {
                    if (zaznamy[i] == null) {
                        zaznamy[i] = zaznam;
                        return;
                    }
                }
                throw new IndexOutOfRangeException("Nepovedlo se zapsat do volné kapacity bloku! Tato výjimka by neměla nastat!");
            } else {
                throw new IndexOutOfRangeException("Již je v bloku moc záznamů!");
            }
        }

        /// <summary>
        /// Odebere prvek na indexu
        /// </summary>
        /// <param name="blok"></param>
        /// <returns></returns>
        public Zaznam<T> Odeber(int index) {
            // TODO: nebo dle klíče?
            Zaznam<T> z = zaznamy[index];
            zaznamy[index] = null;
            return z;
        }

        /// <summary>
        /// Zobrazí, zda-li je již celý blok obsazen.
        /// </summary>
        /// <returns><c>true</c> blok je již celý obsazen. <c>false</c> je možné zapsat do bloku další data.</returns>
        public bool JePlny() {
            return KapacitaBloku <= zaznamy.Count;
        }

        /// <summary>
        /// Vrací postupně jednotlivé prvky bloku.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() {
            foreach (Zaznam<T> item in zaznamy) {
                yield return item;
            }
        }

        /// <summary>
        /// Přidá, nebo vrátí prvek na indexu.
        /// </summary>
        /// <param name="blok"></param>
        /// <returns></returns>
        public Zaznam<T> this[int index] {
            get { return zaznamy[index]; }
            set { zaznamy[index] = value; }
        }

        /// <summary>
        /// Vrátí blok hledaného záznamu. Pokud se v bloku nenachází, vrátí -1.
        /// </summary>
        /// <param name="zaznam"></param>
        /// <returns></returns>
        public int DejIndex(Zaznam<T> zaznam) {
            //TODO: zkontrolovat Equals
            for (int i = 0; i < zaznamy.Capacity; i++) {
                if (zaznamy[i].Equals(zaznam)) {
                    return i;
                }
            }
            return -1;
        }
    }
}
