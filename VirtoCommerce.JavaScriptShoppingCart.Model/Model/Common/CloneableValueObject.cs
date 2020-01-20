using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common
{
    public class CloneableValueObject : ValueObject, ICloneable
    {
        #region ICloneable members
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}
