using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace cz.mtrakal.ADT {
    class RTreeFile<T> {
        FileStream file;
        int velikostIndexuBloku = sizeof(int);
        public int VelikostZaznamu { get; private set; }
        public int PocetZaznamuBloku { get; private set; }
        public int VelikostBloku { get; private set; }
        private Dictionary<int, Blok<T>> buffer = new Dictionary<int, Blok<T>>();


        public RTreeFile(string fileName, int velikostZaznamu = sizeof(int), int pocetZaznamuBloku = 4) {
            file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            VelikostZaznamu = velikostZaznamu;
            PocetZaznamuBloku = pocetZaznamuBloku;
            VelikostBloku = pocetZaznamuBloku * pocetZaznamuBloku;
        }

        private void VytvorHlavicku() {
            byte[] hlavicka = Encoding.UTF8.GetBytes(VelikostBloku.ToString().PadLeft(velikostIndexuBloku, '0'));
            file.Write(hlavicka, 0, velikostIndexuBloku);
        }

        /// <summary>
        /// Funkce zapiš zapíše do souboru data na jeho konec
        /// </summary>
        /// <param name="data"></param>
        /// <param name="baseFile"></param>
        public void Zapis(Blok<T> data) {
            BinaryWriter bw = new BinaryWriter(file, Encoding.UTF8);
        }

        public Blok<T> Nacti(int index) {
            BinaryReader br = new BinaryReader(file, Encoding.UTF8);
            byte[] byteBuffer = new byte[VelikostBloku];
            br.Read(byteBuffer, index, VelikostBloku);
            // TODO: zkontrolovat, případně napsat SetByteArray
            return (byteBuffer as Blok<T>);
        }
    }
}
