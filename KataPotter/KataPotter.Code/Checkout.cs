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

            var buckets = PopulatePriceBuckets(books);

            var price = CalculatePrice(buckets);

            return price;
        }

        private decimal CalculatePrice(IEnumerable<PriceBucket> buckets)
        {
            var price = 0m;
            foreach (var bucket in buckets)
            {
                var shouldHaveDiscount = bucket.Count > 1;
                if (shouldHaveDiscount)
                {
                    price += ApplyDiscountedPrice(bucket);
                }
                else
                {
                    price += bucket.Count * SingleBookPrice;
                }
            }

            return price;
        }

        private IEnumerable<PriceBucket> PopulatePriceBuckets(List<int> books)
        {
            var buckets = new List<PriceBucket>();
            var maxItemsPerBucket = CalculateMaximumItemsInABucket(books);

            for (var i = 0; i <= books.Count; i++)
            {
                var bucket = new List<int>();
                var booksToIterate = books.ToList();

                foreach (var book in booksToIterate.Where(book => CanBeAddedToBucket(book, bucket, maxItemsPerBucket)))
                {
                    bucket.Add(book);
                    books.Remove(book);
                }

                buckets.Add(new PriceBucket { Books = bucket });
            }

            return buckets;
        }

        private static bool CanBeAddedToBucket(int book, List<int> bucket, int maxItemsPerBucket)
        {
            return bucket.Contains(book) == false 
                   && bucket.Count < maxItemsPerBucket;
        }

        private static decimal ApplyDiscountedPrice(PriceBucket bucket)
        {
            return bucket.Count
                   * SingleBookPrice
                   * DiscountService.GetDiscountFactor(bucket.UniqueCount);
        }

        private static int CalculateMaximumItemsInABucket(List<int> books)
        {
            var buckets = (int)Math.Ceiling((decimal) books.Count / books.Distinct().Count());
            var maxItemsPerBucket = books.Count / buckets;
            return maxItemsPerBucket;
        }
    }
}