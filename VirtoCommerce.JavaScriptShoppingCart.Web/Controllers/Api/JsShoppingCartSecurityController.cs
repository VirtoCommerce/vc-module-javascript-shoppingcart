using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
    [RoutePrefix("api/jsShoppingCart")]
    [AllowAnonymous]
    public class JsShoppingCartSecurityController : ApiController
    {
        private readonly ISecurityService _securityService;

        public JsShoppingCartSecurityController(ISecurityService securityService, IRoleManagementService roleManagementService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        [Route("registerUser")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RegisterUser(ApplicationUserExtended user)
        {
            var result = await _securityService.CreateAsync(user);

            return Content(result.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        [HttpGet]
        [Route("getcurrentuser")]
        [ResponseType(typeof(ApplicationUserExtended))]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            var user = await _securityService.FindByNameAsync(User.Identity.Name, UserDetails.Full);
            return Ok(user);
        }
    }
}