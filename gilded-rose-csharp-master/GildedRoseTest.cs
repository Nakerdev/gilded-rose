using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private int _maxQuality = 50;

        [Test]
        public void QualityShouldNeverBeNegative()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Foo",
                    SellIn = 1,
                    Quality = 0
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(0);
        }

        [Test]
        public void SellInShouldDecreaseEachDay()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Foo",
                    SellIn = 2,
                    Quality = 1
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().SellIn.Should().Be(1);
        }

        [Test]
        public void QualityShouldDegradeTwiceAsFastWhenTheSellByDateHasPassed()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Foo",
                    SellIn = 0,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(0);
        }

        [Test]
        public void AgedBrieItemShouldIncreaseItsQualityTheOlderItGetsWhileSellDateHasNotPassed()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Aged Brie",
                    SellIn = 1,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(3);
        }

        [Test]
        public void AgedBrieItemShouldIncreaseTwiceAsFastItsQualityWhenTheSellByDateHasPassed()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Aged Brie",
                    SellIn = 0,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(4);
        }

        [Test]
        public void QualityShouldNeverBeGreaterThanMaxQuality()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Aged Brie",
                    SellIn = 2,
                    Quality = _maxQuality
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(_maxQuality);
        }
        
        [Test]
        public void SulfurasItemShouldNeverDecreaseItsQualityAndSellIn()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = 2,
                    Quality = 80 //Business requirement xD
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().SellIn.Should().Be(2);
            items.First().Quality.Should().Be(80);
        }
        
        [Test]
        public void BackstageTicketItemShouldIncreaseAtDefaultSpeedWhenSellInDaysAreGreaterThan10()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 11,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(3);
        }
        
        [Test]
        public void BackstageTicketItemShouldIncreaseTwiceAsFastWhenCloseToExpiring()
        {
            var closeToExpiring = 10;
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = closeToExpiring,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(4);
        }
        
        [Test]
        public void BackstageTicketItemShouldIncreaseThriceAsFastWhenVeryCloseToExpiring()
        {
            var veryCloseToExpiring = 5;
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = veryCloseToExpiring,
                    Quality = 2
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(5);
        }
        
        [Test]
        public void BackstageTicketItemShouldHaveNoQualityWhenSellInHasPassed()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 0,
                    Quality = 0
                }
            };
            var gildedRose = new GildedRose(Items: items);

            gildedRose.UpdateQuality();

            items.First().Quality.Should().Be(0);
        }
    }
}