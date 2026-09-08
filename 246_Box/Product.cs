using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _246_Box
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public override string ToString() => $"{Name} (осталось: {Quantity})";

        
    }
}
