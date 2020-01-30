using System.ComponentModel.DataAnnotations;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Models.Requests
{
    public class UpdateShipmentRequest
    {
        /// <summary>
        /// Gets or sets the value of shipping method code.
        /// </summary>
        [Required]
        public string ShipmentMethodCode { get; set; }

        /// <summary>
        /// Gets or sets the value of shipping method option.
        /// </summary>
        [Required]
        public string ShipmentMethodOption { get; set; }


        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        /// <value>
        /// Address object.
        /// </value>
        public Address DeliveryAddress { get; set; }
    }
}
