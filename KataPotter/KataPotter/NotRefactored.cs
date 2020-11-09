using System.Collections.Generic;
using System.Linq;

namespace KataPotter
{
    /*
        Ignore: Unrefactored functional algorithm for reference
    */
    class NotRefactored
    {
        public decimal GetPrice(List<int> books)
        {
            const int bookPrice = 8;

            var discounts = new Dictionary<int, decimal>
            {
                {5, 0.75m},
                {4, 0.80m},
                {3, 0.90m},
                {2, 0.95m},
                {1, 1}
            };

            var groups = new List<List<int>>();

            var booksToIterate = books;

            while (booksToIterate.Distinct().Count() > 1)
            {
                var list = new List<int>();
                foreach (var book in booksToIterate.ToList())
                {
                    if (!list.Contains(book))
                    {
                        list.Add(book);
                        booksToIterate.Remove(book);
                    }
                }

                groups.Add(list);
            }

            var totals = 0m;

            foreach (var group in groups)
            {
                totals += group.Count * bookPrice * discounts[group.Distinct().Count()];
            }

            var leftOverFullPriceBooks = booksToIterate.Count * bookPrice;

            return totals + leftOverFullPriceBooks;
        }
    }
}
