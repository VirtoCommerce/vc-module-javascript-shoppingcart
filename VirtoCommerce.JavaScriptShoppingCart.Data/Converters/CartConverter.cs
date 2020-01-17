using Omu.ValueInjecter;
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
using marketingDto = VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    // TechDebt: Need to use Automapper here where possible. Current problem - pass constrcutor parameters to the child object.
    // Link to mitigate the problem using resolution context: http://codebuckets.com/2016/09/24/passing-parameters-with-automapper/
    public static partial class CartConverter
    {

        public static cartDto.ShoppingCart ToShopingCartDto(this ShoppingCart cart)
        {


            var result = new cartDto.ShoppingCart
            {
                ChannelId = cart.ChannelId,
                Comment = cart.Comment,
                CustomerId = cart.CustomerId,
                CustomerName = cart.CustomerName,
                Id = cart.Id,
                Name = cart.Name,
                // ObjectType = cart.ObjectType,
                OrganizationId = cart.OrganizationId,
                Status = cart.Status,
                StoreId = cart.StoreId,
                Type = cart.Type,
                IsAnonymous = cart.IsAnonymous
            };

            if (cart.Language != null)
            {
                result.LanguageCode = cart.Language.CultureName;
            }
            // result.Addresses = cart.Addresses.Select(ToCartAddressDto).ToList();
            result.Coupons = cart.Coupons?.Select(c => c.Code).ToList();
            result.Currency = cart.Currency.Code;
            result.Discounts = cart.Discounts.Select(ToCartDiscountDto).ToList();
            result.HandlingTotal = cart.HandlingTotal.InternalAmount;
            result.HandlingTotalWithTax = cart.HandlingTotal.InternalAmount;
            result.DiscountAmount = cart.DiscountAmount.InternalAmount;
            result.Items = cart.Items.Select(ToLineItemDto).ToList();
            result.Payments = cart.Payments.Select(ToPaymentDto).ToList();
            result.Shipments = cart.Shipments.Select(ToShipmentDto).ToList();
            result.TaxDetails = cart.TaxDetails.Select(ToCartTaxDetailDto).ToList();
            // result.DynamicProperties = cart.DynamicProperties.Select(ToCartDynamicPropertyDto).ToList();
            result.VolumetricWeight = cart.VolumetricWeight;
            result.Weight = cart.Weight;

            return result;
        }

        public static coreDto.Discount ToCartDiscountDto(this Discount discount)
        {
            var result = new coreDto.Discount
            {
                PromotionId = discount.PromotionId,
                Coupon = discount.Coupon,
                Description = discount.Description,
                Currency = discount.Amount.Currency.Code,
                DiscountAmount = discount.Amount.Amount
            };
            return result;
        }


        public static cartDto.LineItem ToLineItemDto(this LineItem lineItem)
        {
            var retVal = new cartDto.LineItem
            {
                Id = lineItem.Id,
                IsReadOnly = lineItem.IsReadOnly,
                CatalogId = lineItem.CatalogId,
                CategoryId = lineItem.CategoryId,
                ImageUrl = lineItem.ImageUrl,
                Name = lineItem.Name,
                ObjectType = lineItem.ObjectType,
                ProductId = lineItem.ProductId,
                ProductType = lineItem.ProductType,
                Quantity = lineItem.Quantity,
                ShipmentMethodCode = lineItem.ShipmentMethodCode,
                Sku = lineItem.Sku,
                TaxType = lineItem.TaxType,
                ThumbnailImageUrl = lineItem.ThumbnailImageUrl,
                WeightUnit = lineItem.WeightUnit,
                MeasureUnit = lineItem.MeasureUnit,
                Weight = lineItem.Weight,
                Width = lineItem.Width,
                Length = lineItem.Length,
                Height = lineItem.Height,

                Currency = lineItem.Currency.Code,
                Discounts = lineItem.Discounts.Select(ToCartDiscountDto).ToList(),

                ListPrice = lineItem.ListPrice.InternalAmount,
                SalePrice = lineItem.SalePrice.InternalAmount,
                TaxPercentRate = lineItem.TaxPercentRate,
                DiscountAmount = lineItem.DiscountAmount.InternalAmount,
                TaxDetails = lineItem.TaxDetails.Select(ToCartTaxDetailDto).ToList(),
                // DynamicProperties = lineItem.DynamicProperties.Select(ToCartDynamicPropertyDto).ToList(),
                VolumetricWeight = lineItem.VolumetricWeight ?? 0
            };

            return retVal;
        }

        public static coreDto.TaxDetail ToCartTaxDetailDto(this TaxDetail taxDetail)
        {
            var result = new coreDto.TaxDetail
            {
                Name = taxDetail.Name,
                Rate = taxDetail.Rate.Amount,
                Amount = taxDetail.Amount.Amount,
            };
            return result;
        }


        public static cartDto.Payment ToPaymentDto(this Payment payment)
        {
            var result = new cartDto.Payment
            {
                Id = payment.Id,
                OuterId = payment.OuterId,
                PaymentGatewayCode = payment.PaymentGatewayCode,
                TaxType = payment.TaxType,

                Amount = payment.Amount.InternalAmount,

                Currency = payment.Currency.Code,
                Price = payment.Price.InternalAmount,
                DiscountAmount = payment.DiscountAmount.InternalAmount,
                TaxPercentRate = payment.TaxPercentRate
            };

            if (payment.BillingAddress != null)
            {
                result.BillingAddress = ToAddressDto(payment.BillingAddress);
            }
            if (payment.Discounts != null)
            {
                result.Discounts = payment.Discounts.Select(ToCartDiscountDto).ToList();
            }
            if (payment.TaxDetails != null)
            {
                result.TaxDetails = payment.TaxDetails.Select(ToCartTaxDetailDto).ToList();
            }

            return result;
        }

        public static cartDto.Shipment ToShipmentDto(this Shipment shipment)
        {
            var retVal = new cartDto.Shipment
            {
                Id = shipment.Id,
                MeasureUnit = shipment.MeasureUnit,
                ShipmentMethodCode = shipment.ShipmentMethodCode,
                ShipmentMethodOption = shipment.ShipmentMethodOption,
                WeightUnit = shipment.WeightUnit,
                Height = shipment.Height,
                Weight = shipment.Weight,
                Width = shipment.Width,
                Length = shipment.Length,

                Currency = shipment.Currency != null ? shipment.Currency.Code : null,
                DiscountAmount = shipment.DiscountAmount != null ? shipment.DiscountAmount.InternalAmount : 0,
                Price = shipment.Price != null ? shipment.Price.InternalAmount : 0,
                TaxPercentRate = shipment.TaxPercentRate
            };

            if (shipment.DeliveryAddress != null)
            {
                retVal.DeliveryAddress = ToAddressDto(shipment.DeliveryAddress);
            }

            if (shipment.Discounts != null)
            {
                retVal.Discounts = shipment.Discounts.Select(ToCartDiscountDto).ToList();
            }

            if (shipment.Items != null)
            {
                retVal.Items = shipment.Items.Select(ToShipmentItemDto).ToList();
            }

            if (shipment.TaxDetails != null)
            {
                retVal.TaxDetails = shipment.TaxDetails.Select(ToCartTaxDetailDto).ToList();
            }

            return retVal;
        }


        public static cartDto.ShipmentItem ToShipmentItemDto(this CartShipmentItem shipmentItem)
        {
            var result = new cartDto.ShipmentItem
            {
                Id = shipmentItem.Id,
                Quantity = shipmentItem.Quantity,
                LineItemId = shipmentItem.LineItem.Id,
                LineItem = shipmentItem.LineItem.ToLineItemDto()
            };

            return result;
        }

        public static coreDto.Address ToAddressDto(this Address address)
        {
            return address.ToCoreAddressDto();
        }

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
                Height = shipmentDto.Height,
                Weight = shipmentDto.Weight,
                Width = shipmentDto.Width,
                Length = shipmentDto.Length,
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


        public static marketingDto.ProductPromoEntry ToProductPromoEntryDto(this LineItem lineItem)
        {
            var result = new marketingDto.ProductPromoEntry
            {
                CatalogId = lineItem.CatalogId,
                CategoryId = lineItem.CategoryId,
                Code = lineItem.Sku,
                ProductId = lineItem.ProductId,
                Discount = lineItem.DiscountTotal.Amount,
                //Use only base price for discount evaluation
                Price = lineItem.SalePrice.Amount,
                Quantity = lineItem.Quantity,
                InStockQuantity = lineItem.InStockQuantity,
                // Outline = lineItem.Product.Outline,
                Variations = null // TODO
            };

            return result;
        }


        public static marketingDto.PromotionEvaluationContext ToPromotionEvaluationContext(this ShoppingCart cart)
        {
            //var result = new PromotionEvaluationContext()
            //{
            //    Cart = cart,
            //    User = cart.Customer,
            //    Currency = cart.Currency,
            //    Language = cart.Language,
            //    StoreId = cart.StoreId
            //};

            var result = new marketingDto.PromotionEvaluationContext();

            result.StoreId = cart.StoreId; //new

            result.CartPromoEntries = cart.Items.Select(x => x.ToProductPromoEntryDto()).ToList();

            result.CartTotal = cart.SubTotal.Amount;
            result.Coupons = cart.Coupons?.Select(c => c.Code).ToList();
            result.Currency = cart.Currency.Code;
            result.CustomerId = cart.CustomerId;
            //result.UserGroups = cart.Customer?.Contact?.UserGroups;
            //result.IsRegisteredUser = cart.Customer?.IsRegisteredUser;
            result.Language = cart.Language.CultureName;
            //Set cart line items as default promo items
            result.PromoEntries = result.CartPromoEntries;


            if (!cart.Shipments.IsNullOrEmpty())
            {
                var shipment = cart.Shipments.First();
                result.ShipmentMethodCode = shipment.ShipmentMethodCode;
                result.ShipmentMethodOption = shipment.ShipmentMethodOption;
                result.ShipmentMethodPrice = shipment.Price.Amount;
            }
            if (!cart.Payments.IsNullOrEmpty())
            {
                var payment = cart.Payments.First();
                result.PaymentMethodCode = payment.PaymentGatewayCode;
                result.PaymentMethodPrice = payment.Price.Amount;
            }

            return result;
        }

        public static PromotionReward ToPromotionReward(this marketingDto.PromotionReward rewardDto, Currency currency)
        {
            var result = new PromotionReward();
            result.InjectFrom(rewardDto);
            result.RewardType = EnumUtility.SafeParse(rewardDto.GetType().Name, PromotionRewardType.CatalogItemAmountReward);
            return result;
            //{
            //    // CategoryId = rewardDto.CategoryId,
            //    Coupon = rewardDto.Coupon,
            //    Description = rewardDto.Description,
            //    IsValid = rewardDto.IsValid,
            //    // LineItemId = rewardDto.LineItemId,
            //    // MeasureUnit = rewardDto.MeasureUnit,
            //    // ProductId = rewardDto.ProductId,
            //    // PromotionId = rewardDto.PromotionId,
            //    // Quantity = rewardDto.Quantity ?? 0,
            //    MaxLimit = rewardDto.MaxLimit,
            //    Amount = (decimal)(rewardDto.Amount ?? 0),
            //    AmountType = EnumUtility.SafeParse(rewardDto.AmountType, AmountType.Absolute),
            //    CouponAmount = new Money(rewardDto.CouponAmount ?? 0, currency),
            //    CouponMinOrderAmount = new Money(rewardDto.CouponMinOrderAmount ?? 0, currency),
            //    Promotion = rewardDto.Promotion.ToPromotion(),
            //    RewardType = EnumUtility.SafeParse(rewardDto.RewardType, PromotionRewardType.CatalogItemAmountReward),
            //    ShippingMethodCode = rewardDto.ShippingMethod,
            //    ConditionalProductId = rewardDto.ConditionalProductId,
            //    ForNthQuantity = rewardDto.ForNthQuantity,
            //    InEveryNthQuantity = rewardDto.InEveryNthQuantity,
            //};

        }


        public static PaymentMethod ToCartPaymentMethod(this Domain.Payment.Model.PaymentMethod paymentMethodDto, ShoppingCart cart)
        {
            var retVal = new PaymentMethod(cart.Currency)
            {
                Code = paymentMethodDto.Code,
                Description = paymentMethodDto.Description,
                LogoUrl = paymentMethodDto.LogoUrl,
                Name = paymentMethodDto.Name,
                PaymentMethodGroupType = paymentMethodDto.PaymentMethodGroupType.ToString(),
                PaymentMethodType = paymentMethodDto.PaymentMethodType.ToString(),
                TaxType = paymentMethodDto.TaxType,

                Priority = paymentMethodDto.Priority
            };

            //if (paymentMethodDto.Settings != null)
            //{
            //    retVal.Settings = paymentMethodDto.Settings.Where(x => !x.ValueType.EqualsInvariant("SecureString")).Select(x => x.JsonConvert<platformDto.Setting>().ToSettingEntry()).ToList();
            //}

            retVal.Currency = cart.Currency;
            retVal.Price = new Money(paymentMethodDto.Price, cart.Currency);
            retVal.DiscountAmount = new Money(paymentMethodDto.DiscountAmount, cart.Currency);
            retVal.TaxPercentRate = paymentMethodDto.TaxPercentRate;

            if (paymentMethodDto.TaxDetails != null)
            {
                retVal.TaxDetails = paymentMethodDto.TaxDetails.Select(td => ToTaxDetail(td, cart.Currency)).ToList();
            }

            return retVal;
        }

    }
}
