using System;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Extensions
{
	public static class CacheKey
	{
		public static string With(params string[] keys)
		{
			return string.Join("-", keys);
		}

		public static string With(Type ownerType, params string[] keys)
		{
			return With($"{ownerType.GetCacheKey()}:{string.Join("-", keys)}");
		}
	}
}
