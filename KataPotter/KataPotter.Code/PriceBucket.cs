using System.Collections.Generic;
using System.Linq;

namespace KataPotter.Code
{
    public class PriceBucket
    {
        public List<int> Books { get; set; }

        public int Count => Books.Count;
        public int UniqueCount => Books.Distinct().Count();
    }
}