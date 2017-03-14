namespace AdditionalTask
{
    public class SuperBaggage
    {
        double _cost;
        double _fragility;
        double _weight;
        double _volume;

        public SuperBaggage(double cost, double fragility, double )
        {
            
        }
        public double Cost
        {
            get { return _cost; }
            set {
                if (value > 0) {
                    _cost = value;
                }
            }
        }

        public double Fragility
        {
            get { return _fragility; }
            set {
                if (value >= 0 && value <= 100) {
                    _fragility = value;
                }
            }
        }

        public double Weight
        {
            get { return _weight; }
            set {
                if (value >= 0) {
                    _weight = value;
                }
            }
        }

        public double Volume
        {
            get { return _volume; }
            set {
                if (value > 0) {
                    _volume = value;
                }
            }
        }
    }
}