using System.Collections.Generic;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Extensions
{
    public static class DefaultableDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> WithDefaultValue<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue)
        {
            return new DefaultableDictionary<TKey, TValue>(dictionary, defaultValue);
        }
    }
}
