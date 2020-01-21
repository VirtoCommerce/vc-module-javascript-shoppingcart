using System;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

using coreDto = VirtoCommerce.Domain.Commerce.Model;

namespace VirtoCommerce.JavaScriptShoppingCart.Data.Converters
{
    public static class AddressConverter
    {
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

        public static coreDto.Address ToCoreAddressDto(this Address address)
        {
            var retVal = new coreDto.Address
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

                AddressType = (coreDto.AddressType)(int)address.Type
            };

            return retVal;
        }


    }
}
