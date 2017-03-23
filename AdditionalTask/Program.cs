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
            //string[] properties = { "Weight", "Fragility"};
            //List<SuperCluster> testClustersList = new List<SuperCluster>();
            //List<SuperBaggage> testListBaggages = new List<SuperBaggage>();

            //for (int i = 0; i < 10; i++) {

            //    List<double> temp = new List<double>();


            //    for (int j = 0; j < properties.Length; j++) {
            //        temp.Add(random.Next(0,100));
            //    }

            //    testListBaggages.Add(new SuperBaggage(properties, temp));


            //}


            //testClustersList = ClusterizationWithNumber(testListBaggages, 3, properties);

            //Console.WriteLine(testClustersList);

            //foreach (SuperCluster currCluster in testClustersList) {
            //    Console.WriteLine(currCluster);
            //}






            #endregion

            #region TestWithSuperDelta

            int delta = 20;

            List<Item> items = new List<Item>();

            for (int i = 0; i < 128; i++) {
                items.Add(new Item(0, RandCoords(4, 0, 60)));
            }

            List<ItemCluster> cl = MegaClusterizationWithDelta(items, delta);
            Console.WriteLine("Delta: " + delta);
            foreach (var c in cl) {
                Console.Write("Cluster n ------------------------------------\n");
                Console.WriteLine(c.ToString() + "\n");
            }

            #endregion


            Console.ReadKey();

            

        }
        static Random rand = new Random();
        public static List<double> RandCoords(int cords, int min, int max)
        {
            List<double> coords = new List<double>();
            for (int i = 0; i < cords; i++) {
                coords.Add(rand.Next(min, max));
            }
            return coords;
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
                    }       
                }
                if (isCompleted) {
                    break;

                }

            }
            return temp;
        }

        public static List<ItemCluster> MegaClusterizationWithNumber
            (List<Item> items, int number, string[] props)
        {
            List<double> maxCoords = GetMaxCoords(items);
            List<ItemCluster> clusters = new List<ItemCluster>();
            List<Item> vectors = new List<Item>();
            List<List<double>> distances = new List<List<double>>();
            List<double> approximateDistances = new List<double>();
            List<ItemCluster> tempClusters = new List<ItemCluster>();

            // Creating clusters
            for (int i = 0; i < number; i++)
                clusters.Add(new ItemCluster());

            // Creating vectors
            for (int i = 0; i < props.Length; i++) {
                vectors.Add(new Item(props.Length, 0, (int)maxCoords[i] + 1));
                tempClusters.Add(new ItemCluster());
            }

            // Creating list of distances,
            //  where: each list in distances is a distances from item[i] to some vector.
            //         distance[i][j] is a [j] coordinate of some vector.
            for (int i = 0; i < items.Count; i++) {
                distances.Add(new List<double>());
            }

            while (true) {
                // Getting each list in distances
                for (int i = 0; i < distances.Count; i++) {
                    // Getting each n-vector
                    for (int j = 0; j < vectors.Count; j++) {
                        // Getting each list in distances
                        for (int k = 0; k < items.Count; k++) {
                            distances[i].Add(vectors[j].GetDistance(items[k]));
                        }
                    }
                }


                for (int i = 0; i < distances.Count; i++) {
                    int index = 0;
                    double min = distances[i][0];

                    for (int j = 0; j < distances[i].Count; j++) {
                        if (distances[i][j] < min) {
                            min = distances[i][j];
                            index = j;
                        }
                    }


                }

            }

            return clusters;
        }
        public static Cluster[,] ClusterizationWithDelta(List<Baggage> list, int delta)
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

            Cluster[,] temp = new Cluster[Convert.ToInt32
                (Math.Ceiling(height / delta) + 10),
                Convert.ToInt32(Math.Ceiling(width / delta)) + 10];          

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

        public static List<ItemCluster> MegaClusterizationWithDelta
            (List<Item> items, double delta)
        {
            List<double> minCoords = GetMinCoords(items);
            List<double> maxCoords = GetMaxCoords(items);
            List<int> dimsCells = maxCoords.Select(c => Convert.ToInt32(Math.Ceiling(c / delta))).ToList();

            int clustersCount = 1;
            foreach (var d in dimsCells) {
                clustersCount *= d;
            }

            List<ItemCluster> clusters = new List<ItemCluster>();           

            foreach (var item in items) {
                List<double> cords = new List<double>();

                for (int i = 0; i < item.Coords.Count; i++)
                    cords.Add(Math.Floor(item.Coords[i] / delta));

                if (clusters.Count == 0) {
                    clusters.Add(new ItemCluster(new List<Item>() { item }, cords));
                }
                else {
                    bool wasAdded = false;
                    foreach(var clust in clusters) {
                        if (clust.StartCoords.SequenceEqual(cords)) {
                            wasAdded = true;
                            clust.Content.Add(item);
                            break;
                        }                         
                    }

                    if (!wasAdded) {
                        clusters.Add(new ItemCluster(new List<Item>() { item }, cords));
                    }
                }           
            }
       

            return clusters;
        }

        public static List<double> GetMinCoords(List<Item> items)
        {
            List<double> retList = new List<double>();

            for (int i = 0; i < items[0].Coords.Count; i++) {
                retList.Add(items[0].Coords[i]);
            }

            foreach (var item in items) {
                for (int i = 0; i < item.Coords.Count; i++) {
                    retList[i] = Math.Min(item.Coords[i], retList[i]);
                }
            }
            return retList;
        }
        public static List<double> GetMaxCoords(List<Item> items) {
            List<double> retList = new List<double>(items[0].Coords.Count);

            for (int i = 0; i < items[0].Coords.Count; i++) {
                retList.Add(items[0].Coords[i]);
            }

            foreach (var item in items) {
                for (int i = 0; i < item.Coords.Count; i++) {
                    retList[i] = Math.Max(item.Coords[i], retList[i]);
                }
            }
            return retList;
        }


    }
}
