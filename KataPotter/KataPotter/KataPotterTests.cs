using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace KataPotter
{
    [TestFixture]
    public class KataPotterTests
    {
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _checkout = new Checkout();
        }

        [Test]
        public void ZeroBooks_PleaseLeaveTheStore()
        {
            var books = new List<int>();
            var price = _checkout.GetPriceFor(books);
            Assert.AreEqual(0, price);
        }

        [Test]
        public void OneBook_CostsEightPounds()
        {
            var books = new List<int> {1};

            var price = _checkout.GetPriceFor(books);

            Assert.AreEqual(8, price);
        }

        [Test]
        public void TwoOfTheSameBooks_CostsSixteenPounds()
        {
            var books = new List<int> {1, 1};

            var price = _checkout.GetPriceFor(books);

            Assert.AreEqual(16, price);
        }

        [TestCase("1,2,3,4,5", 30)]
        [TestCase("1,2,3", 21.6)]
        [TestCase("1,2", 15.2)]

        public void UniqueBooks_HaveTheCorrectDiscount(string bookStr, decimal expectedPrice)
        {
            var books = bookStr.Split(',')
                .Select(int.Parse).ToList();

            var price = _checkout.GetPriceFor(books);

            Assert.AreEqual(expectedPrice, price);
        }

        [Test]
        public void TwoSetsOf5Books_Returns25PercentDiscount()
        {
            var books = new List<int>
            {
                1, 2, 3, 4, 5, 1, 2, 3, 4, 5
            };

            var price = _checkout.GetPriceFor(books);

            Assert.AreEqual(60, price);
        }

        [Test]
        public void Answer()
        {
            var books = new List<int> {1, 1, 2, 2, 3, 3, 4, 5};

            var price = _checkout.GetPriceFor(books);

            Assert.AreEqual(51.20, price);
        }
    }
}