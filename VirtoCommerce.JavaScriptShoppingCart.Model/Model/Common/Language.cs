using System.Collections.Generic;
using System.Globalization;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common
{
    /// <summary>
    /// Represent language for specified culture.
    /// </summary>
    public partial class Language : CloneableValueObject
    {
        public Language(string cultureName)
        {
            var culture = CultureInfo.InvariantCulture;
            if (!string.IsNullOrEmpty(cultureName))
            {
                culture = CultureInfo.GetCultureInfo(cultureName);
            }

            CultureName = culture.Name;
            ThreeLeterLanguageName = culture.ThreeLetterISOLanguageName;
            TwoLetterLanguageName = culture.TwoLetterISOLanguageName;
            NativeName = culture.NativeName;

            if (culture == CultureInfo.InvariantCulture)
            {
                return;
            }

            var regionInfo = new RegionInfo(culture.LCID);
            TwoLetterRegionName = regionInfo.TwoLetterISORegionName;
            ThreeLetterRegionName = regionInfo.ThreeLetterISORegionName;
        }

        private Language()
            : this(CultureInfo.InvariantCulture.Name)
        {
        }

        public static Language InvariantLanguage
        {
            get
            {
                return new Language();
            }
        }

        public bool IsInvariant
        {
            get
            {
                return CultureName == CultureInfo.InvariantCulture.Name;
            }
        }

        /// <summary>
        /// culture name format (e.g. en-US).
        /// </summary>
        public string CultureName { get; private set; }

        public string NativeName { get; private set; }

        /// <summary>
        ///  Gets the ISO 639-2 three-letter code for the language.
        /// </summary>
        public string ThreeLeterLanguageName { get; private set; }

        /// <summary>
        ///   Gets the ISO 639-1 two-letter code for the language.
        /// </summary>
        public string TwoLetterLanguageName { get; private set; }

        /// <summary>
        ///  Gets the two-letter code defined in ISO 3166 for the country/region.
        /// </summary>
        public string TwoLetterRegionName { get; private set; }

        /// <summary>
        ///  Gets the three-letter code defined in ISO 3166 for the country/region.
        /// </summary>
        public string ThreeLetterRegionName { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object>
                   {
                       CultureName,
                   };
        }
    }
}
