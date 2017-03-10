using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalTask
{
    class Program
    {
        public static Random random = new Random();

        static void Main(string[] args)
        {
            List<Baggage> testList = new List<Baggage>();

            for (int i = 0; i < 10; i++) {
                Baggage temp = new Baggage(random.Next(0,100), random.Next(0,100));
                testList.Add(temp);
            }

            List<Cluster> testClusters = ClasterizationWithNumber(testList, 2);
            //
            foreach (Cluster cluster in testClusters) {
                Console.WriteLine(cluster.ToString());
                Console.WriteLine(new string('-',50));
            }



        }

        public static List<Cluster> ClasterizationWithNumber(List<Baggage> list, int number) {
            List<Cluster> clusterList = new List<Cluster>();
     
            for (int i = 0; i < number; i++) {
                Cluster temp = new Cluster();
                temp.Center = new WeightCenter(random.Next(0,100), random.Next(0,100));
                clusterList.Add(temp);
            }
           
            while (true) {
                foreach (Baggage currentBaggage in list) {
                    double minDistance = clusterList[0].GetDistance(currentBaggage.Fragility, currentBaggage.Weight);
                    Cluster minCluster = new Cluster();
                    foreach (Cluster cluster in clusterList) {
                        double currentDistance = cluster.GetDistance(currentBaggage.Fragility, currentBaggage.Weight);
                        if (currentDistance <= minDistance) {
                            minDistance = currentDistance;
                            minCluster = cluster;
                        }
                    }
                    minCluster.Content.Add(currentBaggage);
                }

                bool isCompleted = true;
                foreach (Cluster cluster in clusterList) {

                    double midX;
                    double midY;
                    double sumX = 0;
                    double sumY = 0;
                    foreach (Baggage baggage in cluster.Content) {
                        sumX += baggage.Fragility;
                        sumY += baggage.Weight;
                    }
                    midX = sumX / cluster.Content.Count;
                    midY = sumY / cluster.Content.Count;
                    if (Math.Abs(cluster.Center.X - midX) > 1 || Math.Abs(cluster.Center.Y - midY) > 1) {
                        cluster.Center.X = midX;
                        cluster.Center.Y = midY;
                        isCompleted = false;
                        cluster.Content.Clear();
                    }
                    
                    
                }
                if (isCompleted) {
                    break;

                }
                

            }
            return clusterList;

        }
    }
}
