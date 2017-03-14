using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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
                Baggage temp = new Baggage(random.Next(0, 100), random.Next(0, 100));
                testList.Add(temp);
            }


            Cluster[,] testClusters = ClasterizationWithDelta(testList, 20);

            for (int i = 0; i < testClusters.GetLength(0); i++) {
                for (int j = 0; j < testClusters.GetLength(1); j++) {
                    for (int k = 0; k < testClusters[i, j].Content.Count; k++) {

                        Console.WriteLine();
                        Console.WriteLine("Fragility of the cluster: {0} - {1}", testClusters[i, j].startX, testClusters[i, j].endX);

                        Console.WriteLine("Weight of the cluster: {0} - {1}", testClusters[i, j].startY, testClusters[i, j].endY);

                        Console.Write("Fragility of the baggage: " + testClusters[i, j].Content[k].Fragility + " ");
                        Console.Write("Weight of the baggage: " + testClusters[i, j].Content[k].Weight + " ");
                        Console.WriteLine();
                    }

                }
            }



        }

        public static List<Cluster> ClasterizationWithNumber(List<Baggage> list, int number) {
            List<Cluster> clusterList = new List<Cluster>();
     
            for (int i = 0; i < number; i++) {
                Cluster temp = new Cluster();
                temp.Center = new Point(random.Next(0,100), random.Next(0,100));
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

        public static List<SuperCluster> ClasterizationWithNumber1(List<SuperBaggage> baggages, int number,)
        {
            List<SuperCluster> temp = new List<SuperCluster>();


            return temp;
        }

        public static Cluster[,] ClasterizationWithDelta(List<Baggage> list, int delta) {

            

            double minWeight = list[0].Weight;
            double minFragility = list[0].Fragility;
            double maxWeight = list[0].Weight;
            double maxFragility = list[0].Fragility;



            foreach (Baggage currentBaggage in list) {
                if (currentBaggage.Weight < minWeight) {
                    minWeight = currentBaggage.Weight;
                } else if (currentBaggage.Weight > maxWeight) {
                    maxWeight = currentBaggage.Weight;
                }
                if (currentBaggage.Fragility < minFragility) {
                    minFragility = currentBaggage.Fragility;
                } else if (currentBaggage.Fragility > maxFragility) {
                    maxFragility = currentBaggage.Fragility;
                }
            }

            int xClusters = (int)Math.Ceiling(minFragility / delta);  //number of clusters deleted because of relative coordinates
            int yClusters = (int)Math.Ceiling(minWeight / delta);

            double width = maxFragility - minFragility;
            double height = maxWeight - minWeight;

            //Point[,] arr = new Point[Convert.ToInt32(Math.Ceiling(width / delta)),Convert.ToInt32(Math.Ceiling(height/delta))];

            Cluster[,] temp = new Cluster[Convert.ToInt32(Math.Ceiling(height / delta)),Convert.ToInt32(Math.Ceiling(width / delta))];

            

            for (int i = 0; i < temp.GetLength(0); i++) {
                for (int j = 0; j < temp.GetLength(1); j++) {
                    temp[i, j] = new Cluster();
                }
            }

            for (int i = 0; i < temp.GetLength(0); i++) {
                for (int j = 0; j < temp.GetLength(1); j++) {

                    temp[i, j].startX = minFragility + delta * j;
                    temp[i, j].endX = minFragility + delta * (j + 1);
                    temp[i, j].startY = minWeight + delta * i;
                    temp[i, j].endY = minWeight + delta * (i + 1);


                }
            }



            foreach (Baggage currentBaggage in list) {
                int index1 = (int)((currentBaggage.Weight / delta) - yClusters);
                int index2 = (int)((currentBaggage.Fragility / delta) - xClusters);
                temp[index1,index2].Content.Add(currentBaggage);
            }


            return temp;




        }
    }
}
