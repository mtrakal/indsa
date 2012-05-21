using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cz.mtrakal.ADT;

namespace Test {
    class Program {
        static void Main(string[] args) {
            const int pocetPrvku = 100;
            const int prvkuVBloku = 5;

            BlokPrenos bp = new BlokPrenos("blokPrenos.dat", prvkuVBloku, sizeof(long));
            bp.ClearFile();

            for (int i = 0; i < pocetPrvku; i++) {
                if (i == 2) {
                    bp.Flush();
                }
                bp.Write(BitConverter.GetBytes((long)i));
            }
            bp.Flush();

            //for (int i = 0; i < pocetPrvku; i++) {
            //    Console.WriteLine(BitConverter.ToInt64(bp.Read(i / prvkuVBloku, i % prvkuVBloku), 0).ToString());
            //}

            foreach (byte[] item in bp) {
                Console.WriteLine(BitConverter.ToInt64(item, 0).ToString());
            }

            Console.WriteLine();
            Console.WriteLine(bp.GetBlockCount());
            Console.ReadLine();
        }
    }
}
