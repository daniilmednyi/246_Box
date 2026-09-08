using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _246_Box
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
