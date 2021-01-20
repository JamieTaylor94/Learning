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

        private decimal CalculatePrice(IEnumerable<Bucket> buckets)
        {
            var price = 0m;
            foreach (var bucket in buckets)
            {
                var shouldHaveDiscount = bucket.UniqueCount > 1;
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

        private IEnumerable<Bucket> PopulatePriceBuckets(List<int> books)
        {
            var buckets = new List<Bucket>();
            var maxItemsPerBucket = CalculateMaximumItemsInABucket(books);
            
            foreach (var book in books)
            {
                var existingBuckets = buckets.Where(bucket => bucket.CanBeAdded(book, maxItemsPerBucket)).ToList();

                if (existingBuckets.Any())
                {
                    existingBuckets.First().Add(book);
                }
                else
                {
                    CreateBucket(buckets, book);
                }
            }

            return buckets;
        }

        private static void CreateBucket(List<Bucket> buckets, int book)
        {
            buckets.Add(new Bucket
            {
                Items = new List<int> {book}
            });
        }

        private static decimal ApplyDiscountedPrice(Bucket bucket)
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