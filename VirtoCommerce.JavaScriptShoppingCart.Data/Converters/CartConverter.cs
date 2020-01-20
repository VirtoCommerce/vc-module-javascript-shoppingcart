using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using VirtoCommerce.Platform.Core.Common;
using cartDto = VirtoCommerce.Domain.Cart.Model;
using coreDto = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    // TechDebt: Need to use Automapper here where possible. Current problem - pass constrcutor parameters to the child object.
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
                HasPhysicalProducts = true
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

        public static Payment ToPayment(this cartDto.Payment paymentDto, ShoppingCart cart)
        {
            var result = new Payment(cart.Currency)
            {
                Id = paymentDto.Id,
                OuterId = paymentDto.OuterId,
                PaymentGatewayCode = paymentDto.PaymentGatewayCode,
                TaxType = paymentDto.TaxType,

                Amount = new Money(paymentDto.Amount, cart.Currency)
            };

            if (paymentDto.BillingAddress != null)
            {
                result.BillingAddress = ToAddress(paymentDto.BillingAddress);
            }

            result.Price = new Money(paymentDto.Price, cart.Currency);
            result.DiscountAmount = new Money(paymentDto.DiscountAmount, cart.Currency);
            result.PriceWithTax = new Money(paymentDto.PriceWithTax, cart.Currency);
            result.DiscountAmountWithTax = new Money(paymentDto.DiscountAmountWithTax, cart.Currency);
            result.Total = new Money(paymentDto.Total, cart.Currency);
            result.TotalWithTax = new Money(paymentDto.TotalWithTax, cart.Currency);
            result.TaxTotal = new Money(paymentDto.TaxTotal, cart.Currency);
            result.TaxPercentRate = (decimal?)paymentDto.TaxPercentRate ?? 0m;

            if (paymentDto.TaxDetails != null)
            {
                result.TaxDetails = paymentDto.TaxDetails.Select(td => ToTaxDetail(td, cart.Currency)).ToList();
            }

            if (!paymentDto.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(paymentDto.Discounts.Select(x => ToDiscount(x, new[] { cart.Currency }, cart.Language)));
            }

            return result;
        }

        public static Shipment ToShipment(this cartDto.Shipment shipmentDto, ShoppingCart cart)
        {
            var retVal = new Shipment(cart.Currency)
            {
                Id = shipmentDto.Id,
                MeasureUnit = shipmentDto.MeasureUnit,
                ShipmentMethodCode = shipmentDto.ShipmentMethodCode,
                ShipmentMethodOption = shipmentDto.ShipmentMethodOption,
                WeightUnit = shipmentDto.WeightUnit,
                Height = (double?)shipmentDto.Height,
                Weight = (double?)shipmentDto.Weight,
                Width = (double?)shipmentDto.Width,
                Length = (double?)shipmentDto.Length,
                Currency = cart.Currency,
                Price = new Money(shipmentDto.Price, cart.Currency),
                PriceWithTax = new Money(shipmentDto.PriceWithTax, cart.Currency),
                DiscountAmount = new Money(shipmentDto.DiscountAmount, cart.Currency),
                Total = new Money(shipmentDto.Total, cart.Currency),
                TotalWithTax = new Money(shipmentDto.TotalWithTax, cart.Currency),
                DiscountAmountWithTax = new Money(shipmentDto.DiscountAmountWithTax, cart.Currency),
                TaxTotal = new Money(shipmentDto.TaxTotal, cart.Currency),
                TaxPercentRate = (decimal?)shipmentDto.TaxPercentRate ?? 0m
            };

            if (shipmentDto.DeliveryAddress != null)
            {
                retVal.DeliveryAddress = ToAddress(shipmentDto.DeliveryAddress);
            }

            if (shipmentDto.Items != null)
            {
                retVal.Items = shipmentDto.Items.Select(i => ToShipmentItem(i, cart)).ToList();
            }

            if (shipmentDto.TaxDetails != null)
            {
                retVal.TaxDetails = shipmentDto.TaxDetails.Select(td => ToTaxDetail(td, cart.Currency)).ToList();
            }

            if (!shipmentDto.Discounts.IsNullOrEmpty())
            {
                retVal.Discounts.AddRange(shipmentDto.Discounts.Select(x => ToDiscount(x, new[] { cart.Currency }, cart.Language)));
            }

            return retVal;
        }

        public static TaxDetail ToTaxDetail(this coreDto.TaxDetail taxDeatilDto, Currency currency)
        {
            var result = new TaxDetail(currency)
            {
                Name = taxDeatilDto.Name,
                Rate = new Money(taxDeatilDto.Rate, currency),
                Amount = new Money(taxDeatilDto.Amount, currency),
            };
            return result;
        }

        public static Address ToAddress(this coreDto.Address addressDto)
        {
            var retVal = new Address
            {
                Key = addressDto.Key,
                City = addressDto.City,
                CountryCode = addressDto.CountryCode,
                CountryName = addressDto.CountryName,
                Email = addressDto.Email,
                FirstName = addressDto.FirstName,
                LastName = addressDto.LastName,
                Line1 = addressDto.Line1,
                Line2 = addressDto.Line2,
                MiddleName = addressDto.MiddleName,
                Name = addressDto.Name,
                Organization = addressDto.Organization,
                Phone = addressDto.Phone,
                PostalCode = addressDto.PostalCode,
                RegionId = addressDto.RegionId,
                RegionName = addressDto.RegionName,
                Zip = addressDto.Zip,
                Type = (AddressType)Enum.Parse(typeof(AddressType), addressDto.AddressType.ToString(), true)
            };
            return retVal;
        }

        public static Discount ToDiscount(this coreDto.Discount discountDto, IEnumerable<Currency> availCurrencies, Language language)
        {
            var currency = availCurrencies.FirstOrDefault(x => x.Equals(discountDto.Currency)) ?? new Currency(language, discountDto.Currency);

            var result = new Discount(currency)
            {
                Coupon = discountDto.Coupon,
                Description = discountDto.Description,
                PromotionId = discountDto.PromotionId,
                Amount = new Money(discountDto.DiscountAmount, currency)
            };

            return result;
        }

        public static CartShipmentItem ToShipmentItem(this cartDto.ShipmentItem shipmentItemDto, ShoppingCart cart)
        {
            var result = new CartShipmentItem
            {
                Id = shipmentItemDto.Id,
                Quantity = shipmentItemDto.Quantity,
                LineItem = cart.Items.FirstOrDefault(x => x.Id == shipmentItemDto.LineItemId)
            };

            return result;
        }
    }
}
