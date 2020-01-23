using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using VirtoCommerce.Platform.Core.Common;
using cartDto = VirtoCommerce.Domain.Cart.Model;
using coreDto = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    // TechDebt: Need to use Automapper here where possible. Current problem - pass constructor parameters to the child object.
    // Link to mitigate the problem using resolution context: http://codebuckets.com/2016/09/24/passing-parameters-with-automapper/
    public static partial class CartConverter
    {
        public static ShoppingCart ToShoppingCart(this cartDto.ShoppingCart cartDto, Currency currency, Language language)
        {
            var result = new ShoppingCart(currency, language)
            {
                ChannelId = cartDto.ChannelId,
                Comment = cartDto.Comment,
                CustomerId = cartDto.CustomerId,
                CustomerName = cartDto.CustomerName,
                Id = cartDto.Id,
                Name = cartDto.Name,
                OrganizationId = cartDto.OrganizationId,
                Status = cartDto.Status,
                StoreId = cartDto.StoreId,
                Type = cartDto.Type,
                HasPhysicalProducts = true,
            };

            if (cartDto.Coupons != null)
            {
                result.Coupons = cartDto.Coupons.Select(c => new Coupon { Code = c, AppliedSuccessfully = !string.IsNullOrEmpty(c) }).ToList();
            }

            if (cartDto.Payments != null)
            {
                result.Payments = cartDto.Payments.Select(p => ToPayment(p, result)).ToList();
            }

            if (cartDto.Shipments != null)
            {
                result.Shipments = cartDto.Shipments.Select(s => ToShipment(s, result)).ToList();
            }

            if (cartDto.TaxDetails != null)
            {
                result.TaxDetails = cartDto.TaxDetails.Select(td => ToTaxDetail(td, currency)).ToList();
            }

            result.DiscountAmount = new Money(cartDto.DiscountAmount, currency);
            result.HandlingTotal = new Money(cartDto.HandlingTotal, currency);
            result.HandlingTotalWithTax = new Money(cartDto.HandlingTotalWithTax, currency);

            result.Total = new Money(cartDto.Total, currency);
            result.SubTotal = new Money(cartDto.SubTotal, currency);
            result.SubTotalWithTax = new Money(cartDto.SubTotalWithTax, currency);
            result.ShippingPrice = new Money(cartDto.ShippingSubTotal, currency);
            result.ShippingPriceWithTax = new Money(cartDto.ShippingSubTotalWithTax, currency);
            result.ShippingTotal = new Money(cartDto.ShippingTotal, currency);
            result.ShippingTotalWithTax = new Money(cartDto.ShippingTotalWithTax, currency);
            result.PaymentPrice = new Money(cartDto.PaymentSubTotal, currency);
            result.PaymentPriceWithTax = new Money(cartDto.PaymentSubTotalWithTax, currency);
            result.PaymentTotal = new Money(cartDto.PaymentTotal, currency);
            result.PaymentTotalWithTax = new Money(cartDto.PaymentTotalWithTax, currency);

            result.DiscountTotal = new Money(cartDto.DiscountTotal, currency);
            result.DiscountTotalWithTax = new Money(cartDto.DiscountTotalWithTax, currency);
            result.TaxTotal = new Money(cartDto.TaxTotal, currency);

            result.IsAnonymous = cartDto.IsAnonymous;
            result.IsRecuring = cartDto.IsRecuring == true;
            result.VolumetricWeight = cartDto.VolumetricWeight ?? 0;
            result.Weight = cartDto.Weight ?? 0;

            return result;
        }

        public static Payment ToPayment(this cartDto.Payment payment, ShoppingCart cart)
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
                result.TaxDetails = payment.TaxDetails.Select(td => ToTaxDetail(td, cart.Currency)).ToList();
            }

            if (!payment.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(payment.Discounts.Select(x => ToDiscount(x, new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }

        public static Shipment ToShipment(this cartDto.Shipment shipment, ShoppingCart cart)
        {
            var retVal = new Shipment(cart.Currency)
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
                retVal.DeliveryAddress = ToAddress(shipment.DeliveryAddress);
            }

            if (shipment.Items != null)
            {
                retVal.Items = shipment.Items.Select(i => ToShipmentItem(i, cart)).ToList();
            }

            if (shipment.TaxDetails != null)
            {
                retVal.TaxDetails = shipment.TaxDetails.Select(td => ToTaxDetail(td, cart.Currency)).ToList();
            }

            if (!shipment.Discounts.IsNullOrEmpty())
            {
                retVal.Discounts.AddRange(shipment.Discounts.Select(x => ToDiscount(x, new[] { cart.Currency }, cart.Language)));
            }

            return retVal;
        }

        public static TaxDetail ToTaxDetail(this coreDto.TaxDetail taxDetail, Currency currency)
        {
            var result = new TaxDetail(currency)
            {
                Name = taxDetail.Name,
                Rate = new Money(taxDetail.Rate, currency),
                Amount = new Money(taxDetail.Amount, currency),
            };
            return result;
        }

        public static Address ToAddress(this coreDto.Address address)
        {
            var retVal = new Address
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
            return retVal;
        }

        public static Discount ToDiscount(this coreDto.Discount discount, IEnumerable<Currency> availCurrencies, Language language)
        {
            var currency = availCurrencies.FirstOrDefault(x => x.Equals(discount.Currency)) ?? new Currency(language, discount.Currency);

            var result = new Discount(currency)
            {
                Coupon = discount.Coupon,
                Description = discount.Description,
                PromotionId = discount.PromotionId,
                Amount = new Money(discount.DiscountAmount, currency),
            };

            return result;
        }

        public static CartShipmentItem ToShipmentItem(this cartDto.ShipmentItem shipmentItem, ShoppingCart cart)
        {
            var result = new CartShipmentItem
            {
                Id = shipmentItem.Id,
                Quantity = shipmentItem.Quantity,
                LineItem = cart.Items.FirstOrDefault(x => x.Id == shipmentItem.LineItemId),
            };

            return result;
        }
    }
}
