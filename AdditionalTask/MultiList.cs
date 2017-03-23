using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalTask
{
    public class MultiList
    {
        private List<Item> Items;
        private int Dimentions;

        public MultiList()
        {
            Dimentions = 0;
            Items = new List<Item>();
        }
        public MultiList(int dimentions)
        {
            Dimentions = dimentions;
            Items = new List<Item>();
        }
        public MultiList(List<Item> items)
        {
            Items.AddRange(items);
            Dimentions = items[0]?.Coords.Count ?? 0;
        }

        public Item GetItem(List<double> coords)
        {
            foreach (var item in Items) {
                if (item.Coords.SequenceEqual(coords))
                    return item;
            }
            return null;
        }
        public void Add(Item item)
        {
            try {
                if (item.Coords.Count == Dimentions)
                    Items.Add(item);
                else throw new Exception("Dimentions not match.");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
        }

        public override string ToString()
        {
            string s = "Dimentions: " + Dimentions + "\n";
            
            foreach (var Item in Items) {
                s += String.Format(Item + "\n");
            }

            return s;
        }
    }
}
