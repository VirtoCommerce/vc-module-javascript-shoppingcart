using System;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;
using DomainCommerceModels = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    public static class AddressExtensions
    {
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
    }
}
