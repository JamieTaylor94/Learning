using System.Collections.Generic;
using System.Linq;
using KataPotter.Models;

namespace KataPotter.Services
{
    public class DiscountService
    {
        public static readonly List<Dictionary<int, decimal>> DiscountCombinations = new List<Dictionary<int, decimal>>
        {
            new Dictionary<int, decimal>
            {
                {5, 0.75m},
                {4, 0.80m},
                {3, 0.90m},
                {2, 0.95m},
                {1, 1}
            },
            new Dictionary<int, decimal>
            {
                {4, 0.80m},
                {3, 0.90m},
                {2, 0.95m},
                {1, 1}
            }
        };

        public DiscountGrouping GetDiscountGroups(List<int> books, int maxDiscountFactor)
        {
            var groups = new DiscountGrouping();

            var booksToIterate = books;

            while (booksToIterate.Distinct().Count() > 1)
            {
                var list = new List<int>();
                foreach (var book in booksToIterate.ToList())
                {
                    if (!list.Contains(book) && list.Count < maxDiscountFactor)
                    {
                        list.Add(book);
                        booksToIterate.Remove(book);
                    }
                }

                groups.Add(new DiscountGroup {Group = list});
            }

            groups.LeftOverBooks = booksToIterate;

            return groups;
        }
    }
}