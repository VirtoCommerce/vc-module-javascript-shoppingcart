using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using VirtoCommerce.Platform.Core.Common;
using DomainCartModels = VirtoCommerce.Domain.Cart.Model;
using DomainCommerceModels = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    // TechDebt: Need to use Automapper here where possible.
    // Current problem - pass constructor parameters to the child object.
    // Link to mitigate the problem using resolution context: http://codebuckets.com/2016/09/24/passing-parameters-with-automapper/
    public static class CartConverter
    {
        public static ShoppingCart ToShoppingCart(this DomainCartModels.ShoppingCart cart, Currency currency, Language language)
        {
            var result = new ShoppingCart(currency, language)
            {
                ChannelId = cart.ChannelId,
                Comment = cart.Comment,
                CustomerId = cart.CustomerId,
                CustomerName = cart.CustomerName,
                Id = cart.Id,
                Name = cart.Name,
                OrganizationId = cart.OrganizationId,
                Status = cart.Status,
                StoreId = cart.StoreId,
                Type = cart.Type,
                HasPhysicalProducts = true,
            };

            if (cart.Coupons != null)
            {
                result.Coupons = cart.Coupons.Select(coupon => new Coupon { Code = coupon, AppliedSuccessfully = !string.IsNullOrEmpty(coupon) }).ToList();
            }

            if (cart.Payments != null)
            {
                result.Payments = cart.Payments.Select(payment => ToPayment(payment, result)).ToList();
            }

            if (cart.Shipments != null)
            {
                result.Shipments = cart.Shipments.Select(shipment => ToShipment(shipment, result)).ToList();
            }

            if (cart.TaxDetails != null)
            {
                result.TaxDetails = cart.TaxDetails.Select(taxDetail => ToTaxDetail(taxDetail, currency)).ToList();
            }

            result.DiscountAmount = new Money(cart.DiscountAmount, currency);
            result.HandlingTotal = new Money(cart.HandlingTotal, currency);
            result.HandlingTotalWithTax = new Money(cart.HandlingTotalWithTax, currency);

            result.Total = new Money(cart.Total, currency);
            result.SubTotal = new Money(cart.SubTotal, currency);
            result.SubTotalWithTax = new Money(cart.SubTotalWithTax, currency);
            result.ShippingPrice = new Money(cart.ShippingSubTotal, currency);
            result.ShippingPriceWithTax = new Money(cart.ShippingSubTotalWithTax, currency);
            result.ShippingTotal = new Money(cart.ShippingTotal, currency);
            result.ShippingTotalWithTax = new Money(cart.ShippingTotalWithTax, currency);
            result.PaymentPrice = new Money(cart.PaymentSubTotal, currency);
            result.PaymentPriceWithTax = new Money(cart.PaymentSubTotalWithTax, currency);
            result.PaymentTotal = new Money(cart.PaymentTotal, currency);
            result.PaymentTotalWithTax = new Money(cart.PaymentTotalWithTax, currency);

            result.DiscountTotal = new Money(cart.DiscountTotal, currency);
            result.DiscountTotalWithTax = new Money(cart.DiscountTotalWithTax, currency);
            result.TaxTotal = new Money(cart.TaxTotal, currency);

            result.IsAnonymous = cart.IsAnonymous;
            result.IsRecuring = cart.IsRecuring == true;
            result.VolumetricWeight = cart.VolumetricWeight ?? 0;
            result.Weight = cart.Weight ?? 0;

            return result;
        }

        public static Payment ToPayment(this DomainCartModels.Payment payment, ShoppingCart cart)
        {
            var result = new Payment(cart.Currency)
            {
                Id = payment.Id,
                OuterId = payment.OuterId,
                PaymentGatewayCode = payment.PaymentGatewayCode,
                TaxType = payment.TaxType,

                Amount = new Money(payment.Amount, cart.Currency),
            };

            if (payment.BillingAddress != null)
            {
                result.BillingAddress = ToAddress(payment.BillingAddress);
            }

            result.Price = new Money(payment.Price, cart.Currency);
            result.DiscountAmount = new Money(payment.DiscountAmount, cart.Currency);
            result.PriceWithTax = new Money(payment.PriceWithTax, cart.Currency);
            result.DiscountAmountWithTax = new Money(payment.DiscountAmountWithTax, cart.Currency);
            result.Total = new Money(payment.Total, cart.Currency);
            result.TotalWithTax = new Money(payment.TotalWithTax, cart.Currency);
            result.TaxTotal = new Money(payment.TaxTotal, cart.Currency);
            result.TaxPercentRate = (decimal?)payment.TaxPercentRate ?? 0m;

            if (payment.TaxDetails != null)
            {
                result.TaxDetails = payment.TaxDetails.Select(taxDetail => ToTaxDetail(taxDetail, cart.Currency)).ToList();
            }

            if (!payment.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(payment.Discounts.Select(discount => ToDiscount(discount, new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }

        public static Shipment ToShipment(this DomainCartModels.Shipment shipment, ShoppingCart cart)
        {
            var result = new Shipment(cart.Currency)
            {
                Id = shipment.Id,
                MeasureUnit = shipment.MeasureUnit,
                ShipmentMethodCode = shipment.ShipmentMethodCode,
                ShipmentMethodOption = shipment.ShipmentMethodOption,
                WeightUnit = shipment.WeightUnit,
                Height = (double?)shipment.Height,
                Weight = (double?)shipment.Weight,
                Width = (double?)shipment.Width,
                Length = (double?)shipment.Length,
                Currency = cart.Currency,
                Price = new Money(shipment.Price, cart.Currency),
                PriceWithTax = new Money(shipment.PriceWithTax, cart.Currency),
                DiscountAmount = new Money(shipment.DiscountAmount, cart.Currency),
                Total = new Money(shipment.Total, cart.Currency),
                TotalWithTax = new Money(shipment.TotalWithTax, cart.Currency),
                DiscountAmountWithTax = new Money(shipment.DiscountAmountWithTax, cart.Currency),
                TaxTotal = new Money(shipment.TaxTotal, cart.Currency),
                TaxPercentRate = (decimal?)shipment.TaxPercentRate ?? 0m,
            };

            if (shipment.DeliveryAddress != null)
            {
                result.DeliveryAddress = ToAddress(shipment.DeliveryAddress);
            }

            if (shipment.Items != null)
            {
                result.Items = shipment.Items.Select(shipmentItem => ToShipmentItem(shipmentItem, cart)).ToList();
            }

            if (shipment.TaxDetails != null)
            {
                result.TaxDetails = shipment.TaxDetails.Select(taxDetail => ToTaxDetail(taxDetail, cart.Currency)).ToList();
            }

            if (!shipment.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(shipment.Discounts.Select(discount => ToDiscount(discount, new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }

        public static TaxDetail ToTaxDetail(this DomainCommerceModels.TaxDetail taxDetail, Currency currency)
        {
            var result = new TaxDetail(currency)
            {
                Name = taxDetail.Name,
                Rate = new Money(taxDetail.Rate, currency),
                Amount = new Money(taxDetail.Amount, currency),
            };
            return result;
        }

        public static Address ToAddress(this DomainCommerceModels.Address address)
        {
            var result = new Address
            {
                Key = address.Key,
                City = address.City,
                CountryCode = address.CountryCode,
                CountryName = address.CountryName,
                Email = address.Email,
                FirstName = address.FirstName,
                LastName = address.LastName,
                Line1 = address.Line1,
                Line2 = address.Line2,
                MiddleName = address.MiddleName,
                Name = address.Name,
                Organization = address.Organization,
                Phone = address.Phone,
                PostalCode = address.PostalCode,
                RegionId = address.RegionId,
                RegionName = address.RegionName,
                Zip = address.Zip,
                Type = (AddressType)Enum.Parse(typeof(AddressType), address.AddressType.ToString(), true),
            };
            return result;
        }

        public static Discount ToDiscount(this DomainCommerceModels.Discount discount, IEnumerable<Currency> currencies, Language language)
        {
            var defaultCurrency = currencies.FirstOrDefault(currency => currency.Equals(discount.Currency)) ?? new Currency(language, discount.Currency);

            var result = new Discount(defaultCurrency)
            {
                Coupon = discount.Coupon,
                Description = discount.Description,
                PromotionId = discount.PromotionId,
                Amount = new Money(discount.DiscountAmount, defaultCurrency),
            };

            return result;
        }

        public static CartShipmentItem ToShipmentItem(this DomainCartModels.ShipmentItem shipmentItem, ShoppingCart cart)
        {
            var result = new CartShipmentItem
            {
                Id = shipmentItem.Id,
                Quantity = shipmentItem.Quantity,
                LineItem = cart.Items.FirstOrDefault(lineItem => lineItem.Id == shipmentItem.LineItemId),
            };

            return result;
        }
    }
}
