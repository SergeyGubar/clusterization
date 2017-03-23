using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalTask
{
    public class Item
    {
        public double Value;
        public List<double> Coords;

        private static Random rand = new Random();

        public Item()
        {
            Coords = new List<double>();
        }

        public Item(int number, int min, int max)
        {
            this.Value = 0;
            this.Coords = new List<double>();
            SetRandomCoords(number, min, max);
        }
        public Item(double value)
        {
            Coords = new List<double>();
            Value = value;
        }
        public Item(double value, List<double> coords)
        {
            Coords = new List<double>();
            Coords.AddRange(coords);
            Value = value;
        }

        public string GetCoords()
        {
            string s = "";
            foreach (var coord in Coords) {
                s += coord + ", ";
            }

            return s;
        }

        public double GetDistance(Item item)
        {
            double sum = 0;

            for (int i = 0; i < Coords.Count; i++) {
                sum += Math.Pow(Coords[i] - item.Coords[i], 2);
            }

            return Math.Sqrt(sum);
        }

        public void SetRandomCoords(int coordsNumer, int min, int max)
        {
            for (int i = 0; i < coordsNumer; i++) {
                Coords.Add(rand.Next(min, max));
            }
        }

        public override string ToString()
        {
            return String.Format("Item coords: {0}", GetCoords());
        }
    }
}
