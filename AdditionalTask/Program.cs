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

            #region TestWithDelta

            //List<Baggage> testList = new List<Baggage>();

            //for (int i = 0; i < 10; i++) {
            //    Baggage temp = new Baggage(random.Next(0, 100), random.Next(0, 100));
            //    testList.Add(temp);
            //}


            //Cluster[,] testClusters = ClasterizationWithDelta(testList, 20);

            //for (int i = 0; i < testClusters.GetLength(0); i++) {
            //    for (int j = 0; j < testClusters.GetLength(1); j++) {
            //        if (testClusters[i, j].Content.Count == 0) {
            //            Console.WriteLine();
            //            Console.WriteLine("Fragility of the cluster: {0} - {1}", testClusters[i, j].startX, testClusters[i, j].endX);

            //            Console.WriteLine("Weight of the cluster: {0} - {1}", testClusters[i, j].startY, testClusters[i, j].endY);
            //            Console.WriteLine("No elements in this cluster");
            //            Console.WriteLine();
            //        }

            //        for (int k = 0; k < testClusters[i, j].Content.Count; k++) {



            //            Console.WriteLine();
            //            Console.WriteLine("Fragility of the cluster: {0} - {1}", testClusters[i, j].startX, testClusters[i, j].endX);

            //            Console.WriteLine("Weight of the cluster: {0} - {1}", testClusters[i, j].startY, testClusters[i, j].endY);

            //            Console.Write("Fragility of the baggage: " + testClusters[i, j].Content[k].Fragility + " ");
            //            Console.Write("Weight of the baggage: " + testClusters[i, j].Content[k].Weight + " ");

            //            Console.WriteLine();
            //        }

            //    }
            //}

            #endregion

            #region TestWithNumber
            string[] properties = { "Weight", "Fragility"};
            List<SuperCluster> testClustersList = new List<SuperCluster>();
            List<SuperBaggage> testListBaggages = new List<SuperBaggage>();

            for (int i = 0; i < 10; i++) {

                List<double> temp = new List<double>();
                

                for (int j = 0; j < properties.Length; j++) {
                    temp.Add(random.Next(0,100));
                }

                testListBaggages.Add(new SuperBaggage(properties, temp));


            }


            testClustersList = ClusterizationWithNumber(testListBaggages, 3, properties);

            Console.WriteLine(testClustersList);

            foreach (SuperCluster currCluster in testClustersList) {
                Console.WriteLine(currCluster);
            }






            #endregion

            Console.ReadKey();


            
        }

        public static List<Cluster> ClusterizationWithNumber(List<Baggage> list, int number) {
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

                    double minDistance = temp[0].GetDistance(currentSuperBaggage.Coords);
                    SuperCluster minCluster = temp[0];

                    foreach (SuperCluster cluster in temp) {
                        double currentDistance = cluster.GetDistance(currentSuperBaggage.Coords);
                        if (currentDistance <= minDistance) {
                            minDistance = currentDistance;
                            minCluster = cluster;
                        }
                    }
                    

                    minCluster.Content.Add(currentSuperBaggage);

                }

                bool isCompleted = true;

                foreach (SuperCluster currentCluster in temp) {

                    List<double> midCoordinates = new List<double>();
                    List<double> sumCoordinates = new List<double>();

                    foreach (SuperBaggage currentSuperBaggage in listOfBaggages) {
                        for (int i = 0; i < currentSuperBaggage.Coords.Count; i++) {
                            sumCoordinates.Add(currentSuperBaggage.Coords[i]);
                        }
                    }


                    for (int i = 0; i < sumCoordinates.Count; i++) {
                        midCoordinates.Add(sumCoordinates[i] / currentCluster.Content.Count);
                    }

                    for (int j = 0; j < currentCluster.Center.Coords.Count; j++) {
                        
                        if (Math.Abs(currentCluster.Center.Coords[j] - midCoordinates[j]) > 0.5) {
                           
                            currentCluster.Center.Coords[j] = midCoordinates[j];
                            isCompleted = false;
                            currentCluster.Content.Clear(); // <----- ШПИОН

                        }

                        //TODO пофиксить вот то что сверху ^
                        
                    }
                    



                }
                if (isCompleted) {
                    break;

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
