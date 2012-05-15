using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace cz.mtrakal.ADT {
    class Zaznam<T> {
        public int ID { get; set; }
        public PointF V1 { get; set; }
        public PointF V2 { get; set; }
        public int Potomek { get; set; }
        public bool List { get; set; }

        public Zaznam() { }

        public void Uloz(FileStream fs) {
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);
            bw.Write(ID);
            bw.Write(V1.X);
            bw.Write(V1.Y);
            bw.Write(V2.X);
            bw.Write(V2.Y);
            bw.Write(Potomek);
            bw.Write(List);
        }

        public void Nacti(FileStream fs) {
            BinaryReader br = new BinaryReader(fs, Encoding.UTF8);
            ID = br.ReadInt32();
            V1 = new PointF(br.ReadSingle(), br.ReadSingle());
            V2 = new PointF(br.ReadSingle(), br.ReadSingle());
            Potomek = br.ReadInt32();
            List = br.ReadBoolean();
        }
    }
}
