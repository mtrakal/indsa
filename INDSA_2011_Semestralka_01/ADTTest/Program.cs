using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using cz.mtrakal.ADT;
using System.Collections;

namespace ConsoleApplication1 {
    class PriorityQueueA<TValue, TPriority> where TPriority : IComparable {
        private class PQPoint {
            public TValue Value { get; set; }
            public TPriority Priority { get; set; }
        }
        private SortedDictionary<TPriority, Queue<PQPoint>> sortedDict = new SortedDictionary<TPriority, Queue<PQPoint>>();

        public int Count { get; private set; }
        public bool Empty { get { return Count == 0; } }

        public void Enqueue(TValue val, TPriority pri) {
            ++Count;
            if (!sortedDict.ContainsKey(pri)) {
                sortedDict[pri] = new Queue<PQPoint>();
            }
            PQPoint pq = new PQPoint() { Priority = pri, Value = val };
            sortedDict[pri].Enqueue(pq);
        }

        public TValue Dequeue() {
            --Count;
            var item = sortedDict.First();
            if (item.Value.Count == 1) {
                sortedDict.Remove(item.Key);
            }
            return item.Value.Dequeue().Value;
        }
    }
    class Stopky {
        Stopwatch s = new Stopwatch();

        public void Start() {
            s.Start();
        }
        public void Stop(string popis) {
            s.Stop();
            Debug.WriteLine(popis + ": " + s.ElapsedMilliseconds);
            s.Reset();
        }
    }
    class Program {
        static void Main(string[] args) {
            LinkedList<double> ll = new LinkedList<double>();
            StreamReader sr = new StreamReader("./../../data.txt");

            while (!sr.EndOfStream) {
                ll.AddLast(double.Parse(sr.ReadLine()));
            }

            Stopky s = new Stopky();
            pqMischel(ref s, ref ll, "PriorityQueue Mischel collection");
            //pq22(ref s, ref ll, "PriorityQueue2 s kapacitou");
            //pq2(ref s, ref ll, "PriorityQueue2");
            //pq1(ref s, ref ll, "PriorityQueue1 na BST");
            //al1(ref s, ref ll, "ArrayList bez sortu"); // nekonečný jen načítání, asi nefunguje
            //pq3(ref s, ref ll, "PriorityQueue3");
            //ll1(ref s, ref ll, "LinkedList bez sortu");
            //bst1(ref s, ref ll, "BST");
            //l1(ref s, ref ll, "List bez sortu");
            //sortDict1(ref s, ref ll, "Sorted Dictionary");
            //dict1(ref s, ref ll, "Dictionary bez sortu");
        }

        private static void pqMischel(ref Stopky s, ref LinkedList<double> ll, string popis) {
            Mischel.Collections.PriorityQueue<double, double> collection = new Mischel.Collections.PriorityQueue<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Enqueue(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Dequeue();
            }
            s.Stop("Mazání z " + popis);
        }

        static void sortDict1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            SortedDictionary<double, double> collection = new SortedDictionary<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Add(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Remove(collection.First().Key);
            }
            s.Stop("Mazání z " + popis);
        }
        static void l1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            List<double> collection = new List<double>();
            s.Start();
            foreach (double item in ll) {
                collection.Add(item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.RemoveAt(0);
            }
            s.Stop("Mazání z " + popis);
        }
        static void dict1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            Dictionary<double, double> collection = new Dictionary<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Add(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Remove(collection.First().Key);
            }
            s.Stop("Mazání z " + popis);
        }
        static void ll1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            LinkedList<double> collection = new LinkedList<double>();
            s.Start();
            foreach (double item in ll) {
                collection.AddLast(item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.RemoveFirst();
            }
            s.Stop("Mazání z " + popis);
        }
        static void bst1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            BST<double, double> collection = new BST<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Add(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.DejMin();
            }
            s.Stop("Mazání z " + popis);
        }
        static void pq22(ref Stopky s, ref LinkedList<double> ll, string popis) {
            cz.mtrakal.ADT.ADTPriorityQueue.PriorityQueue<double, double, double> collection = new cz.mtrakal.ADT.ADTPriorityQueue.PriorityQueue<double, double, double>(ll.Count);
            s.Start();
            foreach (double item in ll) {
                collection.Enqueue(item, item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Dequeue();
            }
            s.Stop("Mazání z " + popis);
        }
        static void pq2(ref Stopky s, ref LinkedList<double> ll, string popis) {
            cz.mtrakal.ADT.ADTPriorityQueue.PriorityQueue<double, double, double> collection = new cz.mtrakal.ADT.ADTPriorityQueue.PriorityQueue<double, double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Enqueue(item, item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Dequeue();
            }
            s.Stop("Mazání z " + popis);
        }
        static void pq3(ref Stopky s, ref LinkedList<double> ll, string popis) {
            PriorityQueue<double, double> collection = new PriorityQueue<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Enqueue(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Dequeue();
            }
            s.Stop("Mazání z " + popis);
        }
        static void pq1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            PriorityQueueA<double, double> collection = new PriorityQueueA<double, double>();
            s.Start();
            foreach (double item in ll) {
                collection.Enqueue(item, item);
            }
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.Dequeue();
            }
            s.Stop("Mazání z " + popis);
        }
        static void al1(ref Stopky s, ref LinkedList<double> ll, string popis) {
            ArrayList collection = new ArrayList();
            s.Start();
            foreach (double item in ll) {
                collection.Add(item);
            }
            //collection.Sort();
            s.Stop("Přidání do " + popis);
            s.Start();
            while (collection.Count != 0) {
                collection.RemoveAt(0);
            }
            s.Stop("Mazání z " + popis);
        }
    }
}
