using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace AdditionalTask
{
    public class SuperBaggage
    {
        public string[] Properties;
        public List<double> Coords;


        public SuperBaggage(string[] props, List<double> coords )
        {
            Properties = props;
            Coords = coords;
        }

        public override string ToString()
        {
            return string.Join(",", Coords);
        }
    }
}