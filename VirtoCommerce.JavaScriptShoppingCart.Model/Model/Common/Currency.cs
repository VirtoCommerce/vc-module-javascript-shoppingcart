using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using VirtoCommerce.JavaScriptShoppingCart.Core.Extensions;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common
{
    /// <summary>
    /// Represents currency information in storefront.
    /// Contains some extra information as exchange rate, symbol, formatting.
    /// </summary>
    public class Currency : CloneableValueObject
    {
        private static readonly IDictionary<string, string> _isoCurrencySymbolDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase).WithDefaultValue(null);
        private Language _language;
        private string _code;

        static Currency()
        {
            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                try
                {
                    var regionInfo = new RegionInfo(cultureInfo.LCID);
                    _isoCurrencySymbolDict[regionInfo.ISOCurrencySymbol] = regionInfo.CurrencySymbol;
                }
                catch (Exception)
                {
                    // No need to catch
                }
            }
        }

        public Currency(Language language, string code, string name, string symbol, decimal exchangeRate)
            : this(language, code)
        {
            ExchangeRate = exchangeRate;

            if (!string.IsNullOrEmpty(name))
            {
                EnglishName = name;
            }

            if (!string.IsNullOrEmpty(symbol))
            {
                Symbol = symbol;
                NumberFormat.CurrencySymbol = symbol;
            }
        }

        public Currency(Language language, string code)
        {
            _language = language;
            _code = code;
            ExchangeRate = 1;
            Initialize();
        }

        protected Currency()
        {
        }

        /// <summary>
        /// Currency code may be used ISO 4217.
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
                Initialize();
            }
        }

        public string CultureName
        {
            get
            {
                return _language != null ? _language.CultureName : null;
            }

            set
            {
                _language = new Language(value);
                Initialize();
            }
        }

        [JsonIgnore]
        public NumberFormatInfo NumberFormat { get; private set; }

        public string Symbol { get; set; }

        public string EnglishName { get; set; }

        /// <summary>
        /// Exchange rate with primary currency.
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/dwhawy9k%28v=vs.110%29.aspx?f=255&amp;MSPPError=-2147217396.
        /// </summary>
        public string CustomFormatting { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var result = base.Equals(obj);
            if (!result && obj is string code)
            {
                result = code.EqualsInvariant(Code);
            }

            return result;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object>
                   {
                       Code,
                       CultureName,
                   };
        }


        private void Initialize()
        {
            if (_language == null)
            {
                return;
            }

            if (!_language.IsInvariant)
            {
                var cultureInfo = CultureInfo.GetCultureInfo(_language.CultureName);
                NumberFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                var region = new RegionInfo(cultureInfo.LCID);
                EnglishName = region.CurrencyEnglishName;

                if (_code != null)
                {
                    Symbol = _isoCurrencySymbolDict[_code] ?? "N/A";
                    NumberFormat.CurrencySymbol = Symbol;
                }
            }
            else
            {
                NumberFormat = CultureInfo.InvariantCulture.NumberFormat.Clone() as NumberFormatInfo;
            }
        }
    }
}
