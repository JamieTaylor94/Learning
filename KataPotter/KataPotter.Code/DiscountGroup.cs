using System.Collections.Generic;
using System.Linq;

namespace KataPotter.Code
{
    public class DiscountGroup
    {
        public List<int> Group { get; set; }

        public int Count => Group.Count;
        public int UniqueCount => Group.Distinct().Count();
    }
}