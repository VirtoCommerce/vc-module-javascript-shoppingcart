using System.Globalization;
using FluentAssertions;
using VirtoCommerce.JavaScriptShoppingCart.Crawling;
using VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests;
using Xunit;

namespace VirtoCommerce.JavaScriptShoppingCart.Test
{
    public class AddCartLineItemRequestTests
    {
        [Fact]
        public void Equals_DifferentObjects_ShouldBeFalse()
        {
            // arrange
            var empty = string.Empty;
            var request = new AddCartLineItemRequest();
            var other = new CrawlingItem("id", "sku", "price", empty, "3", empty, empty, empty);

            // act
            var result = request.Equals(other);

            // assert
            result.Should().Be(false);
        }

        [Fact]
        public void Equals_SameObjects_ShouldBeTrue()
        {
            // arrange
            const string productId = "productId";
            const string sku = "sku";
            const decimal price = 30.5m;
            const int quantity = 3;
            var empty = string.Empty;
            var request = new AddCartLineItemRequest
            {
                CatalogId = productId,
                Sku = sku,
                ListPrice = price,
                Quantity = quantity,
            };

            var other = new CrawlingItem(
                productId,
                sku,
                price.ToString(CultureInfo.InvariantCulture),
                empty,
                quantity.ToString(),
                empty,
                empty,
                empty);

            // act
            var result = request.Equals(other);

            // assert
            result.Should().Be(true);
        }
    }
}
