using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace AdditionalTask
{
    

    public class SuperCluster
    {
        public List<SuperBaggage> Content = new List<SuperBaggage>();

        public SuperCenter Center;
        //TODO: OVERRIDE TOSTRING

        public SuperCluster()
        {
            
        }

        

        public override string ToString()
        {

            string temp = "";

            //foreach (double coord in Center.Coords) {
            //    int i = 1;
            //    temp += ("Coord with name " + Content + " equals to + " + coord);
            //}

            for (int i = 0; i < Center.Coords.Count; i++) {
                temp += "Center coord with name: " + Content[i].Properties[i] + " = " + Center.Coords[i] + "\n";
            }

            temp += "\n";

            temp += "In this cluster there are next baggages: \n";


            foreach (SuperBaggage bag in Content) {
                for (int i = 0; i < bag.Coords.Count; i++) {
                    temp += "Coord " + i + " = " + bag.Coords[i] + " ";
                } 
                temp += "\n";
            }
            temp += new string('-', 25);

            return temp;
        }

       
        public double GetDistance(List<double> pointCoords)
        {
            double result = 0;

            

            for (int i = 0; i < pointCoords.Count; i++) {
                result += Math.Pow(Center.Coords[i] - pointCoords[i], 2);
            }

            result = Math.Sqrt(result);


            return result;

        }



    }
}