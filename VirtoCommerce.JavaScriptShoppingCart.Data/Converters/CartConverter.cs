using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Cart;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Model.Marketing;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Tax;
using VirtoCommerce.Platform.Core.Common;
using domain_cart_model = VirtoCommerce.Domain.Cart.Model;
using domain_core_model = VirtoCommerce.Domain.Commerce.Model;
using domain_shipping_model = VirtoCommerce.Domain.Shipping.Model;
using domain_store_model = VirtoCommerce.Domain.Store.Model;
using domain_tax_model = VirtoCommerce.Domain.Tax.Model;
using marketing_domain_model = VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    // TechDebt: Need to use Automapper here where possible. Current problem - pass constrcutor parameters to the child object.
    // Link to mitigate the problem using resolution context: http://codebuckets.com/2016/09/24/passing-parameters-with-automapper/
    public static class CartConverter
    {
        public static domain_cart_model.ShoppingCart ToShopingCartDto(this ShoppingCart cart)
        {
            var result = new domain_cart_model.ShoppingCart
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
                IsAnonymous = cart.IsAnonymous,
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

        public static domain_core_model.Discount ToCartDiscountDto(this Discount discount)
        {
            var result = new domain_core_model.Discount
            {
                PromotionId = discount.PromotionId,
                Coupon = discount.Coupon,
                Description = discount.Description,
                Currency = discount.Amount.Currency.Code,
                DiscountAmount = discount.Amount.Amount,
            };
            return result;
        }


        public static domain_cart_model.LineItem ToLineItemDto(this LineItem lineItem)
        {
            var retVal = new domain_cart_model.LineItem
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
                VolumetricWeight = lineItem.VolumetricWeight ?? 0,
            };

            return retVal;
        }

        public static domain_core_model.TaxDetail ToCartTaxDetailDto(this TaxDetail taxDetail)
        {
            var result = new domain_core_model.TaxDetail
            {
                Name = taxDetail.Name,
                Rate = taxDetail.Rate.Amount,
                Amount = taxDetail.Amount.Amount,
            };
            return result;
        }


        public static domain_cart_model.Payment ToPaymentDto(this Payment payment)
        {
            var result = new domain_cart_model.Payment
            {
                Id = payment.Id,
                OuterId = payment.OuterId,
                PaymentGatewayCode = payment.PaymentGatewayCode,
                TaxType = payment.TaxType,

                Amount = payment.Amount.InternalAmount,

                Currency = payment.Currency.Code,
                Price = payment.Price.InternalAmount,
                DiscountAmount = payment.DiscountAmount.InternalAmount,
                TaxPercentRate = payment.TaxPercentRate,
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

        public static domain_cart_model.Shipment ToShipmentDto(this Shipment shipment)
        {
            var retVal = new domain_cart_model.Shipment
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
                TaxPercentRate = shipment.TaxPercentRate,
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


        public static domain_cart_model.ShipmentItem ToShipmentItemDto(this CartShipmentItem shipmentItem)
        {
            var result = new domain_cart_model.ShipmentItem
            {
                Id = shipmentItem.Id,
                Quantity = shipmentItem.Quantity,
                LineItemId = shipmentItem.LineItem.Id,
                LineItem = shipmentItem.LineItem.ToLineItemDto(),
            };

            return result;
        }

        public static domain_core_model.Address ToAddressDto(this Address address)
        {
            return address.ToCoreAddressDto();
        }

        public static ShoppingCart ToShoppingCart(this domain_cart_model.ShoppingCart cartDto, Currency currency, Language language)
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

            if (cartDto.Items != null)
            {
                result.Items = cartDto.Items.Select(item => item.ToLineItem(currency, language)).ToList();
                result.HasPhysicalProducts = result.Items.Any();
            }

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

        public static LineItem ToLineItem(this domain_cart_model.LineItem lineItemDto, Currency currency, Language language)
        {
            var result = new LineItem(currency, language)
            {
                Id = lineItemDto.Id,
                IsReadOnly = lineItemDto.IsReadOnly,
                CatalogId = lineItemDto.CatalogId,
                CategoryId = lineItemDto.CategoryId,
                ImageUrl = lineItemDto.ImageUrl,
                Name = lineItemDto.Name,
                ObjectType = lineItemDto.ObjectType,
                ProductId = lineItemDto.ProductId,
                ProductType = lineItemDto.ProductType,
                Quantity = lineItemDto.Quantity,
                ShipmentMethodCode = lineItemDto.ShipmentMethodCode,
                Sku = lineItemDto.Sku,
                TaxType = lineItemDto.TaxType,
                ThumbnailImageUrl = lineItemDto.ThumbnailImageUrl,
                WeightUnit = lineItemDto.WeightUnit,
                MeasureUnit = lineItemDto.MeasureUnit,
                Weight = lineItemDto.Weight,
                Width = lineItemDto.Width,
                Length = lineItemDto.Length,
                Height = lineItemDto.Height,
            };



            result.ImageUrl = lineItemDto.ImageUrl;

            if (lineItemDto.TaxDetails != null)
            {
                result.TaxDetails = lineItemDto.TaxDetails.Select(td => ToTaxDetail(td, currency)).ToList();
            }

            // if (lineItemDto.DynamicProperties != null)
            // {
            //    result.DynamicProperties = new MutablePagedList<DynamicProperty>(lineItemDto.DynamicProperties.Select(ToDynamicProperty).ToList());
            // }
            if (!lineItemDto.Discounts.IsNullOrEmpty())
            {
                result.Discounts.AddRange(lineItemDto.Discounts.Select(x => ToDiscount(x, new[] { currency }, language)));
            }

            result.Comment = lineItemDto.Note;
            result.IsGift = lineItemDto.IsGift;
            result.IsReccuring = lineItemDto.IsReccuring;
            result.ListPrice = new Money(lineItemDto.ListPrice, currency);
            result.RequiredShipping = lineItemDto.RequiredShipping;
            result.SalePrice = new Money(lineItemDto.SalePrice, currency);
            result.TaxPercentRate = lineItemDto.TaxPercentRate;
            result.DiscountAmount = new Money(lineItemDto.DiscountAmount, currency);
            result.TaxIncluded = lineItemDto.TaxIncluded;
            result.Weight = lineItemDto.Weight;
            result.Width = lineItemDto.Width;
            result.Height = lineItemDto.Height;
            result.Length = lineItemDto.Length;


            result.DiscountAmountWithTax = new Money(lineItemDto.DiscountAmountWithTax, currency);
            result.DiscountTotal = new Money(lineItemDto.DiscountTotal, currency);
            result.DiscountTotalWithTax = new Money(lineItemDto.DiscountTotalWithTax, currency);
            result.ListPriceWithTax = new Money(lineItemDto.ListPriceWithTax, currency);
            result.SalePriceWithTax = new Money(lineItemDto.SalePriceWithTax, currency);
            result.PlacedPrice = new Money(lineItemDto.PlacedPrice, currency);
            result.PlacedPriceWithTax = new Money(lineItemDto.PlacedPriceWithTax, currency);
            result.ExtendedPrice = new Money(lineItemDto.ExtendedPrice, currency);
            result.ExtendedPriceWithTax = new Money(lineItemDto.ExtendedPriceWithTax, currency);
            result.TaxTotal = new Money(lineItemDto.TaxTotal, currency);

            return result;
        }

        public static Payment ToPayment(this domain_cart_model.Payment paymentDto, ShoppingCart cart)
        {
            var result = new Payment(cart.Currency)
            {
                Id = paymentDto.Id,
                OuterId = paymentDto.OuterId,
                PaymentGatewayCode = paymentDto.PaymentGatewayCode,
                TaxType = paymentDto.TaxType,
                Amount = new Money(paymentDto.Amount, cart.Currency),
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

        public static Shipment ToShipment(this domain_cart_model.Shipment shipmentDto, ShoppingCart cart)
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
                TaxPercentRate = shipmentDto.TaxPercentRate,
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

        public static TaxDetail ToTaxDetail(this domain_core_model.TaxDetail taxDeatilDto, Currency currency)
        {
            var result = new TaxDetail(currency)
            {
                Name = taxDeatilDto.Name,
                Rate = new Money(taxDeatilDto.Rate, currency),
                Amount = new Money(taxDeatilDto.Amount, currency),
            };
            return result;
        }

        public static Address ToAddress(this domain_core_model.Address addressDto)
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
                Type = (AddressType)Enum.Parse(typeof(AddressType), addressDto.AddressType.ToString(), true),
            };
            return retVal;
        }

        public static Discount ToDiscount(this domain_core_model.Discount discountDto, IEnumerable<Currency> availCurrencies, Language language)
        {
            var currency = availCurrencies.FirstOrDefault(x => x.Equals(discountDto.Currency)) ?? new Currency(language, discountDto.Currency);

            var result = new Discount(currency)
            {
                Coupon = discountDto.Coupon,
                Description = discountDto.Description,
                PromotionId = discountDto.PromotionId,
                Amount = new Money(discountDto.DiscountAmount, currency),
            };

            return result;
        }

        public static CartShipmentItem ToShipmentItem(this domain_cart_model.ShipmentItem shipmentItemDto, ShoppingCart cart)
        {
            var result = new CartShipmentItem
            {
                Id = shipmentItemDto.Id,
                Quantity = shipmentItemDto.Quantity,
                LineItem = cart.Items.FirstOrDefault(x => x.Id == shipmentItemDto.LineItemId),
            };

            return result;
        }


        public static marketing_domain_model.ProductPromoEntry ToProductPromoEntryDto(this LineItem lineItem)
        {
            var result = new marketing_domain_model.ProductPromoEntry
            {
                CatalogId = lineItem.CatalogId,
                CategoryId = lineItem.CategoryId,
                Code = lineItem.Sku,
                ProductId = lineItem.ProductId,
                Discount = lineItem.DiscountTotal.Amount,

                // Use only base price for discount evaluation
                Price = lineItem.SalePrice.Amount,
                Quantity = lineItem.Quantity,
                InStockQuantity = lineItem.InStockQuantity,

                // Outline = lineItem.Product.Outline,
                Variations = null, // TODO
            };

            return result;
        }


        public static marketing_domain_model.PromotionEvaluationContext ToPromotionEvaluationContext(this ShoppingCart cart)
        {
            var result = new marketing_domain_model.PromotionEvaluationContext();

            result.StoreId = cart.StoreId; // new

            result.CartPromoEntries = cart.Items.Select(x => x.ToProductPromoEntryDto()).ToList();

            result.CartTotal = cart.SubTotal.Amount;
            result.Coupons = cart.Coupons?.Select(c => c.Code).ToList();
            result.Currency = cart.Currency.Code;
            result.CustomerId = cart.CustomerId;

            // result.UserGroups = cart.Customer?.Contact?.UserGroups;
            // result.IsRegisteredUser = cart.Customer?.IsRegisteredUser;
            result.Language = cart.Language.CultureName;

            // Set cart line items as default promo items
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

        public static Promotion ToPromotion(this marketing_domain_model.Promotion promotionDto)
        {
            var result = new Promotion
            {
                Id = promotionDto.Id,
                Name = promotionDto.Name,
                Description = promotionDto.Description,
            };

            return result;
        }

        public static PromotionReward ToPromotionReward(this marketing_domain_model.PromotionReward rewardDto, Currency currency)
        {
            var result = new PromotionReward();
            result.InjectFrom(rewardDto);
            result.RewardType = EnumUtility.SafeParse(rewardDto.GetType().Name, PromotionRewardType.CatalogItemAmountReward);

            // result.AmountType = EnumUtility.SafeParse(rewardDto.AmountType, AmountType.Absolute),
            result.CouponAmount = new Money(rewardDto.CouponAmount, currency);
            result.CouponMinOrderAmount = new Money(rewardDto.CouponMinOrderAmount ?? 0, currency);

            result.PromotionId = rewardDto.Promotion?.Id;
            result.Promotion = rewardDto.Promotion?.ToPromotion();

            return result;
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
                Priority = paymentMethodDto.Priority,
            };

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

        public static ShippingMethod ToShippingMethod(this domain_shipping_model.ShippingRate shippingRate, Currency currency, IEnumerable<Currency> availCurrencies)
        {
            var rateCurrency = availCurrencies.FirstOrDefault(x => x.Equals(shippingRate.Currency)) ?? new Currency(new Language(currency.CultureName), shippingRate.Currency);
            var ratePrice = new Money(shippingRate.Rate, rateCurrency);
            var rateDiscount = new Money(shippingRate.DiscountAmount, rateCurrency);

            if (rateCurrency != currency)
            {
                ratePrice = ratePrice.ConvertTo(currency);
                rateDiscount = rateDiscount.ConvertTo(currency);
            }

            var result = new ShippingMethod(currency);
            result.OptionDescription = shippingRate.OptionDescription;
            result.OptionName = shippingRate.OptionName;

            result.Price = ratePrice;
            result.DiscountAmount = rateDiscount;

            if (shippingRate.ShippingMethod != null)
            {
                result.LogoUrl = shippingRate.ShippingMethod.LogoUrl;
                result.Name = shippingRate.ShippingMethod.Name;
                result.Priority = shippingRate.ShippingMethod.Priority;
                result.TaxType = shippingRate.ShippingMethod.TaxType;
                result.ShipmentMethodCode = shippingRate.ShippingMethod.Code;
            }

            return result;
        }


        public static domain_tax_model.TaxEvaluationContext ToTaxEvaluationContextDto(this ShoppingCart cart, domain_store_model.Store store)
        {
            var result = new domain_tax_model.TaxEvaluationContext()
            {
                Id = cart.Id,
                Code = cart.Name,
                Currency = cart.Currency.Code,
                Type = "Cart",
                Store = store,

                // Customer = cart.Customer?.Contact?.ToCoreContactDto(), //storefront logic
                // StoreTaxCalculationEnabled = store.TaxCalculationEnabled,
                // FixedTaxRate = store.FixedTaxRate
            };

            foreach (var lineItem in cart.Items)
            {
                result.Lines.Add(new domain_tax_model.TaxLine()
                {
                    Id = lineItem.Id,
                    Code = lineItem.Sku,
                    Name = lineItem.Name,
                    TaxType = lineItem.TaxType,

                    // Special case when product have 100% discount and need to calculate tax for old value
                    Amount = lineItem.ExtendedPrice.Amount > 0 ? lineItem.ExtendedPrice.Amount : lineItem.SalePrice.Amount,
                    Quantity = lineItem.Quantity,
                    Price = lineItem.PlacedPrice.Amount,
                    TypeName = "item",
                });
            }

            foreach (var shipment in cart.Shipments)
            {
                var totalTaxLine = new domain_tax_model.TaxLine()
                {
                    Id = shipment.Id,
                    Code = shipment.ShipmentMethodCode,
                    Name = shipment.ShipmentMethodOption,
                    TaxType = shipment.TaxType,

                    // Special case when shipment have 100% discount and need to calculate tax for old value
                    Amount = shipment.Total.Amount > 0 ? shipment.Total.Amount : shipment.Price.Amount,
                    TypeName = "shipment",
                };
                result.Lines.Add(totalTaxLine);

                if (shipment.DeliveryAddress != null)
                {
                    result.Address = shipment.DeliveryAddress.ToAddressDto();
                }
            }

            foreach (var payment in cart.Payments)
            {
                var totalTaxLine = new domain_tax_model.TaxLine()
                {
                    Id = payment.Id,
                    Code = payment.PaymentGatewayCode,
                    Name = payment.PaymentGatewayCode,
                    TaxType = payment.TaxType,

                    // Special case when shipment have 100% discount and need to calculate tax for old value
                    Amount = payment.Total.Amount > 0 ? payment.Total.Amount : payment.Price.Amount,
                    TypeName = "payment",
                };
                result.Lines.Add(totalTaxLine);
            }

            return result;
        }


        public static domain_tax_model.TaxLine[] ToTaxLines(this ShippingMethod shipmentMethod)
        {
            var retVal = new List<domain_tax_model.TaxLine>
            {
                new domain_tax_model.TaxLine()
                {
                    Id = shipmentMethod.BuildTaxLineId(),
                    Code = shipmentMethod.ShipmentMethodCode,
                    TaxType = shipmentMethod.TaxType,

                    // Special case when shipment method have 100% discount and need to calculate tax for old value
                    Amount = shipmentMethod.Total.Amount > 0 ? shipmentMethod.Total.Amount : shipmentMethod.Price.Amount,
                },
            };
            return retVal.ToArray();
        }

        public static domain_tax_model.TaxLine[] ToTaxLines(this PaymentMethod paymentMethod)
        {
            var retVal = new List<domain_tax_model.TaxLine>
            {
                new domain_tax_model.TaxLine()
                {
                    Id = paymentMethod.Code,
                    Code = paymentMethod.Code,
                    TaxType = paymentMethod.TaxType,

                     // Special case when payment method have 100% discount and need to calculate tax for old value
                    Amount = paymentMethod.Total.Amount > 0 ? paymentMethod.Total.Amount : paymentMethod.Price.Amount,
                },
            };
            return retVal.ToArray();
        }
    }
}
