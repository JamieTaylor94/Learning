using System.Collections.Generic;

namespace KataPotter.Models
{
    public class DiscountGrouping
    {
        public List<DiscountGroup> DiscountGroups { get; set; }
        public List<int> LeftOverBooks { get; set; }

        public DiscountGrouping()
        {
            DiscountGroups = new List<DiscountGroup>();
        }

        public void Add(DiscountGroup group)
        {
            DiscountGroups.Add(group);
        }
    }
}