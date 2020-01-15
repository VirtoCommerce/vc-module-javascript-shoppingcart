namespace VirtoCommerce.JavaScriptShoppingCart.Crawling
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