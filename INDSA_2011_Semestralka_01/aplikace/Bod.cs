using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    class Bod {
        public double X { get; set; }
        public double Y { get; set; }
        public Bod() {

        }
        public Bod(double x, double y) {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj) {
            if (this.X == (obj as Bod).X && this.Y == (obj as Bod).Y) {
                return true;
            }
            return false;
        }
        public override int GetHashCode() {
            if (X == 0 && Y == 0) {
                // Ensure that 0 and -0 have the same hash code 
                return 0;
            }
            long value = (long)(X+Y);
            return unchecked((int)value) ^ ((int)(value >> 32));
        }
    }
}
