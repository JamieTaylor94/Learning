using System;
using System.Collections.Generic;
using System.Linq;

namespace KataPotter.Code
{
    public class Checkout
    {
        private const int SingleBookPrice = 8;

        public decimal GetPriceFor(List<int> books)
        {
            if (books.Count == 0) return 0;

            var groups = GenerateDiscountGroups(books);

            var price = CalculatePrice(groups);

            return price;
        }

        private static decimal CalculatePrice(IEnumerable<DiscountGroup> groups)
        {
            var price = 0m;
            foreach (var group in groups)
            {
                var shouldHaveDiscount = group.Count > 1;
                if (shouldHaveDiscount)
                {
                    price += ApplyDiscountedPrice(group);
                }
                else
                {
                    price += group.Count * SingleBookPrice;
                }
            }

            return price;
        }

        private IEnumerable<DiscountGroup> GenerateDiscountGroups(List<int> books)
        {
            var groups = new List<DiscountGroup>();
            var maxItemsPerGroup = CalculateMaximumItemsInAGroup(books);

            for (var i = 0; i < books.Count + 1; i++)
            {
                var group = new List<int>();
                var booksToIterate = books.ToList();

                foreach (var book in booksToIterate.Where(book => !group.Contains(book) && group.Count < maxItemsPerGroup))
                {
                    group.Add(book);
                    books.Remove(book);
                }

                groups.Add(new DiscountGroup { Group = group });
            }

            return groups;
        }

        private static decimal ApplyDiscountedPrice(DiscountGroup group)
        {
            return group.Count 
                   * SingleBookPrice 
                   * DiscountService.GetDiscountFactor(group.UniqueCount);
        }

        private static int CalculateMaximumItemsInAGroup(IReadOnlyCollection<int> books)
        {
            var buckets = (int) Math.Ceiling((decimal) books.Count / books.Distinct().Count());
            var maxItemsPerBucket = books.Count / buckets;
            return maxItemsPerBucket;
        }
    }
}