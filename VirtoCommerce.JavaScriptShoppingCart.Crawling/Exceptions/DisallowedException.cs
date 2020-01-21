namespace VirtoCommerce.JavaScriptShoppingCart.Crawling.Exceptions
{
    using System;

    public class DisallowedException : Exception
    {
        public DisallowedException(string message)
            : base(message)
        {
        }
    }
}
