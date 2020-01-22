namespace VirtoCommerce.JavaScriptShoppingCart.Test.Services
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Moq;

    using VirtoCommerce.JavaScriptShoppingCart.Crawling;
    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Exceptions;
    using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;

    using Xunit;

    public class DefaultCrawlerTests
    {
        private readonly Uri _testUri = new Uri("http://localhost");

        [Fact]
        public async void CrawlAsync_EmptyMapping_ResultShouldHaveMappingException()
        {
            // arrange
            var crawler = new DefaultCrawler(Mock.Of<ICrawlingConfiguration>(
                t => t.Mapping == new Dictionary<CrawlingAttributeType, string>()));

            // act
            var result = await crawler.CrawlAsync(_testUri);

            // assert
            result.Exception.Should().BeOfType<MappingException>();
        }

        [Fact]
        public async void CrawlAsync_EmptyMapping_IsSuccessShouldBeFalse()
        {
            // arrange
            var crawler = new DefaultCrawler(Mock.Of<ICrawlingConfiguration>(
                t => t.Mapping == new Dictionary<CrawlingAttributeType, string>()));

            // act
            var result = await crawler.CrawlAsync(_testUri);

            // assert
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async void CrawlAsync_EmptyMapping_IsSuccessShouldBeTrue()
        {
            // arrange
            var crawler = new DefaultCrawler(Mock.Of<ICrawlingConfiguration>(
                t => t.Mapping == PredefinedMappings.DefaultMapping));

            // act
            var result = await crawler.CrawlAsync(_testUri);

            // assert
            result.IsSuccess.Should().Be(true);
        }
    }
}
