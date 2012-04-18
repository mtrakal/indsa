﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cz.mtrakal.ADT;
using System.Drawing;

namespace Test {
    class Program {
        static void Main(string[] args) {

            //Console.WriteLine(BitConverter.DoubleToInt64Bits(15.2335344534));

            RTree<string, int> rtree = new RTree<string, int>();

            rtree.Vloz("Rr", new Rectangle(33, 52, 5, 11), 110);
            rtree.Vloz("Rm", new Rectangle(30, 63, 3, 7), 111);

            rtree.Vloz("Ra", new Rectangle(5, 5, 10, 10), 101);
            rtree.Vloz("Rx", new Rectangle(10, 10,10,15), 102);
            rtree.Vloz("Rs", new Rectangle(20, 12, 15, 13), 103);

            rtree.Vloz("Rt", new Rectangle(30, 12, 5, 18), 104);
            rtree.Vloz("Rw", new Rectangle(30, 30, 8, 22), 105);
            rtree.Vloz("Rk", new Rectangle(15, 45, 13, 5), 106);

            rtree.Vloz("Rz", new Rectangle(15, 45, 7, 15), 107);
            rtree.Vloz("Rf", new Rectangle(25, 50, 3, 5), 108);
            rtree.Vloz("Rd", new Rectangle(25, 55, 8, 8), 109);


            rtree.PostavStrom();

            List<int> listBodove =  rtree.VyhledejBodove(new PointF(30, 20));
            List<int> listIntervalove = rtree.VyhledejIntervalove(new RectangleF(7, 3, 10, 12));

            Console.ReadLine();
        }
    }
}
