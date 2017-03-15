using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                Baggage temp = new Baggage(random.Next(0, 100), random.Next(0, 100));
                testList.Add(temp);
            }

            //List<Cluster> testClusters = ClasterizationWithNumber(testList, 2);

            //foreach (Cluster cluster in testClusters) {
            //    Console.WriteLine(cluster.ToString());
            //    Console.WriteLine(new string('-', 50));
            //}
            Cluster[,] testClusters = ClasterizationWithDelta(testList, 20);

            for (int i = 0; i < testClusters.GetLength(0); i++) {
                for (int j = 0; j < testClusters.GetLength(1); j++) {
                    for (int k = 0; k < testClusters[i, j].Content.Count; k++) {
                        //Console.Write("Start Fragility of the cluster: " + testClusters[i, j].startX + " ");
                        //Console.Write("End Fragility of the cluster: " + testClusters[i, j].endX + " ");
                        Console.WriteLine();
                        Console.WriteLine("Fragility of the cluster: {0} - {1}", testClusters[i, j].startX, testClusters[i, j].endX);
                        //Console.Write("Start Weight of the cluster: " + testClusters[i, j].startY + " ");
                        //Console.Write("End Weight of the cluster: " + testClusters[i, j].endY + " ");
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

        public static List<SuperCluster> ClusterizationWithNumber(List<SuperBaggage> listOfBaggages, int number, string[] props)
        {
            List<SuperCluster> temp = new List<SuperCluster>();


            for (int i = 0; i < number; i++) {

                SuperCluster tempCluster = new SuperCluster(); //создаем временный кластер

                List<double> centerValues = new List<double>();

                for (int j = 0; j < props.Length; j++) {        
                    centerValues.Add(random.Next(0,100));
                }

                tempCluster.Center = new SuperCenter(centerValues); //заполняем центр кластера 
                temp.Add(tempCluster);  //добавим этот кластер в результат
                

            }

            while (true) {
                foreach (SuperBaggage currentSuperBaggage in listOfBaggages) {
                    
                }
            }







            return temp;
        }

        public static Cluster[,] ClasterizationWithDelta(List<Baggage> list, int delta)
        {




            double minFragility = 0;
            double minWeight = 0;
            double maxWeight = 0;
            double maxFragility = 0;




            foreach (Baggage current in list) {
                if (current.Weight > maxWeight) {
                    maxWeight = current.Weight;
                }
                if (current.Fragility > maxFragility) {
                    maxFragility = current.Fragility;
                }
            }

           

            double width = maxFragility;
            double height = maxWeight;

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
                int index1 = (int)(currentBaggage.Weight / delta);
                int index2 = (int)(currentBaggage.Fragility / delta);
                temp[index1,index2].Content.Add(currentBaggage);
            }


            return temp;




        }
    }
}
