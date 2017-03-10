using System;

namespace AdditionalTask {
    class Baggage {
        public string Print() {
            return String.Format("This baggage fragility is: {0} and the weight is: {1}", Fragility, Weight);
        }

        private double _weight;
        private double _fragility;

        public Baggage(double weight, double fragility) {
            Weight = weight;
            Fragility = fragility;
        }

        public double Weight {
            get {
                return _weight;
            }
            set {
                if (value > 0) {
                    _weight = value;
                }
            }
        }

        public double Fragility {
            get {
                return _fragility;
            }
            set {
                if (value >= 0 && value <= 100) {
                    _fragility = value;
                }
            }
        }

    }
}