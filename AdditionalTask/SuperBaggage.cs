using System.Collections.Generic;

namespace AdditionalTask
{
    public class SuperBaggage
    {
        public string[] Properties;

        public SuperBaggage(string[] props, List<double> coords )
        {
            Properties = props;
            Coords = coords;
        }

        public List<double> Coords;
    }
}