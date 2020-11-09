using System.Collections.Generic;
using System.Linq;
using KataPotter.Code.Discount;

namespace KataPotter.Code
{
    public class Checkout
    {
        private readonly DiscountService _discountService;
        private const int SingleBookPrice = 8;

        public Checkout()
        {
            _discountService = new DiscountService();
        }

        public decimal GetPriceFor(List<int> books)
        {
            if (books.Count == 0) return 0;

            var comboPrices = new List<decimal>();

            foreach (var discount in DiscountService.DiscountCombinations)
            {
                var bookCopy = new List<int>(books);
                var groups = _discountService.GetDiscountGroups(bookCopy, discount.Keys.Count);

                var discountedBookTotal = groups.DiscountGroups.Sum(g => (g.Count * SingleBookPrice) * discount[g.UniqueCount]);
                var fullPriceBookTotal = groups.LeftOverBooks.Count * SingleBookPrice;

                comboPrices.Add(discountedBookTotal + fullPriceBookTotal);
            }

            return comboPrices.Min();
        }
    }
}