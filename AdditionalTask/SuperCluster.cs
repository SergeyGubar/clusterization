using System;
using System.Collections.Generic;

namespace AdditionalTask
{
    

    public class SuperCluster
    {
        public List<SuperBaggage> Content = new List<SuperBaggage>();

        public SuperCenter Center;

        public SuperCluster()
        {
            
        }

        public double[] StartCoords;
        public double[] EndCoords;

        public SuperCluster(double[] arr, double[] arr2)
        {
            StartCoords = arr;
            EndCoords = arr2;
        }

        public double GetDistance(List<double> pointCoords)
        {
            double result = 0;

            foreach (var coord in pointCoords) {
                result += Math.Pow(coord, 2);
            }

            result = Math.Sqrt(result);


            return result;

        }



    }
}