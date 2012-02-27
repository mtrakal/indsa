using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test {
    class Program {
        static void Main(string[] args) {
            int[,] matice = {
                            { 0, 1, 3, 0, 0 }, 
                            { 1, 0, 0, 2, 5 }, 
                            { 3, 0, 0, 6, 0 }, 
                            { 0, 2, 6, 0, 0 }, 
                            { 0, 5, 0, 0, 0 } 
                            };

            //FloydWarshall<double> fw = new FloydWarshall<double>();
            //string vystup = fw.GetPath(fw.GetFloydWarshall(matice), 0, 4);
            //Console.WriteLine(vystup);
            //Console.ReadLine();
        }
    }
}
