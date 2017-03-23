using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalTask
{
    public class ItemCluster
    {
        public List<Item> Content = new List<Item>();
        public List<double> StartCoords;
        public SuperCenter Center;

        public ItemCluster() {

        }

        public ItemCluster(List<double> startCoords)
        {
            StartCoords = startCoords;
        }

        public ItemCluster(List<Item> content, List<double> startCoords)
        {
            Content = content;
            StartCoords = startCoords;
        }
        public override string ToString() {
            string temp = "Content:\n";

            foreach (var c in Content) {
               temp += c.ToString() + "\n";
            }

            temp += "\n" + "Cluster indexes: ";
            for (int i = 0; i < StartCoords.Count; i++)
                temp += StartCoords[i] + ", ";

            return temp;
        }


        public double GetDistance(List<double> pointCoords) {
            double result = 0;

            for (int i = 0; i < pointCoords.Count; i++) {
                result += Math.Pow(Center.Coords[i] - pointCoords[i], 2);
            }

            result = Math.Sqrt(result);

            return result;

        }
    }
}
