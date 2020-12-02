using System.Collections.Generic;

namespace KataPotter.Code
{
    public class DiscountService
    {
        private static readonly Dictionary<int, decimal> DiscountFactors = new Dictionary<int, decimal>()
        {
            {5, 0.75m},
            {4, 0.80m},
            {3, 0.90m},
            {2, 0.95m}
        };
    
        public static decimal GetDiscountFactor(int groupSize)
        {
            return DiscountFactors[groupSize];
        }
    }
}