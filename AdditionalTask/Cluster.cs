using System;
using System.Collections.Generic;

namespace AdditionalTask {
    class Cluster {
        public List<Baggage> Content = new List<Baggage>();

        public double startX;
        public double startY;
        public double endX;
        public double endY;


        public Point Center;

        
        public Cluster() {
            
        }
        public override string ToString() {
            string temp = "Cluster with center in " + "Fragility: "+ Center.X  + " Weight: " + Center.Y + "\n";
            for (int i = 0; i < Content.Count; i++) {
                temp += Content[i].Print() + "\n";

            }
            return temp;

        }
        
        public double GetDistance(double x, double y) {
            return Math.Sqrt(Math.Pow(x - Center.X, 2) + Math.Pow(y - Center.Y, 2));
        }
        public Cluster(List<Baggage> list) {
            Content = list;
        }

    }
}