using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Cart.Services;
using VirtoCommerce.Domain.Order.Model;
using VirtoCommerce.Domain.Order.Services;
using VirtoCommerce.Domain.Payment.Model;
using VirtoCommerce.Domain.Store.Services;
using VirtoCommerce.OrderModule.Data.Services;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("jscart/api/orders")]
    [CLSCompliant(false)]
    public class JsCartOrderController : ApiController
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ICustomerOrderSearchService _searchService;
        private readonly IStoreService _storeService;
        private readonly IShoppingCartService _cartService;
        private readonly ICustomerOrderBuilder _customerOrderBuilder;

        public JsCartOrderController(ICustomerOrderService customerOrderService, ICustomerOrderSearchService searchService, IStoreService storeService, IShoppingCartService cartService, ICustomerOrderBuilder customerOrderBuilder)
        {
            _customerOrderService = customerOrderService;
            _searchService = searchService;
            _storeService = storeService;
            _cartService = cartService;
            _customerOrderBuilder = customerOrderBuilder;
        }

        /// <summary>
        /// Create new customer order based on shopping cart.
        /// </summary>
        /// <param name="id">shopping cart id</param>
        [HttpPost]
        [ResponseType(typeof(CustomerOrder))]
        [Route("{id}")]
        public async Task<IHttpActionResult> CreateOrderFromCart(string cartId)
        {
            CustomerOrder result;
            using (await AsyncLock.GetLockByKey(cartId).LockAsync())
            {
                var cart = _cartService.GetByIds(new[] { cartId }).FirstOrDefault();
                result = _customerOrderBuilder.PlaceCustomerOrderFromCart(cart);
            }

            return Ok(result);
        }

        /// <summary>
        /// Register customer order payment in external payment system
        /// </summary>
        /// <remarks>Used in storefront checkout or manual order payment registration</remarks>
        /// <param name="orderId">customer order id</param>
        /// <param name="paymentId">payment id</param>
        /// <param name="bankCardInfo">banking card information</param>
        [HttpPost]
        [Route("{orderId}/processPayment/{paymentId}")]
        [ResponseType(typeof(ProcessPaymentResult))]
        public IHttpActionResult ProcessOrderPayments(string orderId, string paymentId,
            [SwaggerOptional] BankCardInfo bankCardInfo)
        {
            var order = _customerOrderService.GetByIds(new[] { orderId }, CustomerOrderResponseGroup.Full.ToString())
                .FirstOrDefault();

            if (order == null)
            {
                var searchCriteria = AbstractTypeFactory<CustomerOrderSearchCriteria>.TryCreateInstance();
                searchCriteria.Number = orderId;
                searchCriteria.ResponseGroup = CustomerOrderResponseGroup.Full.ToString();

                order = _searchService.SearchCustomerOrders(searchCriteria).Results.FirstOrDefault();
            }

            if (order == null)
            {
                throw new InvalidOperationException($"Cannot find order with ID {orderId}");
            }

            var payment = order.InPayments.FirstOrDefault(x => x.Id == paymentId);
            if (payment == null)
            {
                throw new InvalidOperationException($"Cannot find payment with ID {paymentId}");
            }

            var store = _storeService.GetById(order.StoreId);
            var paymentMethod = store.PaymentMethods.FirstOrDefault(x => x.Code == payment.GatewayCode);
            if (paymentMethod == null)
            {
                throw new InvalidOperationException($"Cannot find payment method with code {payment.GatewayCode}");
            }

            var context = new ProcessPaymentEvaluationContext
            {
                Order = order,
                Payment = payment,
                Store = store,
                BankCardInfo = bankCardInfo,
            };

            var result = paymentMethod.ProcessPayment(context);
            if (result.OuterId != null)
            {
                payment.OuterId = result.OuterId;
            }

            _customerOrderService.SaveChanges(new[] { order });

            return Ok(result);
        }
    }
}
