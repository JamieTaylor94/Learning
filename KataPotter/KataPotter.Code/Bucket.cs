using System.Collections.Generic;
using System.Linq;

namespace KataPotter.Code
{
    public class Bucket
    {
        public List<int> Items { get; set; }
        public int Count => Items.Count;
        public int UniqueCount => Items.Distinct().Count();

        public void Add(int item)
        {
            Items.Add(item);
        }

        public bool CanBeAdded(int item, int maxSize)
        {
            return Items.Contains(item) == false
                   && Count < maxSize;
        }
    }
}