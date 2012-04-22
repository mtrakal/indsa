using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cz.mtrakal.ADT;
using System.Drawing;
using System.Diagnostics;

namespace Test {
    class Program {
        static void Main(string[] args) {

            //Console.WriteLine(BitConverter.DoubleToInt64Bits(15.2335344534));

            RTree<string, int> rtree = new RTree<string, int>();
            rtree.Vloz("Rr", new PointF(33, 63), new PointF(38, 52), 110);
            rtree.Vloz("Rm", new PointF(30, 70), new PointF(33, 63), 111);

            rtree.Vloz("Ra", new PointF(5, 5), new PointF(10, 10), 101);
            rtree.Vloz("Rx", new PointF(10, 10), new PointF(20, 25), 102);
            rtree.Vloz("Rs", new PointF(20, 25), new PointF(35, 12), 103);

            rtree.Vloz("Rt", new PointF(30, 30), new PointF(35, 12), 104);
            rtree.Vloz("Rw", new PointF(30, 30), new PointF(38, 52), 105);
            rtree.Vloz("Rk", new PointF(15, 45), new PointF(28, 50), 106);

            rtree.Vloz("Rz", new PointF(15, 45), new PointF(22, 60), 107);
            rtree.Vloz("Rf", new PointF(25, 55), new PointF(28, 50), 108);
            rtree.Vloz("Rd", new PointF(25, 55), new PointF(33, 63), 109);


            rtree.PostavStrom();

            List<int> listBodove = rtree.VyhledejBodove(new PointF(10, 10));
            List<int> listIntervalove = rtree.VyhledejIntervalove(new Rectangle(7, 3, 10, 12));

            Debug.WriteLine("");
            Debug.WriteLine(string.Format("{0,-5}\t{1}\t{2,15}\t{3,10}\t{4,10}\t{5,5}\t{6,5}\t{7,5}\t{8,5}\t{9,5}\t{10,5}", "Data", "Key", "Z-order", "X order", "Y ordr", "V1.X", "V1.Y", "V2.X", "V2.Y", "Width", "Height"));
            foreach (RTree<string, int>.RVrchol item in rtree.poleListu) {
                item.GetZOrder();
                //Debug.WriteLine(item.Data + "\t" + item.Key);
            }
            rtree.DebugStrom();
            Console.ReadLine();
        }
    }
}
